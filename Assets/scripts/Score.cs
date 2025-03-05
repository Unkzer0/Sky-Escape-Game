using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public int playerScore;
    public int levelHighScore;
    public int levelIndex; // Unique identifier for each level
    TMP_Text scoreText;
    [SerializeField] TextMeshProUGUI highscoreText;

    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "SCORE :";
        levelHighScore = PlayerPrefs.GetInt("Level" + levelIndex + "Highscore", 0); // Load high score for this level
        UpdateHighscoreText();
    }

    public void Updatescore(int hitpoint)
    {
        playerScore += hitpoint;
        scoreText.text = "SCORE : " + playerScore.ToString();
        if (playerScore > levelHighScore)
        {
            levelHighScore = playerScore;
            PlayerPrefs.SetInt("Level" + levelIndex + "Highscore", levelHighScore); // Save high score for this level
            UpdateHighscoreText();
        }
    }

    private void UpdateHighscoreText()
    {
        highscoreText.text = $"HIGHSCORE : {levelHighScore}";
    }
}

