using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	[HideInInspector] public Color color = Color.red;

	private static float LEFT 	= -60;
	private static float RIGHT	= 60;

	private GameObject currentHex = null;

	void Start() {
		GetComponent<Renderer>().material.color = color;
	}

	void Update() {
		Vector3 newPos = transform.position + (transform.forward * speed * Time.deltaTime);

		if (currentHex != null) {
			float travelDist = Toolbox.DistanceXZ(transform.position, newPos);
			float centerDist = Toolbox.DistanceXZ(transform.position, currentHex.transform.position);

			if (travelDist > centerDist) {
				float turnAngle = GetTurnAngle();
				if (turnAngle != 0) {
					transform.Rotate(new Vector3(0, turnAngle, 0));

					newPos = currentHex.transform.position;
					newPos.y = transform.position.y;
				}

				currentHex = null;
			}
		}

		transform.position = newPos;
	}

	void OnTriggerEnter(Collider other) {
		if (isCollisionFatal(other)) {
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

	bool isCollisionFatal(Collider other) {
		GameObject otherObj = other.gameObject;

		if (otherObj.CompareTag("Wall"))
			return true;

		if (otherObj.CompareTag("Hex") && otherObj.GetComponentInChildren<MoveHex>().IsActivated())
			return true;

		return false;
	}
}