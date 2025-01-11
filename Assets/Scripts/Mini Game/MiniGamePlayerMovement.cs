using UnityEngine;

public class MiniGamePlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;

    float moveSpeed = 3.5f;
    float moveX, moveY;

    Vector3 moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        if (moveX < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveX > 0) 
        {
            spriteRenderer.flipX= false;
        }

        moveDirection = new Vector3(moveX, moveY, 0);
        moveDirection.Normalize();
        if (moveDirection.magnitude < 0.01f)
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetBool("isMoving", true);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);
    }
}
