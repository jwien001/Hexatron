using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;

	private static int LEFT 	= -60;
	private static int RIGHT	= 60;

	void Update() {
		Vector3 movement = new Vector3();
		if (Input.GetKey(KeyCode.W))
			movement = transform.forward * speed * Time.deltaTime;
		else if (Input.GetKey(KeyCode.S))
			movement = -1 * transform.forward * speed * Time.deltaTime;
		transform.position = movement + transform.position;

		if (Input.GetKeyDown(KeyCode.A))
			transform.Rotate(new Vector3(0, LEFT, 0));
		else if (Input.GetKeyDown(KeyCode.D))
			transform.Rotate(new Vector3(0, RIGHT, 0));
	}
}