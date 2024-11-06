using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    Image stat;
    PlayerCombat player;

    void Start()
    {
        this.stat = GetComponent<Image>();
        player= GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
    }

    void Update()
    {
        if (player==null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
        }
        stat.fillAmount = (float)player.GetHealth() / 100;
    }

}
