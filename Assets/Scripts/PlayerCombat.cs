using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    Animator animator;
    public Collider swordHitbox;

    float lastClickedTime = 0f;
    float doubleClickThreshold = 0.5f;

    int health = 100;
    int damage = 15;

    bool canHit = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            //collision.gameObject.TakeDamage(damage);
            Debug.Log("hit!\n");
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
                if (!animator.GetBool("IsAttackingTwice"))
                {
                    animator.SetBool("IsAttackingTwice", true);
                }
                else
                {
                    animator.SetBool("IsAttackingTwice", false);
                }
            }
            else
            {
                animator.SetBool("IsAttackingTwice", false);
            }

            swordHitbox.enabled = true;
            animator.SetBool("IsAttacking", true);
            lastClickedTime = Time.time;
        }

        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("IsAttacking", false);
            swordHitbox.enabled = false;
        }
    }
    
    public int GetHealth() { return health; }  

    public void SetCanHit(bool newCanHit) { canHit = newCanHit; }
}
