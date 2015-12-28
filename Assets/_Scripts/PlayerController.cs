using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;

	private static int LEFT 	= -60;
	private static int RIGHT	= 60;

	void Update() {
		transform.position += transform.forward * speed * Time.deltaTime;

		if (Input.GetKeyDown(KeyCode.A))
			transform.Rotate(new Vector3(0, LEFT, 0));
		else if (Input.GetKeyDown(KeyCode.D))
			transform.Rotate(new Vector3(0, RIGHT, 0));
	}

	void OnTriggerEnter(Collider other) {
		Vector3 center = other.gameObject.transform.position;
		Debug.Log("Collided with hex centered at (" + center.x + ", " + center.z + ")");

		if (other.gameObject.CompareTag("Wall")) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
}