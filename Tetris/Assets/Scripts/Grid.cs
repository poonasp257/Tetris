using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {
	private GameObject[,] grid;

	[Header("Size Setting")]
	[SerializeField] private int width = 10;
	[SerializeField] private int height = 20;

	[Header("Tile Setting")]
	[SerializeField] private GameObject tilePrefab;

	public int Width { get { return width; } }
	public int Height { get { return height; } }
	public int HalfWidth { get { return width / 2; } }
	public int HalfHeight { get { return height / 2; } }

	private void Start() {
		grid = new GameObject[width, height];
		if(tilePrefab) createTiles();
	}

	private void createTiles() {
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
