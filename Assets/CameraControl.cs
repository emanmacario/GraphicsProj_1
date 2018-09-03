using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    public float fwdSpeed=20.0f, yawSpeed=45.0f, pitchSpeed=45.0f;
    private float fwdMag, yawMag, pitchMag;
    private Transform tf;

    public void Start() {
        tf = this.transform;
    }

    public void Update() {

        /* Source: https://docs.unity3d.com/ScriptReference/Input.GetAxis.html */
        yawMag = Input.GetAxis("Mouse X") * yawSpeed;
        pitchMag = Input.GetAxis("Mouse Y") * pitchSpeed;
        tf.Rotate(0, yawMag, 0);
        tf.Rotate(-pitchMag, 0, 0);

        fwdMag = fwdSpeed * Time.deltaTime;
        Vector3 normCamVec = tf.forward.normalized;

        Vector3 sideVec = fwdMag * tf.TransformDirection(Vector3.right);
        Vector3 fwdVec = fwdMag * normCamVec;

        if (Input.GetKey("a")) { tf.localPosition -= sideVec; }
        if (Input.GetKey("d")) { tf.localPosition += sideVec; }

        if (Input.GetKey("s")) { tf.localPosition -= fwdVec; }
        if (Input.GetKey("w")) { tf.localPosition += fwdVec; }

    }

}
