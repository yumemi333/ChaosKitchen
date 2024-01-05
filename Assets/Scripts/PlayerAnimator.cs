using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Player player;
    private const string IS_WALKING = "IsWalking";

    private void Reset()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();        
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking);
    }
}
