using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour {

	public Rigidbody2D rb;
	public Rigidbody2D hook;

	public float releaseTime = 0.15f;
	public float maxDragDistance = 2f;
	public float minDragDistance = 0.4f;

	public GameObject nextBall;
	public Vector2 mousePos;

	private bool isPressed = false;


	void Update() {
		if (isPressed) {
			mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			if (Vector3.Distance(mousePos, hook.position) > maxDragDistance)
				rb.position = hook.position + (mousePos - hook.position).normalized * maxDragDistance;
			else 
				rb.position = mousePos;
		}
	}

	void OnMouseDown() { 
		isPressed = true;
		rb.isKinematic = true; 
	}

	void OnMouseUp() { 
		isPressed = false; 
		rb.isKinematic = false;

		if (Vector3.Distance(mousePos, hook.position) > minDragDistance)
			StartCoroutine(Release());
	}

	IEnumerator Release() {
		yield return new WaitForSeconds(releaseTime);

		GetComponent<SpringJoint2D>().enabled = false;
		this.enabled = false;

		yield return new WaitForSeconds(2f);

		if (nextBall != null) nextBall.SetActive(true);
		else {
			Enemy.EnemiesAlive = 0;
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}
