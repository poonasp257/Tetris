using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboUI : MonoBehaviour {
	private int combo;

	private Text comboText;
	private TetrominoManager tetrominoManager;

	private void Start() {
		comboText = GetComponent<Text>();
		tetrominoManager = GameObject.Find("Tetromino Manager").GetComponent<TetrominoManager>();
	}

	private void Update() {
		if (!comboText) {
			comboText = this.gameObject.AddComponent<Text>();
		}

		combo = tetrominoManager.Combo;
		comboText.text = "Combo: " + combo;
	}
}
