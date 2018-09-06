using UnityEngine;
using System.Collections;
using System;

public class Sun : MonoBehaviour {

	public Color color;
    public float period, orbitRadius;
    private float time, timeToRads, timeToDegs, rads, degs;
    private Transform tf;

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

	public Vector3 GetWorldPosition() {
		return this.tf.position;
	}

}
