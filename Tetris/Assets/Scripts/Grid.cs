using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Grid : MonoBehaviour {
	private GameObject[,] grid;

	[SerializeField] private int width = 10;
	[SerializeField] private int height = 20;

	[SerializeField] private GameObject tilePrefab;

	public int Width { get { return width; } }
	public int Height { get { return height; } }
	public int HalfWidth { get { return width / 2; } }
	public int HalfHeight { get { return height / 2; } }

	private void Start() {
		grid = new GameObject[width, height];

		CreateTiles();
	}

	private void CreateTiles() {
		if (!tilePrefab) return;

		for (int i = 0; i < width; ++i) {
			for (int j = 0; j < height; ++j) {
				var tile = Instantiate(tilePrefab, this.transform);
				tile.transform.position = this.transform.position + new Vector3(
					i - HalfWidth, j - HalfHeight, 0);
				grid[i, j] = tile;
			}
		}
	}

}
