using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour {
	private GameObject blockManager;
	private GameObject[] childBlocks;
	private Vector3 pivot;

	private bool isFalling = true;
	private float fallCycle = 1.0f;

	private void Start() {
		Initialize();

		//StartCoroutine("FallCycle");
	}

	private void Update() {
		ProcessInput();
	}

	private void Initialize() {
		blockManager = GameObject.Find("Block Manager");
		childBlocks = new GameObject[transform.childCount];

		for(int i = 0; i < transform.childCount; ++i) {
			childBlocks[i] = transform.GetChild(i).gameObject;
		}
		
		pivot = Vector3.zero;

		foreach (GameObject child in childBlocks) {
			pivot += child.transform.position;
		}

		pivot /= transform.childCount;
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
		transform.Rotate(0, 0, -90, Space.Self);
		//transform.RotateAround(pivot, Vector3.forward, -90f);
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
