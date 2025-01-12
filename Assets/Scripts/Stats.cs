using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    Image stat;
    PlayerCombat player; 
    TextMeshProUGUI _numberOfKnives;
    TextMeshProUGUI _numberOfBombs;

    void Start()
    {
        this.stat = GetComponent<Image>();
        player= GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
        _numberOfKnives = GameObject.FindGameObjectWithTag("numberOfKnives").GetComponent<TextMeshProUGUI>();
        _numberOfBombs = GameObject.FindGameObjectWithTag("numberOfBombs").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (player==null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
        }
        stat.fillAmount = (float)player.GetHealth() / 100;
        _numberOfKnives.text = player.GetKnives().ToString();
        _numberOfBombs.text = player.GetBombs().ToString();
    }

}
