using UnityEngine;

public class StrengthBoost : BaseItem
{
    public override void PickUp()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
        StartCoroutine(player.SetDamage());
        Destroy(gameObject);
    }
}
