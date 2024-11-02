using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    Image stat;
    Player player;

    void Start()
    {
        this.stat = GetComponent<Image>();
        player= GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (player==null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
        stat.fillAmount = (float)player.GetHealth() / 100;
    }

}
