using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameStatus : MonoBehaviour
{
    [Range(0.1f, 10f)][SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointPerBLockDestroyed = 1;
    [SerializeField] int currentScore = 0;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI ballQtyText;
    [SerializeField] TextMeshProUGUI currentLevelText;
    [SerializeField] bool autlyPlay;
    public int ballQty;

    Level level;
    SceneLoader sceneLoader;

    void Start()
    {
        scoreText.text = "Scores: " + currentScore.ToString();
        ballQtyText.text = "x" + ballQty;
        currentLevelText.text = "Current Leve: " + level.currentLevel;
    }

    private void Awake()
    {
        level = FindObjectOfType<Level>();
        int gameStateCount = FindObjectsOfType<GameStatus>().Length;
        if (gameStateCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void AddToScore()
    {
        currentScore = currentScore + pointPerBLockDestroyed;
        scoreText.text = "Scores: " + currentScore.ToString();
    }
    public void LevelUpdate()
    {
        currentLevelText.text = "Current Leve: " + level.currentLevel;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public bool IsAutoPlayEnabled()
    {
        return autlyPlay;
    }
    public void BallDestroyed()
    {
        ballQty--;
        ballQtyText.text = "x" + ballQty;

        if (ballQty <= 0)
        {
            sceneLoader.LoadResultScene();
        }
    }
}
