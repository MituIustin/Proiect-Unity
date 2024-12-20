using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    PlayerCombat playerCombat;

    float movementSpeedHorizontal = 5f;
    float movementSpeedVertical = 3f;
    float dashSpeedMultiplier = 3f;
    float dashDuration = 0.1f;
    float dashCooldown = 1.5f;
    float dashCooldownTimer = 1.5f;

    bool isDashing = false;
    bool canDash = true;

    Vector3 direction;
    Vector3 dashDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        playerCombat = GetComponent<PlayerCombat>();
    }

    private void FixedUpdate()
    {
        float speedMultiplier = (isDashing && canDash) ? dashSpeedMultiplier : 1f; 
        Vector3 moveDirection = isDashing ? dashDirection : direction; 
        rb.MovePosition(transform.position + moveDirection * speedMultiplier * Time.fixedDeltaTime);
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        bool isRunning = (horizontal != 0) || (vertical != 0);
        animator.SetBool("IsRunning", isRunning);

        if (horizontal < 0)
        {
            spriteRenderer.flipX = true; 
        }
        else if (horizontal > 0)
        {
            spriteRenderer.flipX = false; 
        }

        direction = new Vector3(horizontal, 0, vertical).normalized;
        direction.x *= movementSpeedHorizontal;
        direction.z *= movementSpeedVertical;

        if (!canDash)
        {
            dashCooldownTimer += Time.deltaTime;
        }

        if (dashCooldownTimer >= dashCooldown)
        {
            canDash = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && canDash)
        {
            dashDirection = direction;
            playerCombat.SetCanHit(false);
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        animator.SetBool("IsDashing", true);
        yield return new WaitForSeconds(dashDuration);
        canDash = false;
        dashCooldownTimer = 0f;
        isDashing = false;
        animator.SetBool("IsDashing", false);
        playerCombat.SetCanHit(true);
    }
}
