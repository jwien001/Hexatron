using UnityEngine;

public class Toolbox : Singleton<Toolbox> {
	protected Toolbox () {} // guarantee this will be always a singleton only - can't use the constructor!

	[HideInInspector] public float hexRatio = Mathf.Sqrt(3f) / 2f;

	void Awake () {
		DontDestroyOnLoad(gameObject);
	}

	public static float DistanceXZ(Vector3 a3, Vector3 b3) {
		Vector2 a2 = new Vector2(a3.x, a3.z);
		Vector2 b2 = new Vector2(b3.x, b3.z);

		return Vector2.Distance(a2, b2);
	}
}
