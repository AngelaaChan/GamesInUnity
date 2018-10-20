using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBugMovement : MonoBehaviour {

    //spider is the transform of the spider
    //web is the prefab 
    //webObj is the actual web object that is instantiated when the bug is clicked
    private Transform spider;
    public GameObject web;
    private GameObject webObj;

    private ScoreController scoreController;
    private const int SCORE = 10;

    //speed is the speed at which the bug moves towards the spider
    //GroundBug is the rigid body of the bug
    public float speed = 2f;
    private Rigidbody GroundBug;

    //collided is a bool that is used to know when the bug collides with the spider
    //spiderObj is the actual spider gameObject
    //velocity is used for smoothDamp
    private bool collided;
    public GameObject spiderObj;
    private Vector3 velocity = Vector3.zero;

    //heading and direction of the bug
    private Vector3 heading;
    private Vector3 direction;

    void Start () {
        // Score
        scoreController = GameObject.FindObjectOfType<ScoreController> ();

        //Initializing the objects
        spiderObj = GameObject.Find("Spider");
        spider = spiderObj.transform;
        GroundBug = GetComponent<Rigidbody> ();
        collided = false;
    }

    // Update is called once per frame
    void Update () {
        if (GlobalSetting.isPause) {
            return;
        }
        //Initializing the heading and direction of the bug relative to the spider
        heading = spider.position - GroundBug.position;
        direction = heading / (heading.magnitude);

        //target is used when the bug is following the spider
        Vector3 target = spider.position + direction * 0.7f + Vector3.up * 0.5f;
        //targetCaught is used when the spider catches the bug
        Vector3 targetCaught = spider.position;

        //if the webObj is not initialized yet, the bug follows the spider
        if (webObj == null) {
            //if the distance between the bug and spider is 4f or less, then the bug hits the spider and bounces off
            if (heading.magnitude < 10f && collided == false) {
                GroundBug.MovePosition (Vector3.MoveTowards (transform.position, targetCaught, 15 * Time.deltaTime));
            }
            //If collision has already happened, the bug doesnt move towards the spider until the distance is 5f or more
            else if (collided == true) {
                if (heading.magnitude > 10f) {
                    collided = false;
                }
            }
            //Otherwise, the bug follws the spider with smoothDamp
            else {
                GroundBug.MovePosition (Vector3.SmoothDamp (transform.position, target, ref velocity, speed));
            }
        }

        //if the webObj is initialized then move the bug obj towards the spider and destroy it 
        else if (webObj != null) {
            //spiderObj.GetComponent<Rigidbody>().freezeRotation = true;
            //spiderObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;

            //if the backTrig (is the webObj moving back towards the spider) is false, then just keep moving the bug like before
            if (heading.magnitude > 1f && webObj.GetComponent<WebTrailRenderer> ().backTrig == false) {
                GroundBug.MovePosition (Vector3.SmoothDamp (transform.position, target, ref velocity, speed));

            }
            //if the backTrig is true, then move the bug towards the spider with the same speed as the webObj
            else if (webObj.GetComponent<WebTrailRenderer> ().backTrig == true) {
                GroundBug.MovePosition (Vector3.MoveTowards (GroundBug.position, targetCaught, Time.deltaTime * webObj.GetComponent<WebTrailRenderer> ().web_speed));
                //if the distance between the bug and spider is 1 or less, then destroy both the bug and the webObj
                //if (heading.magnitude < 20f) {
                    Destroy (gameObject);
                    Destroy (webObj);
                //}
            }
        }
        //the bug should always face the spider
        GroundBug.MoveRotation(Quaternion.LookRotation(new Vector3(spider.position.x, 90f, spider.position.z)));
        //GroundBug.MoveRotation (Quaternion.LookRotation (spider.position));
    }

    //When the bug is clicked
    private void OnMouseDown () {
        if (GlobalSetting.isPause) {
            return;
        }
        // deactivae collsion
        this.tag = "Untagged";
        //Create the webObj and set the trig(trigger of starting the web from the spider to the bug) to true
        webObj = Instantiate (web, spider.transform.position, Quaternion.identity);
        webObj.GetComponent<WebTrailRenderer> ().bug = gameObject;
        webObj.GetComponent<WebTrailRenderer> ().trig = true;

        // Increase score
        scoreController.UpdateScore (SCORE);
    }

    //When a collision takes place
    private void OnCollisionStay (Collision collision) {
        if (GlobalSetting.isPause) {
            return;
        }
        //if the bug has collided with the spider, set the collided to true
        if (collision.gameObject.name == "Spider") {
            collided = true;
        }
    }
}