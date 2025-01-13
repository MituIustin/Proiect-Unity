using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    
    SceneManagerScript _sceneManagerScript;
    void Start()
    {

        _sceneManagerScript = Resources.Load<GameObject>("SceneManagerObject").GetComponent<SceneManagerScript>();
    }

    
    void Update()
    {
        
    }

    public void Retry()
    {
        _sceneManagerScript.GoToLevel1();
    }
    public void Menu()
    {
        _sceneManagerScript.GoToMenu();
    }

    public void QuitApp()
    {
        Application.Quit();
    }


}
