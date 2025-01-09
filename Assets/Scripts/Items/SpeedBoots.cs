using UnityEngine;

public class SpeedBoots : BaseItem
{
    public override void PickUp()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        player.PickUpSpeedBoost();
        SetUI();
        Destroy(gameObject);
    }
}
