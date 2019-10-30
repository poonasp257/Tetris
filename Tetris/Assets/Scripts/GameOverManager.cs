using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {
	public void OnRestart() {
		SceneManager.LoadScene("Main");
	}

	public void OnExit() {
		Application.Quit();
	}
}
