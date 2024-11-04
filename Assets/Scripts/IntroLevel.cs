using UnityEngine;

public class IntroLevel : MonoBehaviour
{
    public GameObject playerPrefab;
    GameObject player;
    public GameObject UiPrefab;
    GameObject UI;
    void Start()
    {
        player= Instantiate(playerPrefab);
        UI=Instantiate(UiPrefab);
    }

    void Update()
    {
        
    }
}
