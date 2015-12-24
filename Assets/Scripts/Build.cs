using UnityEngine;
using System.Collections;

public class Build : MonoBehaviour {

	public int mapSize;
	public Transform hex;
	public float hexGap;
	public Transform wall;
	public Transform underglow;

	void Awake() {
		/**
		 * All hex math sourced from http://www.redblobgames.com/grids/hexagons/
		 **/

		// The axial conversion uses the "radius" to space out the hexes,
		// which is half the width (equates to scale) of the hexagon in the z-axis.
		float spacing = (hex.lossyScale.z / 2f) + hexGap;

		float hexHeight = hex.lossyScale.y;
		float wallHeight = wall.lossyScale.y;

		int mapSizeWithWall = mapSize + 1;
		for (int q = -mapSizeWithWall; q <= mapSizeWithWall; q++) {
			int r1 = Mathf.Max(-mapSizeWithWall, -q - mapSizeWithWall);
			int r2 = Mathf.Min(mapSizeWithWall, -q + mapSizeWithWall);
			for (int r = r1; r <= r2; r++) {
				int s = -q - r;

				// Convert axial coordinates to pixels
				float x = spacing * Mathf.Sqrt(3f) * (q + r / 2f);
				float z = spacing * 3f / 2f * r;

				if (IsEdge(q, r, s, mapSizeWithWall)) {
					Transform newWall = (Transform) Instantiate(wall, new Vector3(x, wallHeight / 2f, z), Quaternion.identity);
					newWall.localScale += new Vector3(hexGap * Toolbox.Instance.hexRatio * 2, 0, hexGap * 2);
				} else {
					Instantiate(hex, new Vector3(x, -1 * hexHeight / 2f, z), Quaternion.identity);
				}
			}
		}

		// We multiply by 4 here because both spacing and mapSize are radii,
		// but scale is a diameter, so we double it twice.
		float underglowSize = spacing * mapSize * 4;
		underglow.transform.localScale = new Vector3(
			underglowSize,
			underglowSize * Toolbox.Instance.hexRatio,
			1
		);
	}

	private bool IsEdge(int q, int r, int s, int edge) {
		return Mathf.Abs(q) == edge || Mathf.Abs(r) == edge || Mathf.Abs(s) == edge;
	}
}
