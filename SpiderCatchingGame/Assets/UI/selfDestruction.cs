using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfDestruction : MonoBehaviour {

	private Animator animator;
	// destroy after animation
	void Start () {
		animator = this.GetComponent<Animator> ();
		AnimatorClipInfo[] info = animator.GetCurrentAnimatorClipInfo (0);
		Destroy (gameObject, info[0].clip.length * 3);
	}

	// Update is called once per frame
	void Update () {

	}
}