using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menuPrefab;
    GameObject menu;
    void Start()
    {
        menu=Instantiate(menuPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
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
