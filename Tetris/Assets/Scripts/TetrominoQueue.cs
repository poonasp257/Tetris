using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoQueue : MonoBehaviour {
	private Queue<GameObject> tetrominoQueue;
	private GameObject nextTetromino;

	[SerializeField] private GameObject[] tetrominoPrefabs;
	
	private void Start() {
		tetrominoQueue = new Queue<GameObject>();
	}

	private void Update() {
		if (tetrominoPrefabs.Length <= 0) return;

		if (tetrominoQueue.Count < 4) {
			enqueue(createTetromino());
		}

		if(!nextTetromino) {
			nextTetromino = tetrominoQueue.Peek();
			nextTetromino.SetActive(true);
		}
	}

	private GameObject createTetromino() {
		int randIndex = Random.Range(0, tetrominoPrefabs.Length);
		GameObject tetromino = Instantiate(tetrominoPrefabs[randIndex], this.transform);		

		tetromino.SetActive(false);

		return tetromino;
	}
		 
	private void enqueue(GameObject tetromino) {
		tetrominoQueue.Enqueue(tetromino);
	}

	public GameObject dequeue() {
		if (tetrominoQueue.Count <= 0) return null;

		nextTetromino = null;

		return tetrominoQueue.Dequeue();
	}
}
