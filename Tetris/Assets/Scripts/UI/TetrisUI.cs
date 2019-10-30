using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TetrisUI : MonoBehaviour {
	private Text levelText;
	private Text scoreText;
	private Text comboText;
	private TetrominoManager tetrominoManager;

	private void Start() {
		tetrominoManager = GameObject.Find("Tetromino Manager").GetComponent<TetrominoManager>();

		levelText = GameObject.Find("Level").GetComponent<Text>();
		scoreText = GameObject.Find("Score").GetComponent<Text>();
		comboText = GameObject.Find("Combo").GetComponent<Text>();
	}

	private void Update() {
		levelText.text = "Level: " + tetrominoManager.Level;
		scoreText.text = "Score: " + tetrominoManager.Score;
		comboText.text = "Combo: " + tetrominoManager.Combo;
	}
}
