using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

private int totalScore = 0;
    public TextMeshProUGUI ScoreText;

    public void UpdateScore (int score) {
        totalScore += score;
        updateText ();
    }

    private void updateText () {
        ScoreText.text = totalScore.ToString ();
    }
}