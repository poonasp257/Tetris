using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoManager : MonoBehaviour {
	private Grid grid;
	private TetrominoQueue tetrominoQueue;
	public GameObject currentTetromino;

	private Transform mapNode;
	private GameObject[,] blockMap;

	public int Score { get; private set; }
	public int Combo { get; private set; }

	private void Start() {
		Initilaize();
	}

	private void Update() {
		if (!currentTetromino) {			
			currentTetromino = tetrominoQueue.Dequeue();
			currentTetromino.transform.SetParent(this.transform);
			currentTetromino.transform.position = new Vector3(-1.0f, -2 + grid.HalfHeight, 0);

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

		Score = 0;
		Combo = 0;
	}

	private void DeleteLine(int row) {
		for (int col = 0; col < blockMap.GetLength(0); ++col) {
			Destroy(blockMap[col, row]);
			blockMap[col, row] = null;
		}
	}

	private void PullLine(int deletedLine) {
		for (int row = deletedLine; row < blockMap.GetLength(1) - 1; ++row) { 
			for (int col = 0; col < blockMap.GetLength(0); ++col) {
				if (blockMap[col, row + 1] == null) continue;

				blockMap[col, row] = blockMap[col, row + 1];
				blockMap[col, row + 1] = null;
				blockMap[col, row].transform.position += Vector3.down;
			}
		}
	}
	
	public void CheckMap() {
		bool success = false;
		for (int row = 0; row < blockMap.GetLength(1); ++row) {
			int count = 0;
			for (int col = 0; col < blockMap.GetLength(0); ++col) {
				if (blockMap[col, row] != null) ++count;
			}

			if (count == grid.Width) {
				DeleteLine(row);
				PullLine(row);
				--row;
				if (!success) {
					success = true;
					++Combo;
				}
				Score += 100 * Combo;
			}
		}
		if (!success) Combo = 0;
	}

	public void InsertBlock(GameObject block) {
		int x = Mathf.RoundToInt(block.transform.position.x);
		int y = Mathf.RoundToInt(block.transform.position.y);

		blockMap[x + grid.HalfWidth, y + grid.HalfHeight] = block;
		block.transform.SetParent(mapNode);
	}

	public bool IsOutOfGrid(int x, int y) {
		if ((x < -grid.HalfWidth || x >= grid.HalfWidth)
			|| (y < -grid.HalfHeight || y > grid.HalfHeight)) return true;

		return false;
	}

	public bool FindInBlockMap(int x, int y) {
		if (blockMap[x + grid.HalfWidth, y + grid.HalfHeight] == null) {
			return false;
		}

		return true;
	}
}