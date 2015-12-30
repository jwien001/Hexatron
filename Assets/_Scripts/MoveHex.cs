using UnityEngine;
using System.Collections;

public class MoveHex : MonoBehaviour {

	enum State {Inactive, Active, Rise};

	public float maxHeight;
	public float speed;
	public float colorizeDamping;

	private State state = State.Inactive;
	private Color activatedColor;

	void Update () {
		switch (state) {
			case State.Active:
				Colorize();

				break;

			case State.Rise:
				Vector3 pos = transform.GetChild(0).position;
				if (pos.y < maxHeight)
					pos += Vector3.up * speed * Time.deltaTime;

				if (pos.y > maxHeight)
					pos.y = maxHeight;

				transform.GetChild(0).position = pos;

				// Finish colorizing if we didn't already
				Colorize();

				break;
		}
	}

	void Colorize() {
		Color currentColor = GetComponentInChildren<Renderer>().material.color;
		currentColor = Color.Lerp(currentColor, activatedColor, colorizeDamping * Time.deltaTime);
		GetComponentInChildren<Renderer>().material.color = currentColor;
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Player") && state == State.Inactive) {
			activatedColor = other.gameObject.GetComponent<PlayerController>().color;
			state++;
		} else {
			Debug.Log("Hex collided with a non-player object: " + other.gameObject);
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.CompareTag("Player") && state == State.Active) {
			state++;
		}
	}

	public bool IsActivated() {
		return state >= State.Active;
	}
}
