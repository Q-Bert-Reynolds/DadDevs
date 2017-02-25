using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public Text scoreText;
    public Text livesText;
    public Text gameOverText;

    private static PlayerController player;
    private bool gameOver;
    private bool restart;
    public int score;

    // Use this for initialization
    void Start () {
        score = 0;
        gameOver = false;
        restart = false;
        livesText.text = "";
        gameOverText.text = "";
        UpdateScore();
        if (player == null)
            player = FindObjectOfType<PlayerController>();
    }

    void Update() {
        livesText.text = "Lives: " + player.lives;
        scoreText.text = "Score: " + score;

        if (player == null) {
            gameOver = true;
        }

        if (restart) {
            if (Input.GetKeyDown(KeyCode.R)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if (gameOver) {
            gameOverText.text = "Game Over - Press 'R' to Restart";
            restart = true;
        }
    }

    public void AddScore(int newScoreValue) {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore() {
        scoreText.text = "Score: " + score;
    }
}
