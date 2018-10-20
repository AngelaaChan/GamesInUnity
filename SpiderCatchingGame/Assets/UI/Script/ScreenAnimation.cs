using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenAnimation : MonoBehaviour {

	public Material _Screen;
	private float dy = 0.01f;
	private float offset = 0;


	// Update is called once per frame
	void Update () {
		// offset the screen
		offset += dy;
		_Screen.SetTextureOffset ("_texture", Vector2.up * offset);
	}
}