using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menuPrefab;
    GameObject menu;
    private SceneManagerScript _sceneManager;
    void Start()
    {
        menu=Instantiate(menuPrefab);
        var prefab = Resources.Load<GameObject>("SceneManagerObject");
        _sceneManager = prefab.GetComponent<SceneManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        var prefab = Resources.Load<GameObject>("SceneManagerObject");
        _sceneManager = prefab.GetComponent<SceneManagerScript>();
        _sceneManager.GoToLevel1();
    }

    public void Options()
    {
        SceneManager.LoadScene("Options");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GotoMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
