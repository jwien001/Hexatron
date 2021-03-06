﻿using UnityEngine;
using System.Collections;

public class Build : MonoBehaviour {

	public int mapSize;
	public Transform hex;
	public float hexYOffset = 0;
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

		float wallHeight = wall.lossyScale.y;

		// Generate the map one tile bigger than requested, making the outermost tiles walls
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

					// Set the scale to twice the spacing so there is no gap between wall pieces.
					// This also allows the walls to scale automatically with the hex object.
					newWall.localScale = new Vector3(
						spacing * 2 * Toolbox.Instance.hexRatio,
						newWall.localScale.y,
						spacing * 2
					);
				} else {
					Instantiate(hex, new Vector3(x, hexYOffset, z), Quaternion.identity);
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
