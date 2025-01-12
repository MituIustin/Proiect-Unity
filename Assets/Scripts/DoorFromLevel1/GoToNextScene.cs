using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextScene : MonoBehaviour
{
    bool _GotKey;
    private SceneManagerScript _sceneManager;
    private void Start()
    {
        _GotKey = false;
        var prefab = Resources.Load<GameObject>("SceneManagerObject");
        _sceneManager = prefab.GetComponent<SceneManagerScript>();
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player" && _GotKey)
        {
            _sceneManager.GoToFirstMinigame();
        }
    }

    public void GotTheKey()
    {
        _GotKey = true;
    }
}
