using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;

	private static float LEFT 	= -60;
	private static float RIGHT	= 60;

	private GameObject currentHex = null;

	void Update() {
		Vector3 newPos = transform.position + (transform.forward * speed * Time.deltaTime);

		if (currentHex != null) {
			float travelDist = Toolbox.DistanceXZ(transform.position, newPos);
			float centerDist = Toolbox.DistanceXZ(transform.position, currentHex.transform.position);
			float turnAngle = GetTurnAngle();

			if (travelDist > centerDist && turnAngle != 0) {
				transform.Rotate(new Vector3(0, turnAngle, 0));

				newPos = currentHex.transform.position;
				newPos.y = transform.position.y;

				currentHex = null;
			}
		}

		transform.position = newPos;
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Wall")) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		if (other.gameObject.CompareTag("Hex")) {
			currentHex = other.gameObject;
		}
	}

	float GetTurnAngle() {
		if (Input.GetKey(KeyCode.A))
			return LEFT;

		if (Input.GetKey(KeyCode.D))
			return RIGHT;

		return 0;
	}
}