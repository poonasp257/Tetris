using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoManager : MonoBehaviour {
	private Grid grid;
	private TetrominoQueue tetrominoQueue;
	private BlockMap blockMap;

	private Vector2 spawnPoint; 		
	private GameObject currentTetromino;
	private TetrominoController tetrominoController;

	private float fallCycle;
	private float defaultFallCycle;

	public bool IsGameOver { get; private set; }
	public int Level { get; private set; }
	public int Score { get; private set; }
	public int Combo { get; set; }
	   
	private void Start() {
		initilaize();

		StartCoroutine("FallCycle");
	}

	private void Update() {
		IsGameOver = canSpawnTetromino();
		if (IsGameOver) return;

		if (!currentTetromino) setupTetromino();
		else {
			if (!tetrominoController.canMoveTo(Vector3.down)) {
				blockMap.insertTetromino(currentTetromino);
				Destroy(currentTetromino);
				currentTetromino = null;
			}

		}

		if(Input.GetKeyDown(KeyCode.DownArrow)) {
			fallCycle *= 0.1f;
		}
		if(Input.GetKeyUp(KeyCode.DownArrow)) {
			fallCycle = defaultFallCycle;
		}

		if (Score >= 1500 * Level) NextLevel();
	}

	private void initilaize() {
		grid = GameObject.Find("Map/Grid").GetComponent<Grid>();
		tetrominoQueue = GameObject.Find("Next Tetromino").GetComponent<TetrominoQueue>();
		blockMap = GameObject.Find("Map").GetComponent<BlockMap>();

		IsGameOver = false;

		spawnPoint = new Vector2(-1, -2 + grid.HalfHeight);
		currentTetromino = null;

		defaultFallCycle = 1.0f;
		fallCycle = defaultFallCycle;

		Level = 1;
		Score = 0;
		Combo = 0;
	}
	
	private bool canSpawnTetromino() {
		int x = Mathf.RoundToInt(spawnPoint.x);
		int y = Mathf.RoundToInt(spawnPoint.y);

		return blockMap.findBlock(x, y);
	}

	private void setupTetromino() {
		currentTetromino = tetrominoQueue.dequeue();
		currentTetromino.transform.SetParent(this.transform);
		currentTetromino.transform.position = spawnPoint;
		
		tetrominoController = currentTetromino.GetComponent<TetrominoController>();
		if (!tetrominoController) {
			tetrominoController = currentTetromino.AddComponent<TetrominoController>();
		}
	}

	private void NextLevel() {
		blockMap.DeleteAllBlock(); 
		++Level;
		defaultFallCycle -= 0.2f * Level;
		fallCycle = defaultFallCycle;
	}
	
	public void plusScore() {
		Score += 100 * Combo;
	}

	IEnumerator FallCycle() {
		while (true) {
			if (currentTetromino) { 
				if (tetrominoController.canMoveTo(Vector3.down)) {
					tetrominoController.move(Vector3.down);				
				}
			}

			yield return new WaitForSeconds(fallCycle);
		}
	}
}