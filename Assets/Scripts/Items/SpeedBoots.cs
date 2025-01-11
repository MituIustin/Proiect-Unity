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
        player.PickUpSpeedBoost();
        SetUI(AlreadyHasThisBoost());
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
