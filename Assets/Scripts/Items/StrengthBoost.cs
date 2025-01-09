using UnityEngine;

public class StrengthBoost : BaseItem
{
    public override void PickUp()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
        player.PickUpDamageBoost();
        SetUI();
        Destroy(gameObject);
    }
}
