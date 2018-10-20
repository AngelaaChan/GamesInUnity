using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebTrailRenderer : MonoBehaviour {

    //spider is the spider gameObject
    //bug is the bug gameObject
    //trig is the trigger when the webObj should move towards the bug
    //backTrig is the trigger when the webObj should move back towards the spider
    //webRigid is the rigidBody of the Web gameObject
    //bugRigid is the rigidVody of the bug gameObject
    //webSpeed is the speed at which the webObj moves
    public GameObject spider;
    public GameObject bug;
    public bool trig;
    public bool backTrig;
    private Rigidbody webRigid;
    private Rigidbody bugRigid;
    public float web_speed = 20f;

    //webHeading is the distance between the bug and the web object
    Vector3 webHeading;

	void Start () {
        //Initializing the variables
        spider = GameObject.Find("Spider");
        webRigid = GetComponent<Rigidbody>();
        //initialize the position of the gameObject to the position of the spider
        transform.position = spider.transform.position;
        //Disabling the trailRenderer at the start
        this.GetComponent<TrailRenderer>().enabled = false;
        backTrig = false;

	}
	
	// Update is called once per frame
	void Update () {

        //if there is a bug thats been caught then,
        if (bug != null)
        {
            bugRigid = bug.GetComponent<Rigidbody>();
            if (trig == true)
            {
                //Enabling the TrailRenderer
                this.GetComponent<TrailRenderer>().enabled = true;
                WebRendererTowardsBug();
            }

            else
            {
                //Keep moving with the spider if trig is false
                transform.position = spider.transform.position;
                this.GetComponent<TrailRenderer>().enabled = false;
            }
        }

        //Keep moving with the spider if bug is not caught yet
        else {
            transform.position = spider.transform.position;
            trig = false;
            backTrig = false;
            Destroy(gameObject);
        }
	}

    //This function moves the web towards the bug and renders it
    private void WebRendererTowardsBug() {
        webHeading = bugRigid.position - webRigid.position;

        Vector3 targetPos = bugRigid.position;
        //Moving the web towards the bug
        if (webHeading.magnitude > 1f && backTrig==false)
        {
            webRigid.MovePosition(Vector3.MoveTowards(webRigid.position, targetPos, web_speed * Time.deltaTime));

        }
        //When the web reaches the bug, call WebRendererTowardsSpider()
        else
        {
            backTrig = true;
            //Disabling the TrailRenderer
            this.GetComponent<TrailRenderer>().enabled = false;
            WebRenderedTowardsSpider();
        }
    }

    //This function moves the web towards the spider
    private void WebRenderedTowardsSpider() {

        webHeading = spider.transform.position - webRigid.position;

        Vector3 targetPos = spider.transform.position;
        //Moving the web towards the spider
        if (webHeading.magnitude > 1f)
        {
            webRigid.MovePosition(Vector3.MoveTowards(webRigid.position, targetPos, web_speed * Time.deltaTime));
        }
        //When the web reached the spider, set backTrig and trig to false
        else 
        {
            backTrig = false;
            trig = false;
        }
    }
}
