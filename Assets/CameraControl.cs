using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

    public float SPEED = 20.0f;
    public float yawSpeed = 45.0f;
    public float pitchSpeed = 45.0f;

    public void Update() {

        /* Source: https://docs.unity3d.com/ScriptReference/Input.GetAxis.html */
        float yawMag = Input.GetAxis("Mouse X") * yawSpeed;
        float pitchMag = Input.GetAxis("Mouse Y") * pitchSpeed;
        this.transform.Rotate(0, yawMag, 0);
        this.transform.Rotate(-pitchMag, 0, 0);

        float dispMag = SPEED * Time.deltaTime;
        Vector3 normCamVec = this.transform.forward.normalized;

        Vector3 sideVec = dispMag * this.transform.TransformDirection(Vector3.right);
        Vector3 fwdVec = dispMag * normCamVec;

        if (Input.GetKey("a")) { this.transform.localPosition -= sideVec; }
        if (Input.GetKey("d")) { this.transform.localPosition += sideVec; }

        if (Input.GetKey("s")) { this.transform.localPosition -= fwdVec; }
        if (Input.GetKey("w")) { this.transform.localPosition += fwdVec; }

    }

}
