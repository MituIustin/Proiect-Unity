using UnityEngine;
using UnityEngine.TextCore;

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
        if (AlreadyHasThisBoost())
        {
            var _UI = GameObject.FindGameObjectWithTag(_tag);
            Destroy(_UI.gameObject);
        }
        SetUI();
        player.PickUpDamageBoost();
        
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
