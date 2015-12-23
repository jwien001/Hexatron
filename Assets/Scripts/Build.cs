using UnityEngine;
using System.Collections;

public class Build : MonoBehaviour {

	public int mapSize;
	public Transform hex;

	void Awake() {
		/**
		 * All hex math sourced from http://www.redblobgames.com/grids/hexagons/
		 **/

		// The axial conversion uses the "radius" to space out the hexes,
		// which is half the width (equates to scale) of the hexagon in the z-axis.
		float size = hex.lossyScale.z / 2f;

		for (int q = -mapSize; q <= mapSize; q++) {
			int r1 = Mathf.Max(-mapSize, -q - mapSize);
			int r2 = Mathf.Min(mapSize, -q + mapSize);
			for (int r = r1; r <= r2; r++) {
				// Convert axial coordinates to pixels
				float x = size * Mathf.Sqrt(3f) * (q + r / 2f);
				float z = size * 3f / 2f * r;

				Instantiate(hex, new Vector3(x, 0, z), Quaternion.identity);
			}
		}
	}
}
