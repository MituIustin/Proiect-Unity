using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 0;
    }

    void Update()
    {
        
    }

    public void Resume()
    {
        Time.timeScale = 1;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().SetUI(true);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().SetPauseMenu(false);
        Destroy(gameObject);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
        Destroy(gameObject);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
