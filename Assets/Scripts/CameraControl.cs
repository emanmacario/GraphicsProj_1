using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    public float translateSpeed=20, yawSpeed=45, pitchSpeed=45;
    private float translateMag, yawMag, pitchMag;
    private Transform tf;

    public void Start() {
        tf = this.transform;
    }

    public void Update() {

        /* Source: https://docs.unity3d.com/ScriptReference/Input.GetAxis.html */
        yawMag = Input.GetAxis("Mouse X") * yawSpeed;
        pitchMag = Input.GetAxis("Mouse Y") * pitchSpeed;
        tf.Rotate(-pitchMag, 0, 0);
        tf.Rotate(0, yawMag, 0);

        /* Source: https://docs.unity3d.com/ScriptReference/Transform.Translate.html */
        translateMag = translateSpeed * Time.deltaTime;
        if (Input.GetKey("a")) { tf.Translate(translateMag * Vector3.left); }
        if (Input.GetKey("d")) { tf.Translate(translateMag * Vector3.right); }
        if (Input.GetKey("s")) { tf.Translate(translateMag * Vector3.back); }
        if (Input.GetKey("w")) { tf.Translate(translateMag * Vector3.forward); }

    }

}
