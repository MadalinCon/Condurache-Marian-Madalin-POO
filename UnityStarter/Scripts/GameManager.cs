using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int Score { get; private set; }
    public bool IsPaused => Time.timeScale == 0f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddScore(int points)
    {
        if (points <= 0)
        {
            return;
        }

        Score += points;
        // Hook UI update here if you have one
        // e.g., OnScoreChanged?.Invoke(Score);
    }

    public void ResetScore()
    {
        Score = 0;
    }

    public void RestartScene()
    {
        Time.timeScale = 1f;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }

    public void TogglePause()
    {
        Time.timeScale = IsPaused ? 1f : 0f;
    }
}