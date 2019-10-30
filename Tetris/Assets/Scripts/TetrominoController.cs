using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoController : MonoBehaviour {
	private float moveDelay;
	private BlockMap blockMap;

	private void Start() {
		initialize();
	}

	private void Update() {
		processInput();
	}

	private void initialize() {
		blockMap = GameObject.Find("Map").GetComponent<BlockMap>();

		moveDelay = 0.15f;
	}

	private void processInput() {
		if (moveDelay >= 0.15f) {
			if (Input.GetKey(KeyCode.LeftArrow)) {
				if (canMoveTo(Vector3.left)) {
					move(Vector3.left);
					moveDelay = 0;
				}
			}
			else if (Input.GetKey(KeyCode.RightArrow)) {
				if (canMoveTo(Vector3.right)) {
					move(Vector3.right);
					moveDelay = 0;
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			if (canRotate(Quaternion.Euler(0, 0, 90.0f))) {
				rotate(Quaternion.Euler(0, 0, 90.0f));
			}
		}
		
		if (Input.GetKeyDown(KeyCode.Space)) {
			dropTetromino();
		}

		moveDelay += Time.deltaTime;
	}
	
	public void dropTetromino() {
		Vector3 dir = Vector3.down;
		while (canMoveTo(dir)) move(dir);
	}

	public bool canMoveTo(Vector3 dir) {
		for(int i = 0; i < transform.childCount; ++i) {
			Transform block = transform.GetChild(i);
			int x = Mathf.RoundToInt(block.position.x + dir.x);
			int y = Mathf.RoundToInt(block.position.y + dir.y);

			if (blockMap.isOutOfGrid(x, y)) return false;
			else if (blockMap.findBlock(x, y)) return false;
		}
		
		return true;
	}

	public bool canRotate(Quaternion rotation) {
		Quaternion originRotation = transform.rotation;
		transform.rotation *= rotation;

		bool canRotate = true;
		for (int i = 0; i < transform.childCount; ++i) {
			Transform block = transform.GetChild(i);
			int x = Mathf.RoundToInt(block.position.x);
			int y = Mathf.RoundToInt(block.position.y);
			
			if (blockMap.isOutOfGrid(x, y)) {
				canRotate = false;
				break;
			}
			else if (blockMap.findBlock(x, y)) {
				canRotate = false;
				break;
			}
		}

		transform.rotation = originRotation;
		return canRotate;
	}

	public void move(Vector3 dir) {
		transform.position += dir;
	}

	public void rotate(Quaternion rotation) {
		transform.rotation *= rotation;
	}
}
