using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    Animator animator;
    public Collider swordHitbox;

    float lastClickedTime = 0f;
    float doubleClickThreshold = 0.5f;

    int health = 60;
    int damage = 15;

    int coins = 0;

    bool canHit = true;
    private bool isAttacking = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (isAttacking && collision.collider.CompareTag("Enemy"))
        {
            collision.gameObject.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canHit)
        {
            if (Time.time - lastClickedTime <= doubleClickThreshold)
            {
                animator.SetBool("IsAttackingTwice", !animator.GetBool("IsAttackingTwice"));
            }
            else
            {
                animator.SetBool("IsAttackingTwice", false);
            }

            StartCoroutine(EnableHitboxTemporarily());
            animator.SetBool("IsAttacking", true);
            isAttacking = true; 
            lastClickedTime = Time.time;
        }

        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("IsAttacking", false);
            isAttacking = false; 
        }
    }

    IEnumerator EnableHitboxTemporarily()
    {
        swordHitbox.enabled = true;
        yield return new WaitForSeconds(0.2f);
        swordHitbox.enabled = false;
    }

    public int GetHealth() { return health; }

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
        Debug.Log("Player has died.");
        gameObject.SetActive(false);
    }

    public void SetHealth(int health_)
    {
        health = Mathf.Min(health + health_, 100);
    }

    public void PickUpDamageBoost()
    {
        StartCoroutine(SetDamage());
    }
    private IEnumerator SetDamage()
    {
        damage = 25;
        yield return new WaitForSeconds(15f);
        damage = 15;
    }

    public void SetCanHit(bool newCanHit) { canHit = newCanHit; }

    public void PickUpCoin()
    {
        coins++;
    }
    public int GetCoins()
    {
        return coins;
    }
}
