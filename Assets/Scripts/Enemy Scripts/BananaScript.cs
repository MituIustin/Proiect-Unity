using UnityEngine;

public class BananaScript : MonoBehaviour
{
    private SceneManagerScript _sceneManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var prefab = Resources.Load<GameObject>("SceneManagerObject");
            if (prefab != null)
            {
                _sceneManager = prefab.GetComponent<SceneManagerScript>();
                if (_sceneManager != null)
                {
                    _sceneManager.GoToFinalCutScene();
                }
            }

            Destroy(gameObject);
        }
    }
}