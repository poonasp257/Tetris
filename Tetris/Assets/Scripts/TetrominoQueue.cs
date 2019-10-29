using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoQueue : MonoBehaviour {
	private Grid grid;
	private Queue<GameObject> tetrominoQueue;
	private GameObject nextTetromino;

	[SerializeField] private GameObject[] tetrominoPrefabs = new GameObject[7];
	
	private void Start() {
		grid = GameObject.Find("Grid").GetComponent<Grid>();
		tetrominoQueue = new Queue<GameObject>();
	}

	private void Update() {
		if (tetrominoPrefabs.Length <= 0 || !grid) return;

		if (tetrominoQueue.Count < 4) {
			GameObject newTetromino = CreateTetromino();
			Enqueue(newTetromino);
		}

		if(!nextTetromino) {
			nextTetromino = tetrominoQueue.Peek();
			nextTetromino.SetActive(true);
		}
	}

	private GameObject CreateTetromino() {
		int randIndex = Random.Range(0, tetrominoPrefabs.Length);
		GameObject tetromino = Instantiate(tetrominoPrefabs[randIndex], this.transform);		
		
		tetromino.SetActive(false);

		return tetromino;
	}
		 
	private void Enqueue(GameObject tetromino) {
		tetrominoQueue.Enqueue(tetromino);
	}

	public GameObject Dequeue() {
		if (tetrominoQueue.Count <= 0) return null;

		nextTetromino = null;

		return tetrominoQueue.Dequeue();
	}
}
