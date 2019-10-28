using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour {
	private bool[,] grid;
	private GameObject[,] backgroundTiles;
	private Queue<GameObject> tetrominoQueue = new Queue<GameObject>();

	private GameObject currentTetromino = null;

	[SerializeField] private GameObject bgTilePrefab;
	[SerializeField] private GameObject[] tetrominoPrefabs;  
	[SerializeField] private int Width = 10;
	[SerializeField] private int Height = 20;

	private void Start() {
		grid = new bool[Width, Height];
		backgroundTiles = new GameObject[Width, Height];

		CreateBackground();
	}

	private void Update() {
		if(tetrominoQueue.Count < 4) {
			CreateTetromino();
		}

		if(!currentTetromino) {
			currentTetromino = tetrominoQueue.Dequeue();
			BlockController controller = currentTetromino.GetComponent<BlockController>();
			if (!controller) {
				Destroy(currentTetromino);
				return;
			}

			currentTetromino.transform.position = new Vector3(0, Height / 2, 0);
			controller.enabled = true;
		}

		if (Input.GetKeyDown(KeyCode.Space)) Destroy(currentTetromino);
	}

	private void CreateBackground() {
		for(int i = 0; i < Width; ++i) {
			for(int j = 0; j < Height; ++j) {
				var tile = Instantiate(bgTilePrefab, this.transform);
				tile.transform.position = new Vector2(i - Width / 2, j - Height / 2);
				backgroundTiles[i, j] = tile;
				
			}
		}
	}

	private void CreateTetromino() {
		int prefabSize = tetrominoPrefabs.Length;
		int randIndex = Random.Range(0, prefabSize);

		GameObject newTetromino = Instantiate(tetrominoPrefabs[randIndex]);
		var controller = newTetromino.GetComponent<BlockController>();
		if (!controller) { 
			controller = newTetromino.AddComponent<BlockController>(); 
		}
		controller.enabled = false;

		newTetromino.transform.position = new Vector3(5 + Width / 2, 0 - 3 * tetrominoQueue.Count);
		tetrominoQueue.Enqueue(newTetromino);
	}

	private GameObject PullTetrominoQueue() {
		for(int i = 0; i < tetrominoQueue.Count; ++i) {
			
			
		}
	}
}
