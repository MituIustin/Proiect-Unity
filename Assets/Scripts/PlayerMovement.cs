using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    PlayerCombat playerCombat;
    public GameObject UI;
    GameObject _UI;

    float movementSpeedHorizontal = 5f;
    float movementSpeedVertical = 3f;
    float dashSpeedMultiplier = 3f;
    float dashDuration = 0.1f;
    float dashCooldown = 1.5f;
    float dashCooldownTimer = 1.5f;

    bool _isDead;

    bool isDashing = false;
    bool canDash = true;

    Vector3 direction;
    Vector3 dashDirection;

    bool _hasSpeedBoost;

    bool _pauseMenu;
    public GameObject PauseMenuPrefab;

    void Start()
    {
        _hasSpeedBoost = false;
        _UI = Instantiate(UI);
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        playerCombat = GetComponent<PlayerCombat>();
        _pauseMenu = false;
    }

    private void FixedUpdate()
    {
        float speedMultiplier = (isDashing && canDash) ? dashSpeedMultiplier : 1f; 
        Vector3 moveDirection = isDashing ? dashDirection : direction; 
        rb.MovePosition(transform.position + moveDirection * speedMultiplier * Time.fixedDeltaTime);
    }

    void Update()
    {
        if (!playerCombat.IsDead())
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            bool isRunning = (horizontal != 0) || (vertical != 0);
            animator.SetBool("IsRunning", isRunning);

            if (horizontal < 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (horizontal > 0)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
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

            if (Input.GetKeyDown(KeyCode.P) && !PauseMenu())
            {
                Instantiate(PauseMenuPrefab);
                SetUI(false);
                SetPauseMenu(true);
            }
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

    public void PickUpSpeedBoost()
    {
        if (_hasSpeedBoost)
        {
            StopCoroutine(SetSpeed());
        }
        StartCoroutine(SetSpeed());
        
    }

    public bool AlreadyBoost()
    {
        return _hasSpeedBoost;
    }
    private IEnumerator SetSpeed()
    {
        _hasSpeedBoost = true;
        movementSpeedHorizontal = 7f;
        movementSpeedVertical = 5f;
        yield return new WaitForSeconds(15);
        movementSpeedHorizontal = 5f;
        movementSpeedVertical = 3f;
        _hasSpeedBoost = false;
    }

    public void SetUI(bool active)
    {
        _UI.SetActive(active);
    }

    public bool PauseMenu()
    {
        return _pauseMenu;
    }
    public void SetPauseMenu(bool active)
    {
        _pauseMenu = active;
    }
}
