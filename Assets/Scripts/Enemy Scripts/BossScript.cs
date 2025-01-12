using System.Collections;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public float maintainRange = 5f;
    public float moveSpeed = 5f;
    public int health = 200;
    public GameObject[] enemyPrefabs;
    public GameObject energyBallPrefab;
    public float energyBallSpeed = 10f;
    public float attackCooldown = 2f;
    public int attackDamage = 10;
    public float movementCooldown = 3f;

    private Transform player;
    private Animator animator;
    private bool isAttacking = false;
    private bool isMoving = false;
    private bool facingRight = true;
    private float lastAttackTime = 0f;
    private float lastMoveTime = 0f;

    void Start()
    {
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(0.1f);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (!isMoving && Time.time >= lastMoveTime + movementCooldown)
        {
            if (distanceToPlayer > maintainRange)
            {
                isMoving = true;
                StartCoroutine(MoveTowardsPlayer());
            }
            else if (distanceToPlayer < maintainRange)
            {
                isMoving = true;
                StartCoroutine(MoveAwayFromPlayer());
            }
            lastMoveTime = Time.time;
        }
        else
        {
            if (!isAttacking && !isMoving)
            {
                AttackLogic();
            }
        }
    }


    IEnumerator MoveTowardsPlayer()
    {
        animator.SetInteger("AnimState", 4);
        while (Vector3.Distance(transform.position, player.position) > maintainRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y, player.position.z), moveSpeed * Time.deltaTime);
            yield return null;
        }
        isMoving = false;
    }

    IEnumerator MoveAwayFromPlayer()
    {
        animator.SetInteger("AnimState", 4);
        while (Vector3.Distance(transform.position, player.position) < maintainRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x - (player.position.x - transform.position.x), transform.position.y, player.position.z), moveSpeed * Time.deltaTime);
            yield return null;
        }
        isMoving = false;
    }


    void AttackLogic()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            int attackChoice = Random.Range(0, 10);

            if (attackChoice < 4)
                StartCoroutine(PerformAttack1());
            else if (attackChoice < 8)
                StartCoroutine(PerformAttack2());
            else
                StartCoroutine(PerformAttack3());

            lastAttackTime = Time.time;
        }
    }

    IEnumerator PerformAttack1()
    {
        FlipTowardsPlayer();
        isAttacking = true;
        animator.SetInteger("AnimState", 1);
        yield return new WaitForSeconds(0.5f);
        SpawnEnergyBallReturning();
        yield return new WaitForSeconds(attackCooldown - 0.5f);
        isAttacking = false;
        animator.SetInteger("AnimState", 0);
    }

    IEnumerator PerformAttack2()
    {
        isAttacking = true;
        animator.SetInteger("AnimState", 2);
        yield return new WaitForSeconds(0.5f);
        SpawnEnergyBallAbovePlayer();
        yield return new WaitForSeconds(attackCooldown - 0.5f);
        isAttacking = false;
        animator.SetInteger("AnimState", 0);
    }

    IEnumerator PerformAttack3()
    {
        isAttacking = true;
        animator.SetInteger("AnimState", 3);
        yield return new WaitForSeconds(0.5f);
        Vector3[] spawnOffsets = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
        for (int i = 0; i < 4; i++)
        {
            GameObject enemyToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            //Instantiate(enemyToSpawn, transform.position + spawnOffsets[i] * 2, Quaternion.identity);
        }
        yield return new WaitForSeconds(attackCooldown - 0.5f);
        isAttacking = false;
        animator.SetInteger("AnimState", 0);
    }

    void SpawnEnergyBallReturning()
    {
        Vector3 spawnPosition = transform.position + new Vector3(facingRight ? 1 : -1, -0.5f, 0);
        GameObject ball = Instantiate(energyBallPrefab, spawnPosition, Quaternion.identity);
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direction = facingRight ? Vector3.right : Vector3.left;
            rb.linearVelocity = direction * energyBallSpeed;
            ball.transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
        }
    }

    void SpawnEnergyBallAbovePlayer()
    {
        if (player == null) return;
        Vector3 spawnPosition = new Vector3(player.position.x, player.position.y + 5f, player.position.z);
        GameObject ball = Instantiate(energyBallPrefab, spawnPosition, Quaternion.Euler(0, 0, 270));
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.down * energyBallSpeed;
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
        else
        {
            Debug.Log("Damage Boss");
            animator.SetInteger("AnimState", 5);
        }
    }

    void Die()
    {
        animator.SetInteger("AnimState", 6);
        Destroy(gameObject, 2f);
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
