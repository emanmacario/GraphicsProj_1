using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    public float translateSpeed=5, yawSpeed=45, pitchSpeed=45;
    /* Set both to true for pseudo-FlightSim style instead of FPS style */
    public bool flightSimYaw=false, flightSimPitch=false;

    private Space chosenYawSpace;
    private int chosenPitchDirec;
    private float translateMag, yawMag, pitchMag;
    private Vector3 lastPos;
    private Transform tf;
    private Rigidbody rb;

    public void Start() {
        tf = this.transform;
        rb = this.gameObject.GetComponent<Rigidbody>();
        chosenYawSpace = flightSimYaw ? Space.Self : Space.World;
        chosenPitchDirec = flightSimPitch ? 1 : -1;
    }

    public void Update() {

        /* Source: https://docs.unity3d.com/ScriptReference/Input.GetAxis.html */
        yawMag = Input.GetAxis("Mouse X") * yawSpeed;
        pitchMag = Input.GetAxis("Mouse Y") * pitchSpeed * chosenPitchDirec;
        tf.Rotate(0, yawMag, 0, chosenYawSpace);
        tf.Rotate(pitchMag, 0, 0);

        /* Source: https://docs.unity3d.com/ScriptReference/Transform.Translate.html */
        translateMag = translateSpeed * Time.deltaTime;
        if (Input.GetKey("a") || Input.GetKey("a")) { tf.Translate(translateMag * Vector3.left); }
        if (Input.GetKey("e") || Input.GetKey("d")) { tf.Translate(translateMag * Vector3.right); }
        if (Input.GetKey("o") || Input.GetKey("s")) { tf.Translate(translateMag * Vector3.back); }
        if (Input.GetKey(",") || Input.GetKey("w")) { tf.Translate(translateMag * Vector3.forward); }

        boundInWorld();

        lastPos = tf.localPosition;

    }

    private void boundInWorld() {
        Vector3 pos = tf.localPosition;
        float x=pos.x, y=pos.y, z=pos.z;
        float sqRadius = 5;
        x = (x < -sqRadius) ? -sqRadius : (sqRadius < x) ? sqRadius : x;
        y = (y < 0) ? 0 : y;
        z = (z < -sqRadius) ? -sqRadius : (sqRadius < z) ? sqRadius : z;
        tf.localPosition = new Vector3(x, y, z);
    }

    public void OnCollisionEnter(Collision c) {
        if (c.gameObject.name == "Terrain") {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            tf.localPosition = lastPos;
        }
    }

    public void OnCollisionExit(Collision c) {
        if (c.gameObject.name == "Terrain") {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

}
