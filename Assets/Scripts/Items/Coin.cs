using UnityEngine;

public class Coin : BaseItem
{
    public override void PickUp()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>().PickUpCoin();
        Destroy(gameObject);
    }
}
