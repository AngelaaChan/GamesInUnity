using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpyderController : MonoBehaviour {

    //lookSpeed is the speed at which the player turns
    //speed is the speed at which the player moves
    //curLoc is the current location of the spider
    //prevLoc is the last location of the spider
    //player is the rigidBody attached to the spider gameObject
    //anim is the animator that controls the animation of the spider walking
    public float lookSpeed = 10;
    public float speed = 10;
    private Vector3 curLoc;
    private Vector3 prevLoc;
    private Rigidbody player;
    private Animator anim;
    public HeathController healthBar;

    // On collision 

    void OnCollisionStay (Collision colide) {
        if (GlobalSetting.isPause) {
            return;
        }
        if (colide.gameObject.tag == "Bug") {
            healthBar.ApplyDamage (1);
        }
        if (colide.gameObject.tag == "Bomb") {
            healthBar.ApplyDamage (1);
        }
    }

    void Start () {

        //Initializing the variables
        player = GetComponent<Rigidbody> ();
        anim = GetComponent<Animator> ();
    }

    void Update () {
        if (GlobalSetting.isPause) {
            return;
        }
        //Updating the prevLoc to the curLoc every update
        prevLoc = curLoc;

        //Took this code from one of the first workshops
        //Just basic movement of the spider
        float dx = 0.0f, dz = 0.0f;
        if (Input.GetKey (KeyCode.D)) {
            dx += 1.0f;
        }
        if (Input.GetKey (KeyCode.A)) {
            dx -= 1.0f;
        }
        if (Input.GetKey (KeyCode.W))
            dz += 1.0f;

        if (Input.GetKey (KeyCode.S)) {
            dz -= 1.0f;
        }

        //Doing the movement based of which key was pressed
        Vector3 movement = new Vector3 (dx, 0.0f, dz) * speed * Time.deltaTime;
        player.MovePosition (transform.position + movement);

        //Updating the curLoc to the position of the spider
        curLoc = player.position;

        if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.S)) {
            anim.Play ("walk");
            //Making the player look in the direction it is moving
            player.MoveRotation (Quaternion.Lerp (player.rotation, Quaternion.LookRotation ((curLoc - prevLoc), new Vector3 (0, 1, 0)), Time.deltaTime * lookSpeed));
        } else {
            anim.Play ("New State");
        }

        //if (Input.GetMouseButtonDown (0)) {

        //    Vector2 mouseScreenPos = Input.mousePosition;
        //    float distance = Camera.main.transform.position.y;
        //    Vector3 screenPosWithZDist = (Vector3) mouseScreenPos + (Vector3.forward * distance);
        //    Vector3 theToWorldPos = Camera.main.ScreenToWorldPoint (screenPosWithZDist);
        //    Vector3 turning_pos = theToWorldPos - curLoc;
        //    turning_pos.y = 0.0f;
        //    player.MoveRotation (Quaternion.LookRotation (turning_pos));
        //}

    }
}