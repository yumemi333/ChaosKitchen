using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    private static Player instance;

    public static Player Instance
    {
        get
        {
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter SelectedCounter;
    }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;

    private bool isWalking = false;
    public bool IsWalking => isWalking;

    private float playerHeight = 0.5f;
    private float interactDistance = 0.1f;
    private float playerRadius = 0.7f;

    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter = null;
    private Cannon cannon = null;

    private KitchenObject kitchenObject = null;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("there is one more player instance");
        }
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
        if (cannon != null)
        {
            cannon.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    private void HandleMovement()
    {
        if (!KitchenGameManager.Instance.IsGamePlaying)
        {
            return;
        }
        float moveDistance = Time.deltaTime * moveSpeed;
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        // 進行方向には進めない場合
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;

            // X方向での回転だけはできるかチェック
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                // zだけならいけるか確認
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;

                // Z方向での回転だけはできるかチェック
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }
        }

        if (!canMove)
        {
            return;
        }

        isWalking = inputVector != Vector2.zero;
        transform.position += moveDir * moveDistance;

        if (moveDir == Vector3.zero)
        {
            return;
        }
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * 10);
    }

    private void HandleInteraction()
    {
        if (!KitchenGameManager.Instance.IsGamePlaying)
        {
            return;
        }
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        if (Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, lastInteractDir, out RaycastHit hit, interactDistance))
        {
            // ClearCounterに当たった
            if (hit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (selectedCounter != baseCounter)
                {
                    SetSlectedCounter(baseCounter);
                }
            }      
            else if (hit.transform.TryGetComponent(out Cannon cannon))
            {
                SetCannon(cannon);
            }
            else
            {
                SetSlectedCounter(null);
            }
        }
        else
        {
            SetSlectedCounter(null);
            //SetCannon(null);
        }
    }

    private void SetSlectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            SelectedCounter = this.selectedCounter
        });
    }
    private void SetCannon(Cannon cannon)
    {
        this.cannon = cannon;
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
