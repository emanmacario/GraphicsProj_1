using UnityEngine;
using System.Collections;
using System;

public class Sun : MonoBehaviour {

    public Color color;
    public float period=8, orbitRadius=10;
    private float time, timeToRads, timeToDegs, rads, degs;
    private Transform tf;

    public Vector3 getWorldPos() { return tf.position; }
    public Vector3 getSunDirec() { return tf.TransformDirection(Vector3.left); }

    public void Start() {
        time = 0;
        timeToRads = (float) (2 * Math.PI / period);
        timeToDegs = (float) (360 / period);
        tf = this.transform;
        tf.localPosition = new Vector3(orbitRadius, 0, 0);
    }

    public void Update() {
        time += Time.deltaTime;
        while (period < time) { time -= period; }
        rads = time * timeToRads;
        degs = time * timeToDegs;
        tf.localPosition = orbitRadius * new Vector3((float) Math.Cos(rads), (float) Math.Sin(rads), 0);
        tf.rotation = Quaternion.AngleAxis(degs, Vector3.forward);
    }

}
