using UnityEngine;

public class DeathScreenInstantiate : MonoBehaviour
{
    GameObject _deathScreen;
    void Start()
    {
        var deathscreen = Resources.Load<GameObject>("DeathScreen");
        _deathScreen = Instantiate(deathscreen);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
