using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour {

    //spider is the transform of the spider
    //web is the prefab 
    //webObj is the actual web object that is instantiated when the bug is clicked
    private Transform spider;
    public GameObject web;
    private GameObject webObj;
    private ScoreController scoreController;
    private const int SCORE = 10;

    //speed is the speed at which the bug moves towards the spider
    //bug is the rigid body of the flying bug
    //collided is a bool that is used to know when the bug collides with the spider
    //spiderObj is the actual spider gameObject
    //velocity is used for smoothDamp
    public float speed = 10f;
    private Rigidbody bug;
    private bool collided;
    private GameObject spiderObj;
    private Vector3 velocity = Vector3.zero;

    //heading and direction of the bug
    private Vector3 heading;
    private Vector3 direction;

    void Start () {
        scoreController = GameObject.FindObjectOfType<ScoreController> ();

        //Initializing the objects
        spiderObj = GameObject.Find ("spider");
        spider = GameObject.Find ("spider").transform;
        bug = GetComponent<Rigidbody> ();
        collided = false;
    }

    void Update () {
        //Initializing the heading and direction of the bug relative to the spider
        heading = spider.position - bug.position;
        direction = heading / (heading.magnitude);

        //target is used when the bug is following the spider
        Vector3 target = spider.position + direction * Random.Range (0.2f, 2f) + Vector3.up * Random.Range (0.5f, 2f);
        //targetCaught is used when the spider catches the bug
        Vector3 targetCaught = spider.position;

        //if the webObj is not initialized yet, the bug follows the spider
        if (webObj == null) {
            //if the distance between the bug and spider is 4f or less, then the bug hits the spider and bounces off
            if (heading.magnitude < 4f && collided == false) {
                bug.MovePosition (Vector3.MoveTowards (transform.position, targetCaught, 10 * Time.deltaTime));
            }
            //If collision has already happened, the bug doesnt move towards the spider until the distance is 5f or more
            else if (collided == true) {
                if (heading.magnitude > 5f) {
                    collided = false;
                }
            }
            //Otherwise, the bug follws the spider with smoothDamp
            else {
                bug.MovePosition (Vector3.SmoothDamp (transform.position, target, ref velocity, speed));
            }
        }

        //if the webObj is initialized then move the bug obj towards the spider and destroy it 
        else if (webObj != null) {
            //spiderObj.GetComponent<Rigidbody>().freezeRotation = true;
            //spiderObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;

            //if the backTrig (is the webObj moving back towards the spider) is false, then just keep moving the bug like before
            if (webObj.GetComponent<WebTrailRenderer> ().backTrig == false) {
                bug.MovePosition (Vector3.SmoothDamp (transform.position, target, ref velocity, speed));
            }
            //if the backTrig is true, then move the bug towards the spider with the same speed as the webObj
            else if (webObj.GetComponent<WebTrailRenderer> ().backTrig == true) {
                bug.MovePosition (Vector3.MoveTowards (bug.position, targetCaught, Time.deltaTime * webObj.GetComponent<WebTrailRenderer> ().web_speed));
                //if the distance between the bug and spider is 1 or less, then destroy both the bug and the webObj
                if (heading.magnitude < 1f) {
                    Destroy (gameObject);
                    Destroy (webObj);
                }
            }
        }

        //the bug should always face the spider
        bug.MoveRotation (Quaternion.LookRotation (spider.position));

    }

    //When the bug is clicked
    private void OnMouseDown () {
        if (GlobalSetting.isPause) {
            return;
        }
        //Create the webObj and set the trig(trigger of starting the web from the spider to the bug) to true
        webObj = Instantiate (web, spider.transform.position, Quaternion.identity);
        webObj.GetComponent<WebTrailRenderer> ().bug = gameObject;
        webObj.GetComponent<WebTrailRenderer> ().trig = true;

        // Increase score
        scoreController.UpdateScore (SCORE);
    }

    //When a collisiont takes place
    private void OnCollisionEnter (Collision collision) {
        if (GlobalSetting.isPause) {
            return;
        }
        //if the bug has collided with the spider, set the collided to true
        if (collision.gameObject.name == "spider") {
            collided = true;
        }
    }
}