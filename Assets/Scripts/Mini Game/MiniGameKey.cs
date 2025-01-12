using UnityEngine;

public class MiniGameKey : MonoBehaviour
{
    private SceneManagerScript _sceneManager;

    private void Start()
    {
        var prefab = Resources.Load<GameObject>("SceneManagerObject");
        _sceneManager = prefab.GetComponent<SceneManagerScript>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            _sceneManager.GoToLevel2();
        }
    }
}
