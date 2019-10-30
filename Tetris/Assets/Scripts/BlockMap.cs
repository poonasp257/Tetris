using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMap : MonoBehaviour {
	private Grid grid;
	private TetrominoManager tetrominoManager;
	private List<GameObject>[] map;

	private void Start() {
		grid = GameObject.Find("Grid").GetComponent<Grid>();
		tetrominoManager = GameObject.Find("Tetromino Manager").GetComponent<TetrominoManager>();

		map = new List<GameObject>[grid.Height];
		for (int row = 0; row < map.Length; ++row) {
			map[row] = new List<GameObject>(10);
			for (int col = 0; col < grid.Width; ++col) {
				map[row].Add(null);
			}
		}
	}
	
	private void deleteRow(int row) {
		for(int col = 0; col < map[row].Capacity; ++col) {
			Destroy(map[row][col]);
			map[row][col] = null;
		}
	}

	private void pullRow(int deletedRow) {
		for (int row = deletedRow; row < map.Length - 1; ++row) {
			for(int col = 0; col < map[row].Capacity; ++col) {
				if (map[row + 1][col] == null) continue;

				map[row][col] = map[row + 1][col];
				map[row + 1][col] = null;
				map[row][col].transform.position += Vector3.down;
			}
		}
	}

	private void updateMap() {
		bool isDeleted = false;
		for (int row = 0; row < map.Length; ++row) {
			bool condition = map[row].TrueForAll((block) => {
				return block != null;
			});

			if(condition) {
				deleteRow(row);
				pullRow(row);
				--row;

				if (!isDeleted) {
					isDeleted = true;
					++tetrominoManager.Combo;
				}

				tetrominoManager.plusScore();
			}	
		}

		if (!isDeleted) tetrominoManager.Combo = 0;
	}

	private void insertBlock(GameObject block) {
		int x = Mathf.RoundToInt(block.transform.position.x);
		int y = Mathf.RoundToInt(block.transform.position.y);

		map[y + grid.HalfHeight][x + grid.HalfWidth] = block;
		block.transform.SetParent(this.transform);
	}
	
	public void insertTetromino(GameObject tetromino) {
		Transform tetrominoNode = tetromino.transform;
		int childCount = tetrominoNode.childCount;

		Transform[] childBlocks = new Transform[childCount];
		for(int i = 0; i < childCount; ++i) {
			childBlocks[i] = tetrominoNode.GetChild(i);
		}

		for (int i = 0; i < childCount; ++i) {
			insertBlock(childBlocks[i].gameObject);
		}

		updateMap();
	}

	public void DeleteAllBlock() {
		for(int row = 0; row < map.Length; ++row) {
			deleteRow(row);
		}
	}

	public bool isOutOfGrid(int x, int y) {
		if ((x < -grid.HalfWidth || x >= grid.HalfWidth)
			|| (y < -grid.HalfHeight || y >= grid.HalfHeight)) return true;
			
		return false;
	}

	public bool findBlock(int x, int y) {
		if (map[y + grid.HalfHeight][x + grid.HalfWidth] == null) {
			return false;
		}

		return true;
	}
}
