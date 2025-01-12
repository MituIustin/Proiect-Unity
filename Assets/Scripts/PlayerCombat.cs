using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    Animator animator;
    public Collider swordHitbox;

    float lastClickedTime = 0f;
    float doubleClickThreshold = 0.5f;

    int health = 100;
    int damage = 15;

    int coins = 100;

    int _knives = 60;
    int _bombs = 50;

    bool _hasDamageBoost;

    bool canHit = true;
    private bool isAttacking = false;

    public GameObject _knifePrefab;
    public GameObject _bombPrefab;

    public AudioSource attackSound;

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
        _hasDamageBoost = false;
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

            if (attackSound != null)
            {
                attackSound.Play();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("IsAttacking", false);
            isAttacking = false;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (_knives > 0)
            {
                SpawnKnife();
                _knives--;
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (_bombs > 0)
            {
                DeployBomb();
            }
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
        gameObject.SetActive(false);
    }

    public void SetHealth(int health_)
    {
        health = Mathf.Min(health + health_, 100);
    }

    public void PickUpDamageBoost()
    {
        if (_hasDamageBoost)
        {
            StopCoroutine(SetDamage());
        }
        StartCoroutine(SetDamage());
    }

    private IEnumerator SetDamage()
    {
        _hasDamageBoost = true;
        damage = 25;
        yield return new WaitForSeconds(15f);
        damage = 15;
        _hasDamageBoost = false;
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

    public void SpendCoins(int price)
    {
        coins = coins - price;
    }

    public bool AlreadyBoost()
    {
        return _hasDamageBoost;
    }

    public void BuyKnife()
    {
        _knives++;
    }

    public void BuyBomb()
    {
        _bombs++;
    }

    public int GetKnives()
    {
        return _knives;
    }

    public int GetBombs()
    {
        return _bombs;
    }

    void DeployBomb()
    {
        var bomb = Instantiate(_bombPrefab);
        var pos = transform.position;
        bomb.transform.position = new Vector3(pos.x + 1, pos.y + 2, pos.z);
        bomb.GetComponent<Bomb>().UseBomb();
        _bombs--;
    }

    void SpawnKnife()
    {
        if (_knifePrefab != null)
        {
            Vector3 firePoint;
            var facingRight = transform.localScale.x > 0;
            if (facingRight)
            {
                firePoint = transform.position + new Vector3(1.2f, 0f, 0f);
            }
            else
            {
                firePoint = transform.position + new Vector3(-1.2f, 0f, 0f);
            }

            GameObject knife = Instantiate(_knifePrefab, firePoint, Quaternion.identity);

            knife.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);

            FlyingKnife arrowScript = knife.GetComponent<FlyingKnife>();
            if (arrowScript != null)
            {
                float direction = facingRight ? 1f : -1f;
                Rigidbody arrowRb = knife.GetComponent<Rigidbody>();
                if (arrowRb != null)
                {
                    arrowRb.linearVelocity = new Vector3(direction * 20, 2f, 0f);
                }
                knife.transform.rotation = Quaternion.Euler(0, facingRight ? 180 : 0, -45);
            }
        }
    }
}
