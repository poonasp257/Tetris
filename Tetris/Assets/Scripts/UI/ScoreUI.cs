using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {
	private int score;

	private Text scoreText;
	private TetrominoManager tetrominoManager;

	private void Start() {
		scoreText = GetComponent<Text>();
		tetrominoManager = GameObject.Find("Tetromino Manager").GetComponent<TetrominoManager>();
	}	

	private void Update() {
		if (!scoreText) {
			scoreText = this.gameObject.AddComponent<Text>();
		}

		score = tetrominoManager.Score;
		scoreText.text = "Score: " + score;
	}
}
