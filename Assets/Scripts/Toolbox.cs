using UnityEngine;

public class Toolbox : Singleton<Toolbox> {
	protected Toolbox () {} // guarantee this will be always a singleton only - can't use the constructor!

	public bool savePPMeshes = false;

	void Awake () {
		// Your initialization code here
	}
}
