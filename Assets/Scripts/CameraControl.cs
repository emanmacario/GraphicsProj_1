using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    public float translateSpeed=5, yawSpeed=45, pitchSpeed=45;
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
        tf.Rotate(0, yawMag, 0, Space.World);

        /* Source: https://docs.unity3d.com/ScriptReference/Transform.Translate.html */
        translateMag = translateSpeed * Time.deltaTime;
        if (Input.GetKey("a") || Input.GetKey("a")) { tf.Translate(translateMag * Vector3.left); }
        if (Input.GetKey("e") || Input.GetKey("d")) { tf.Translate(translateMag * Vector3.right); }
        if (Input.GetKey("o") || Input.GetKey("s")) { tf.Translate(translateMag * Vector3.back); }
        if (Input.GetKey(",") || Input.GetKey("w")) { tf.Translate(translateMag * Vector3.forward); }

        boundInWorld();

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

}
