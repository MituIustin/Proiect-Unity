using UnityEngine;

public class HealhPotion : BaseItem
{
    public override void PickUp()
    {
        UseEffect();
        Destroy(gameObject);
    }
    public override void UseEffect()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
        player.SetHealth(20);
    }
    public override int GetPrice()
    {
        return 2;
    }
    public override bool AlreadyHasThisBoost()
    {
        return false;
    }
}
