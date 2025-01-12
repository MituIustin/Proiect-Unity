using System.Collections;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public float detectionRange = 10f;
    public float attackRange = 1.5f;
    public float moveSpeed = 1.2f;
    public int health = 50;
    public GameObject arrowPrefab;
    public float arrowSpeed = 7f;

    private Transform player;
    private Animator animator;
    private Rigidbody rb;
    private bool isAttacking = false;
    private bool facingRight = true;
    private float lastAttackTime = 0f;
    public float attackCooldown = 1.5f;
    public int attackDamage = 5;

    void Start()
    {
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(0.1f);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (player == null) return;
        
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            if (Mathf.Abs(transform.position.y - player.position.y) > 0.3f)
            {
                animator.SetInteger("AnimState", 1);
                float step = 1.2f * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position,
                                                         new Vector3(transform.position.x, transform.position.y, player.position.z),
                                                         step);
            }
            if (Time.time >= lastAttackTime + attackCooldown && !isAttacking)
            {
                StartCoroutine(PerformAttack());
                lastAttackTime = Time.time;
            }
        }
        else if (distanceToPlayer <= detectionRange)
        { 
                ChasePlayer();
        }
        else
        {
            Idle();
        }
    }

    void ChasePlayer()
    {
        if (isAttacking) return;

        animator.SetInteger("AnimState", 1);

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y, player.position.z), moveSpeed * Time.deltaTime);
    }

    IEnumerator PerformAttack()
    {
        FlipTowardsPlayer();
        isAttacking = true;
        animator.SetInteger("AnimState", 2);

        yield return new WaitForSeconds(0.5f); 

        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= attackRange)
            {
                SpawnArrow();
            }
        }

        yield return new WaitForSeconds(attackCooldown - 0.5f);

        isAttacking = false;
        animator.SetInteger("AnimState", 0); 
    }

    void SpawnArrow()
    {
        if (arrowPrefab != null)
        {
            Vector3 firePoint;
            if (facingRight)
            {
                firePoint = transform.position + new Vector3(1.2f, 1f, 0f);
            }
            else
            {
                firePoint = transform.position + new Vector3(-1.2f, 1f, 0f);
            }

            GameObject arrow = Instantiate(arrowPrefab, firePoint, Quaternion.identity);

            arrow.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);

            Arrow arrowScript = arrow.GetComponent<Arrow>();
            if (arrowScript != null)
            {
                float direction = facingRight ? 1f : -1f;
                Rigidbody arrowRb = arrow.GetComponent<Rigidbody>();
                if (arrowRb != null)
                {
                    arrowRb.linearVelocity = new Vector3(direction * arrowSpeed, 2f, 0f);
                }
                arrow.transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 90);
            }
        }
    }





    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetInteger("AnimState", 5);
        Destroy(gameObject, 1f);
        animator.SetBool("IsDead", true);
    }

    void Idle()
    {
        if (!isAttacking)
        {
            animator.SetInteger("AnimState", 0);
        }
    }

    void FlipTowardsPlayer()
    {
        if (player == null) return;

        if ((player.position.x > transform.position.x && !facingRight) ||
            (player.position.x < transform.position.x && facingRight))
        {
            facingRight = !facingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
}
