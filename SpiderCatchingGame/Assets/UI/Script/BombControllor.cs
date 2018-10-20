using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombControllor : MonoBehaviour {

	// stage 2 material

	// explode on on contact
	void OnCollisionEnter (Collision col) {
		// Play the particle system
	var exp = GetComponent<ParticleSystem> ();
		// Play explosion
		exp.Play ();
		// Set Disappear Effect script on
		GetComponent<DisappearEffect>().enabled = true;
		// Destory the game object
		Destroy (gameObject, exp.main.duration);
	}

}