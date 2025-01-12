using System.Collections;
using UnityEngine;

public class MiniGamePlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;

    float moveSpeed = 5f;
    float moveX, moveY;

    Vector3 moveDirection;

    public bool isDead = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator.applyRootMotion = false;
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
        if (moveDirection.magnitude * moveSpeed < 0.01f)
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetBool("isMoving", true);
        }

        if (isDead)
        {
            StartCoroutine(Die());
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);
    }

    IEnumerator Die()
    {
        moveSpeed = 0;

        float elapsedTime = 0f;
        float duration = 1f;

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, -90), elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0, 0, -90);
    }
}
