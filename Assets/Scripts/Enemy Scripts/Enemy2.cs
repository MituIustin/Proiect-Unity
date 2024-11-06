using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float detectionRange = 10f;
    public float attackRange = 1.5f;
    public float moveSpeed = 2f;
    public int AnimState = 0;
    private Transform player;
    private Animator animator;
    private bool isAttacking = false;
    private bool facingRight = true;

    void Start()
    {
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(2f);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            AttackPlayer();
        }
        else if (distanceToPlayer <= detectionRange)
        {
            isAttacking = false;
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

        animator.SetInteger("AnimState", 2);

        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;

        FlipTowardsPlayer();

        transform.position = Vector3.MoveTowards(transform.position,
                                                 new Vector3(player.position.x, transform.position.y, player.position.z),
                                                 moveSpeed * Time.deltaTime);
    }

    void AttackPlayer()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("Attack");
        }
    }

    void Idle()
    {
        isAttacking = false;
        animator.SetInteger("AnimState", 0);
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

    public void OnAttackAnimationEnd()
    {
        isAttacking = false;
    }
}
