using System;
using UnityEngine;

public class Player : MonoBehaviour
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
        public ClearCounter SelectedCounter;
    }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counters;

    private bool isWalking = false;
    public bool IsWalking => isWalking;

    private float playerHeight = 2f;
    private float interactDistance = 2f;
    private float playerRadius = 0.7f;

    private Vector3 lastInteractDir;
    private ClearCounter selectedCounter = null;

    private void Awake()
    {
        if(instance == null)
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
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact();
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    private void HandleMovement()
    {
        float moveDistance = Time.deltaTime * moveSpeed;
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        // 進行方向には進めない場合
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;

            // X方向での回転だけはできるかチェック
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                // zだけならいけるか確認
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;

                // X方向での回転だけはできるかチェック
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

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

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * 10);
    }

    private void HandleInteraction()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, counters))
        {
            // ClearCounterに当たった
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                if (selectedCounter != clearCounter)
                {
                    SetSlectedCounter(clearCounter);
                }
            }
            else
            {
                SetSlectedCounter(null);
            }
        }
        else
        {
            SetSlectedCounter(null);
        }
    }

    private void SetSlectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            SelectedCounter = this.selectedCounter
        });
    }
}
