using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	[HideInInspector] public Color color = Color.red;

	private static float LEFT 	= -60;
	private static float RIGHT	= 60;

	private float turnAngle = 0;
	private GameObject currentHex = null;

	void Start() {
		GetComponent<Renderer>().material.color = color;
	}

	void Update() {
		UpdateInput();

		Vector3 newPos = transform.position + (transform.forward * speed * Time.deltaTime);

		if (currentHex != null) {
			float travelDist = Toolbox.DistanceXZ(transform.position, newPos);
			float centerDist = Toolbox.DistanceXZ(transform.position, currentHex.transform.position);

			if (travelDist > centerDist) {
				if (turnAngle != 0) {
					transform.Rotate(new Vector3(0, turnAngle, 0));

					newPos = currentHex.transform.position;
					newPos.y = transform.position.y;

					turnAngle = 0;
				}

				currentHex = null;
			}
		}

		transform.position = newPos;
	}

	void UpdateInput() {
		if (Input.GetKeyDown(KeyCode.A))
			turnAngle = LEFT;

		if (Input.GetKeyDown(KeyCode.D))
			turnAngle = RIGHT;
	}

	void OnTriggerEnter(Collider other) {
		if (isCollisionFatal(other)) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}

		if (other.gameObject.CompareTag("Hex")) {
			currentHex = other.gameObject;
		}
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