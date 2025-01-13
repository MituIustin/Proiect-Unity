using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    public float lifetime = 5f;
    public int damage = 6;
    private Animator animator;
    private Rigidbody rb;
    private bool isCollided = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerCombat playerCombat = other.gameObject.GetComponent<PlayerCombat>();
            if (playerCombat != null)
            {
                playerCombat.TakeDamage(damage);
            }

            animator.SetInteger("AnimState", 1);
            transform.position = other.transform.position;
            StopMovement();
            Destroy(gameObject, 0.5f);
        }
        else
        {
            if (other.CompareTag("Ground"))
            {
                animator.SetInteger("AnimState", 1);
                StopMovement();
                Destroy(gameObject, 0.5f);
            }
        }
    }

    void StopMovement()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.isKinematic = true;
        }
    }
}
