using UnityEngine;

public class IntroLevel : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject camera;
    GameObject player;
    public GameObject UiPrefab;
    GameObject UI;
    void Start()
    {
       // player= Instantiate(playerPrefab);
        Instantiate(enemy1Prefab);
        Instantiate(enemy2Prefab);
       // Instantiate(camera);
        UI = Instantiate(UiPrefab);
    }

    void Update()
    {
        
    }
}
