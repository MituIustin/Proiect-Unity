using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{

    private TextMeshProUGUI _coins;
    private PlayerCombat _player;
    void Start()
    {
        _coins = GameObject.FindGameObjectWithTag("coinsStatus").GetComponent<TextMeshProUGUI>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        _coins.text = _player.GetCoins().ToString() + " coins";
    }
}
