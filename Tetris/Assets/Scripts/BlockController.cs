using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour {
	private GameObject childBlocks;

	private bool isFalling = true;
	private float fallCycle = 1.0f;

	private void Start() {
		StartCoroutine("FallCycle");
	}

	private void Update() {
		ProcessInput();
	}

	private void ProcessInput() {
		if(Input.GetKey(KeyCode.LeftArrow)) {
			MoveBlock(Vector3.left);
		}
		if(Input.GetKey(KeyCode.RightArrow)) {
			MoveBlock(Vector3.right);
		}
		if(Input.GetKeyDown(KeyCode.UpArrow)) {
			RotateBlock();
		}
		if(Input.GetKeyDown(KeyCode.Space)) {
			DropBlock();
		}
	}

	private void MoveBlock(Vector3 dir) {
		dir.Normalize();

		transform.position += dir;
	}

	private void RotateBlock() {
		transform.Rotate(0, 0, -90);
	}

	private void DropBlock() {

	}

	IEnumerator FallCycle() {
		while (isFalling) {
			transform.position += Vector3.down;

			yield return new WaitForSeconds(fallCycle);
		}
	}
}
