using UnityEngine;
using System.Collections;

public class BossScript : MonoBehaviour
{
    private Animator animator;
    private bool isDead = false;
    private float health = 100f;
    private bool isAttacking = false;
    private bool isApproaching = false;
    private bool isRetreating = false;
    private int attack1Count = 0;
    private int attack2Count = 0;
    private Transform player;
    public float attackRange = 1.5f;
    public float detectionRange = 10f;
    public float moveSpeed = 2f;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (isDead) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange && !isAttacking && !isDead)
        {
            if (attack1Count < 3)
            {
                StartCoroutine(PerformAttack1());
            }
            else if (attack2Count < 1)
            {
                StartCoroutine(PerformAttack2());
            }
            else
            {
                attack1Count = 0;
                attack2Count = 0;
            }
        }

        if (isApproaching)
        {
            MoveTowardsPlayer();
        }

        if (isRetreating)
        {
            MoveAwayFromPlayer();
        }

        if (!isAttacking && !isApproaching && !isRetreating)
        {
            SetAnimState(0);
        }
    }

    private void MoveTowardsPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y, player.position.z), moveSpeed * Time.deltaTime);
    }

    private void MoveAwayFromPlayer()
    {
        Vector3 retreatDirection = transform.position - player.position;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + retreatDirection, moveSpeed * Time.deltaTime);
    }

    private void SetAnimState(int state)
    {
        animator.SetInteger("AnimState", state);
    }

    private IEnumerator PerformAttack1()
    {
        isAttacking = true;
        isApproaching = true;
        SetAnimState(1);
        yield return new WaitForSeconds(1f);
        attack1Count++;
        isApproaching = false;
        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }

    private IEnumerator PerformAttack2()
    {
        isAttacking = true;
        isRetreating = true;
        SetAnimState(2);
        yield return new WaitForSeconds(1f);
        attack2Count++;
        isRetreating = false;
        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        SetAnimState(3);
        Invoke("BackToIdle", 0.5f);
    }

    private void BackToIdle()
    {
        SetAnimState(0);
    }

    private void Die()
    {
        isDead = true;
        SetAnimState(5);
        Destroy(gameObject, 2f);
    }
}
