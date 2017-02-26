using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour {

	public static GameUIController instance;

	public Text scoreText;
    public Text livesText;
    public Text gameOverText;
    public Text energyText;

    void Awake () {
    	if (instance == null) {
    		instance = this;
    	}
    }

    void Start () {
        livesText.text = "";
        gameOverText.text = "";
        energyText.text = "";
    }

    public static void ShowGameOver () {
    	instance.gameOverText.text = "Game Over - Press 'R' to Restart";
    }

   	public static void UpdateScore(int score) {
        instance.scoreText.text = "Score: " + score;
    }

   	public static void UpdateLives(int lives) {
        instance.livesText.text = "Lives: " + lives;
    }

   	public static void UpdateEnergy(float energy) {
        instance.energyText.text = "Energy: " + energy;
    }
    
}
