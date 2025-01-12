using System.Collections;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    public float detectionRange = 75f;
    public float attackRange = 0.75f;
    public float moveSpeed = 3f;
    public int health = 50;
    private Transform player;
    private Animator animator;
    private bool isAttacking = false;
    private bool facingRight = true;
    [SerializeField] private float attackCooldown = 0.95f;
    private float lastAttackTime = 0f;
    public int attackDamage = 2;

    private int _keyChance = 5;
    private int _itemChance = 30;

    public GameObject healthPotion;
    public GameObject damagePotion;
    public GameObject speedPotion;
    public GameObject key;
    public GameObject coin;

    private SpriteRenderer spriteRenderer; 
    private Rigidbody rb;  

    void Start()
    {
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(2f);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();  
        rb = GetComponent<Rigidbody>();  
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
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

        animator.SetInteger("AnimState", 2);
        FlipTowardsPlayer();
        transform.position = Vector3.MoveTowards(transform.position,
                                                 new Vector3(player.position.x, transform.position.y, player.position.z),
                                                 moveSpeed * Time.deltaTime);
    }

    IEnumerator PerformAttack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(0.5f);

        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= attackRange)
            {
                PlayerCombat playerCombat = player.GetComponent<PlayerCombat>();
                if (playerCombat != null)
                {
                    playerCombat.TakeDamage(attackDamage);
                }
            }
        }

        yield return new WaitForSeconds(attackCooldown - 0.5f); 

        isAttacking = false;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red; 
        }

        if (rb != null)
        {
            Vector3 knockbackDirection = (transform.position - player.position).normalized; 
            rb.AddForce(knockbackDirection * 5f, ForceMode.Impulse); 
        }

        if (health <= 0)
        {
            Die();
        }

        StartCoroutine(ResetColor());
    }

    private IEnumerator ResetColor()
    {
        yield return new WaitForSeconds(0.1f); 
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white; 
        }
    }

    public void Die()
    {
        if (!(health > 0))
        {
            DropItem();
        }
        Debug.Log("Enemy has died.");
        Destroy(gameObject);
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

    void DropItem()
    {
        GameObject item = null;
        var itemChance = Random.Range(0, 100);
        if (itemChance < _itemChance)
        {
            var itemToDrop = Random.Range(0, 3);
            switch (itemToDrop)
            {
                case 0:
                    item = Instantiate(healthPotion);
                    break;
                case 1:
                    item = Instantiate(speedPotion);
                    break;
                case 2:
                    item = Instantiate(damagePotion);
                    break;
            }
        }
        var keyChance = Random.Range(0, 100);
        if (keyChance < _keyChance && item == null)
        {
            item = Instantiate(key);
        }
        if (item == null)
        {
            item = Instantiate(coin);
        }
        var positionToSpawn = transform.position;
        positionToSpawn.y = positionToSpawn.y + 0.5f;
        item.transform.position = positionToSpawn;
        if (item.name.ToLower().Contains("new game object"))
        {
            return;
        }
    }
}
