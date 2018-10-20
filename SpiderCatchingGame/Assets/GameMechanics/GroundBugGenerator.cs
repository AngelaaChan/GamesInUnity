using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBugGenerator : MonoBehaviour {

    //computer is the computer gameObject
    //enemyObject is the prefab of the ground bug
    public GameObject computer;
    public GameObject enemyObject;

    private Vector3 minPosition;
    private Vector3 scale;

    private float waitingTime = 0;
    private float countDown = 0;

    void Start () {
        //Initializing variables
        minPosition = computer.transform.position;
        scale = computer.transform.localScale;
        //Initializing waitingTime
        countDown = 10;
    }

    // // Update is called once per frame
    // void Update () {
    //     if (GlobalSetting.isPause) {
    //         return;
    //     }
    //     // Generate Enemy
    //     countDown -= Time.deltaTime;
    //     if (countDown < 0) {
    //         countDown = waitingTime;
    //         GenerateEnemy ();
    //     }
    // }

    // public void setWaitingTime (float time) {
    //     this.waitingTime = time;
    // }

    public void GenerateEnemy () {
        float x = minPosition.x - scale.x / 2 + (scale.x * Random.value);
        float z = minPosition.z - scale.z / 2 + (scale.z * Random.value);
        //Instatiating the bug at a random position on the computer top
        Vector3 position = new Vector3 (x, GameObject.Find ("Spider").transform.position.y + 5f, z);
        Instantiate (enemyObject, position, Quaternion.Euler (new Vector3 (-90, 0, 0)));
    }
}