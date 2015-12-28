using UnityEngine;

public class Toolbox : Singleton<Toolbox> {
	protected Toolbox () {} // guarantee this will be always a singleton only - can't use the constructor!

	[HideInInspector] public float hexRatio = Mathf.Sqrt(3f) / 2f;

	void Awake () {
		DontDestroyOnLoad(gameObject);
	}
}
