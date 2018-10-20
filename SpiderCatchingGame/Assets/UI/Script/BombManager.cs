using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour {

	[Range (20, 100)]
	public int maximumDuration;
	[Range (20, 40)]
	public int height;
	public BombControllor bombPrefab;
	public GameObject target;
	private int duration;
	private float countDown;

	private Vector3 minPosition;
	private Vector3 scale;

	// Use this for initialization
	void Start () {
		// calcuate the time
		duration = (maximumDuration - GlobalSetting.difficulty);
		countDown = duration;
		// calcuate the position
		minPosition = target.transform.position;
		scale = target.transform.localScale;
	}

	// Update is called once per frame
	void Update () {
		if (GlobalSetting.isPause){
			return;
		}
		if (countDown <= 0) {
			// create bomb
			createBomb ();
			// reset countDown
			countDown = duration + duration * Random.value;
		} else {
			countDown -= Time.deltaTime;
		}
	}

	void createBomb () {
		// create the bomb
		BombControllor p = Instantiate<BombControllor> (bombPrefab);
		// direct the bomb to the top of the computer
		float x = minPosition.x - scale.x / 2  + (scale.x * Random.value);
		float z = minPosition.z - scale.z / 2 + (scale.z * Random.value);
		Vector3 position = new Vector3 (x, height, z);
		p.transform.position = position;
	}
}