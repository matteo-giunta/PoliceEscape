using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;
    public int highScore = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public GameObject gameOverPanel;
    public TextMeshProUGUI timeText;
    private float elapsedTime = 0f;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI finalTimeText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Time.timeScale = 1f;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateScoreUI();
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        UpdateTimeUI();
    }

    void UpdateTimeUI()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        timeText.text = $"TIME: {minutes:00}:{seconds:00}";
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;

        // Ferma tutti i suoni del gioco
        AudioSource[] allAudio = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in allAudio)
        {
            audio.Stop();
        }

        // Suono GAME OVER
        GetComponents<AudioSource>()[1].Play();

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        finalScoreText.text = "SCORE: " + score.ToString("D3");
        highScoreText.text = "BEST: " + highScore.ToString("D3");
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        finalTimeText.text = $"TIME: {minutes:00}:{seconds:00}";
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("IntroScene");
    }
}