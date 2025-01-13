using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text timerText;
    public GameObject gameOverUI; 
    public float totalTime = 30f;

    private float timeRemaining;
    private bool isGameOver = false;

    void Start()
    {
        timeRemaining = totalTime;
        UpdateTimerUI();
        gameOverUI.SetActive(false); 
    }

    void Update()
    {
        if (isGameOver) return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                EndGame();
            }
            UpdateTimerUI();
        }
    }

    void UpdateTimerUI()
    {
        int secondsPassed = Mathf.FloorToInt(totalTime - timeRemaining);
        int secondsRemaining = Mathf.FloorToInt(timeRemaining);
        timerText.text = $"Survived: {secondsPassed}s\nRemaining: {secondsRemaining}s";
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverUI.SetActive(true); 
        Time.timeScale = 0;
    }

    public void RetryGame()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    void EndGame()
    {
        var prefab = Resources.Load<GameObject>("SceneManagerObject");
        prefab.GetComponent<SceneManagerScript>().GoToLevel3();
    }
}
