using UnityEngine;

public class SpeedBoots : BaseItem
{
    public override void PickUp()
    {
        UseEffect();
        Destroy(gameObject);
    }
    public override void UseEffect()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>(); 
        if (AlreadyHasThisBoost())
        {
            var _UI = GameObject.FindGameObjectWithTag(_tag);
            Destroy(_UI.gameObject);
        }
        SetUI();
        player.PickUpSpeedBoost();
        
    }
    public override int GetPrice()
    {
        return 3;
    }
    public override bool AlreadyHasThisBoost()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        return player.AlreadyBoost();
    }
}
