using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public SpawnerEnemy[] spawner;

    private static PlayerController player;
    private bool gameOver;
    private bool restart;
    private float deltaGametime = 0;
    public int score;

    // Use this for initialization
    void Start () {
        score = 0;
        gameOver = false;
        restart = false;
        GameUIController.UpdateScore(0);
        if (player == null)
            player = FindObjectOfType<PlayerController>();
    }

    void Update() {
        GameUIController.UpdateLives(player.lives);
        GameUIController.UpdateScore(score);
        GameUIController.UpdateEnergy(player.energy);

        if (!gameOver) {
            deltaGametime += Time.deltaTime;

            if (deltaGametime > 1) {
                if (!spawner[0].gameObject.activeInHierarchy) spawner[0].gameObject.SetActive(true);
                if (!spawner[1].gameObject.activeInHierarchy) spawner[1].gameObject.SetActive(true);
            }
            if (deltaGametime > 16) {
                if (!spawner[2].gameObject.activeInHierarchy) spawner[2].gameObject.SetActive(true);
                if (!spawner[3].gameObject.activeInHierarchy) spawner[3].gameObject.SetActive(true);
            }
            if (deltaGametime > 46) {
                if (!spawner[4].gameObject.activeInHierarchy) spawner[4].gameObject.SetActive(true);
                if (!spawner[5].gameObject.activeInHierarchy) spawner[5].gameObject.SetActive(true);
            }
            if (deltaGametime > 91) {
                if (!spawner[6].gameObject.activeInHierarchy) spawner[6].gameObject.SetActive(true);
                if (!spawner[7].gameObject.activeInHierarchy) spawner[7].gameObject.SetActive(true);
            }
        }

        if (!player.gameObject.activeSelf) {
            gameOver = true;
        }

        if (restart) {
            if (Input.GetKeyDown(KeyCode.R)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if (gameOver) {
            GameUIController.ShowGameOver();
            restart = true;
        }
    }

    public void AddScore(int newScoreValue) {
        score += newScoreValue;
        GameUIController.UpdateScore(score);
    }
}
