using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HeathController : MonoBehaviour {

	[Range (100, 200)]
	public int initialHealth = 100;
	private int currentHealth;
	public Text HealthPercentage;
	public Image HealthBar;
	public UnityEvent zeroHealthEvent;

	// Use this for initialization
	void Start () {
		currentHealth = initialHealth;
		updateHealthPercentage ();
		updateHealthBar ();
	}


	public void ApplyDamage (int damage) {
		currentHealth -= damage;
		updateHealthPercentage ();
		updateHealthBar ();
		if (currentHealth <= 0) {
			this.zeroHealthEvent.Invoke ();
		}
	}

	private void updateHealthPercentage () {
		HealthPercentage.text = currentHealth.ToString ();
	}

	private void updateHealthBar () {
		HealthBar.fillAmount = (float)currentHealth / initialHealth;
	}
}