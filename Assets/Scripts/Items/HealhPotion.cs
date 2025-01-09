using UnityEngine;

public class HealhPotion : BaseItem
{
    public override void PickUp()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
        player.SetHealth(20);
        Destroy(gameObject);
    }
}
