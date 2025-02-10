using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Анимация ходьбы
        bool isWalking = Mathf.Abs(horizontal) > 0 || Mathf.Abs(vertical) > 0;
        animator.SetBool("isWalking", isWalking);
       
    }
}
