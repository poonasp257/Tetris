using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoController : MonoBehaviour {
	private TetrominoManager tetrominoManager;
	private List<Transform> childBlocks;
	
	private float fallCycle;

	private void Start() {
		Initialize();
		
		StartCoroutine("FallCycle");
	}

	private void Update() {
		ProcessInput();
	}

	private void Initialize() {
		tetrominoManager = GameObject.Find("Tetromino Manager").GetComponent<TetrominoManager>();
		childBlocks = new List<Transform>();
		
		fallCycle = 0.5f;

		for (int i = 0; i < transform.childCount; ++i) {
			childBlocks.Add(transform.GetChild(i));
		}
	}

	private void ProcessInput() {
		Vector3 originPosition = transform.position;
		Quaternion originRotation = transform.rotation;

		if(Input.GetKeyDown(KeyCode.LeftArrow)) {
			transform.position += Vector3.left;
		}
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			transform.position += Vector3.right;
		}
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			transform.rotation *= Quaternion.Euler(0, 0, 90.0f);
		}
		if(Input.GetKeyDown(KeyCode.DownArrow)) {
			fallCycle = 0.1f;
		}
		if(Input.GetKeyUp(KeyCode.DownArrow)) {
			fallCycle = 1.0f;
		}
		if (Input.GetKeyDown(KeyCode.Space)) {
			DropTetromino();
			return;
		}

		if (CheckTetromino()) return;

		transform.position = originPosition;
		transform.rotation = originRotation;
	}

	private bool CheckTetromino() {
		foreach (Transform block in childBlocks) {
			int x = Mathf.RoundToInt(block.position.x);
			int y = Mathf.RoundToInt(block.position.y);

			if (tetrominoManager.IsOutOfGrid(x, y)) return false;
			else if (tetrominoManager.FindInBlockMap(x, y)) return false;
		}

		return true;
	}

	private void DropTetromino() {
		while (true) {
			Vector3 originPosition = transform.position;

			transform.position += Vector3.down;

			if(!CheckTetromino()) {
				transform.position = originPosition;
				break;
			}
		}
	}

	IEnumerator FallCycle() {
		while(true) {
			Vector3 originPosition = transform.position;

			transform.position += Vector3.down;
			if (!CheckTetromino()) {
				transform.position = originPosition;
				foreach (Transform block in childBlocks) {
					tetrominoManager.InsertBlock(block.gameObject);
				}
				Destroy(this.gameObject);
				break;
			}

			yield return new WaitForSeconds(fallCycle);
		}
	}
}
