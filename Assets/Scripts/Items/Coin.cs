using UnityEngine;

public class Coin : BaseItem
{
    public override void PickUp()
    {
        UseEffect();
        Destroy(gameObject);
    }

    public override void UseEffect()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>().PickUpCoin();
    }
    public override int GetPrice()
    {
        return 1;
    }

    public override bool AlreadyHasThisBoost()
    {
        return false;
    }
}
