using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugGeneration : MonoBehaviour {

    //enemyObject is the prefab of the flying bug
    //Counter is the the time used to increase level of difficulty with time
    public GameObject enemyObject;

    //This is the gameObject that is instantiated when GenerateEnemy() is called
    private GameObject bugPrefab;

    // waiting timer to generate Enemy
    private float waitingTime = 0;
    private float countDown = 0;

    // level counter
    public float levelSpeed = 100f;

    void Start () {
        //Initializing waitingTime
        countDown = 10;
    }

    // void Update () {
    //     if (GlobalSetting.isPause) {
    //         return;
    //     }
    //     // Generate Enemy
    //     countDown += Time.deltaTime;
    //     if (countDown > waitingTime) {
    //         countDown = 0;
    //         GenerateEnemy ();
    //     }
    // }

    // public void setWaitingTime (float time) {
    //     this.waitingTime = time;
    // }

    public void GenerateEnemy () {
        float x_cord = Random.Range (-30, 30);
        float z_cord = Random.Range (-30, 30);
        float y_cord = Random.Range (Camera.main.transform.position.y, 75);
        //Instatiating the bug at a Random position above the camera position
        Vector3 pos = new Vector3 (x_cord, y_cord, z_cord);
        bugPrefab = Instantiate (enemyObject, pos, Quaternion.identity);
    }
}