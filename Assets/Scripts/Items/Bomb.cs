using System.Collections;
using UnityEngine;

public class Bomb : BaseItem
{
    float _explosionRadius = 3f;
    float _damage = 50f;
    private Animator animator;
    private Vector3 originalScale;

    public override bool AlreadyHasThisBoost()
    {
        return false;
    }

    public override int GetPrice()
    {
        return 5;
    }

    public override void PickUp()
    {
    }

    public override void UseEffect()
    {
    }

    public void UseBomb()
    {
        animator = GetComponent<Animator>();
        originalScale = transform.localScale;  
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(1.6f);

        if (animator != null)
        {
            animator.SetInteger("AnimState", 1);
        }
        else
        {
            Debug.LogError("Animator is not assigned or found.");
        }

        transform.localScale = originalScale * 3f;
        yield return new WaitForSeconds(0.9f);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _explosionRadius);
        foreach (Collider hit in hitColliders)
        {
            if (hit.CompareTag("Enemy"))
            {
                hit.GetComponent<MeleeEnemy>().TakeDamage(100);
            }
        }

        Destroy(gameObject);
    }
}
