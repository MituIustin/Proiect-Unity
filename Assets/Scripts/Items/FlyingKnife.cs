using UnityEngine;

public class FlyingKnife : BaseItem
{
    public float speed = 10f;
    public int damage = 10;


    void Start()
    {

        Destroy(gameObject, 3f);
    }

    void Update()
    {
    }

    protected override void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<MeleeEnemy>().TakeDamage(20);
        }

        Destroy(gameObject);
    }

    public override int GetPrice()
    {
        return 2;
    }

    public override bool AlreadyHasThisBoost()
    {
        return false;
    }

    public override void PickUp()
    {
        
    }

    public override void UseEffect()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>().BuyKnife();
    }
}
