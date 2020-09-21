using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light : MonoBehaviour {
    private float ScaleMin;
    private float ScaleMax;
    private float t = 0.0f;
    void Start () {
        ScaleMin = transform.localScale.x;
        ScaleMax = transform.localScale.x + 0.02f;
    }

    void Update () {
        // animate the position of the game object...
        transform.localScale = new Vector3 (Mathf.Lerp (ScaleMin, ScaleMax, t), Mathf.Lerp (ScaleMin, ScaleMax, t), Mathf.Lerp (ScaleMin, ScaleMax, t));

        // .. and increase the t interpolater
        t += 0.4f * Time.deltaTime;

        // now check if the interpolator has reached 1.0
        // and swap maximum and minimum so game object moves
        // in the opposite direction.
        if (t > 1.0f) {
            float temp = ScaleMax;
            ScaleMax = ScaleMin;
            ScaleMin = temp;
            t = 0.0f;
        }
    }
}