using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour {

	public float health = 4f;
	public GameObject deathEffect;

	public static int EnemiesAlive = 0;

	void Start() {
		EnemiesAlive++;
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.relativeVelocity.magnitude > health) Die();
	}

	void Die() {
		GameObject effect = (GameObject) Instantiate(deathEffect, transform.position, Quaternion.identity);

		EnemiesAlive--;
		if (EnemiesAlive <= 0) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

		Destroy(effect, 2f);
		Destroy(gameObject);
	}
}
