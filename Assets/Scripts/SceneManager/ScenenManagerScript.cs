using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagerScript : MonoBehaviour
{

    public void GoToLevel1()
    {
        SceneManager.LoadScene("FirstLevel");
    }
    public void GoToLevel2()
    {
        SceneManager.LoadScene("Level 2");
    }
    public void GoToLevel3()
    {
        SceneManager.LoadScene("Level 3");
    }
    public void GoToFirstMinigame()
    {
        SceneManager.LoadScene("MiniGame");
    }
    public void GoToSecondMinigame()
    {
        SceneManager.LoadScene("Minigame_Dodge");
    }
    public void Test()
    {
        Debug.Log("test");
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void GoToCutScene()
    {
        SceneManager.LoadScene("Level1CutScene");
    }

    public void GoToDeathScreen()
    {
        SceneManager.LoadScene("DeathScreen");
    }
}
