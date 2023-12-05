using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    private bool isWalking = false;
    public bool IsWalking => isWalking;

    private void Update()
    {
        Vector2 inputVector = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y++;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y--;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x--;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x++;
        }

        inputVector = inputVector.normalized;
        isWalking = inputVector != Vector2.zero;

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        transform.position += moveDir * Time.deltaTime * moveSpeed;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * 10);
    }
}
