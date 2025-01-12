using UnityEngine;

public class StrengthBoost : BaseItem
{
    public override void PickUp()
    {
        UseEffect();
        Destroy(gameObject);
    }
    public override void UseEffect()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
        player.PickUpDamageBoost();
        SetUI(AlreadyHasThisBoost());
    }
    public override int GetPrice()
    {
        return 4;
    }
    public override bool AlreadyHasThisBoost()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
        return player.AlreadyBoost();
    }
}
