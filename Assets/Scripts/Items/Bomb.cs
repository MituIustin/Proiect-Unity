using System.Collections;
using UnityEngine;

public class Bomb : BaseItem
{
    float _explosionRadius = 3f;
    float _damage = 50f;
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
        StartCoroutine(Explode());
    }
    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(3);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _explosionRadius);
        foreach (Collider hit in hitColliders)
        {
            if(hit.tag == "Enemy")
            {
                hit.GetComponent<MeleeEnemy>().TakeDamage(100);
            }
        }
        Debug.Log("boom");

        Destroy(gameObject); 
    }
}
