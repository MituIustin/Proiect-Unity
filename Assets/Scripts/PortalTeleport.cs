using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    private SceneManagerScript _sceneManager;
    private void Start()
    {
        var prefab = Resources.Load<GameObject>("SceneManagerObject");
        _sceneManager = prefab.GetComponent<SceneManagerScript>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            _sceneManager.GoToSecondMinigame();
        }
    }
}
