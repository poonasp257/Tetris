using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoManager : MonoBehaviour {
	private Grid grid;
	private TetrominoQueue tetrominoQueue;
	public GameObject currentTetromino;

	private Transform mapNode;
	private GameObject[,] blockMap;
	
	private void Start() {
		Initilaize();
	}

	private void Update() {
		if (!currentTetromino) {			
			currentTetromino = tetrominoQueue.Dequeue();
			currentTetromino.transform.position = new Vector3(-1, -2 + grid.HalfHeight);
			currentTetromino.transform.SetParent(this.transform);
			var controller = currentTetromino.GetComponent<TetrominoController>();
			if (!controller) {
				currentTetromino.AddComponent<TetrominoController>();
			}
		}
	}

	private void Initilaize() {
		grid = GameObject.Find("Grid").GetComponent<Grid>();
		tetrominoQueue = GameObject.Find("Next Tetromino").GetComponent<TetrominoQueue>();
		mapNode = GameObject.Find("Map").transform;

		blockMap = new GameObject[grid.Width, grid.Height];
	}

	public void InsertBlock(GameObject block) {
		int x = Mathf.RoundToInt(block.transform.position.x);
		int y = Mathf.RoundToInt(block.transform.position.y);

		blockMap[x + grid.HalfWidth, y + grid.HalfHeight] = block;
		//block.transform.SetParent(mapNode);
	}

	public bool IsOutOfGrid(int x, int y) {
		if ((x < -grid.HalfWidth || x >= grid.HalfWidth)
			|| (y <= -grid.HalfHeight || y > grid.HalfHeight)) return true;

		return false;
	}

	public bool FindInBlockMap(int x, int y) {
		if (blockMap[x + grid.HalfWidth, y + grid.HalfHeight] == null) {
			return false;
		}

		return true;
	}
}