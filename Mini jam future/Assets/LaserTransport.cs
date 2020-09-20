using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTransport : MonoBehaviour {
    public GameObject SecretLaser;
    public bool HitByLaser = false;

    public float InternalCooldown = 3.0f;

    public bool Disconnected = false;

    public GameObject NewSecretLaser;

    public GameObject AttachedLaser;

    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        InternalCooldown -= Time.deltaTime;
        if (InternalCooldown < 0 && HitByLaser == true) {
            Destroy (NewSecretLaser);
        }

        GameObject[] glitches = GameObject.FindGameObjectsWithTag ("glitch");
        GameObject[] NONACTIVEglitches = GameObject.FindGameObjectsWithTag ("NONACTIVEGLITCH");
        int TotalGlitchLength = glitches.Length + NONACTIVEglitches.Length;
        if (TotalGlitchLength <= 1) {
            HitByLaser = false;
        }

        if (NewSecretLaser == null && AttachedLaser != null) {
            HitByLaser = false;
        }
    }

    void BeingHitByLaser (GameObject Laser) {
        AttachedLaser = Laser;
        InternalCooldown = 1.0f;
        GameObject[] glitches = GameObject.FindGameObjectsWithTag ("glitch");
        if (HitByLaser == false && glitches.Length > 1) {
            HitByLaser = true;
            Debug.Log ("being hit by laser");
            GameObject GlitchTeleportPos = GetClosest (glitches);
            NewSecretLaser = Instantiate (SecretLaser, GlitchTeleportPos.transform.position, Quaternion.identity);
            NewSecretLaser.transform.SetParent (GlitchTeleportPos.transform);
        }
    }

    void CutLaser () {
        if (HitByLaser == true) {
            AttachedLaser.SendMessage ("LaserDisconnect");
            Disconnected = true;
            AttachedLaser = null;
            Destroy (NewSecretLaser);
        }
    }

    GameObject GetClosest (GameObject[] glitches) {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject g in glitches) {
            float dist = Vector3.Distance (g.transform.position, currentPos);
            Debug.Log (dist);
            if (dist < minDist && dist > 3f) {
                tMin = g;
                minDist = dist;
            }
        }
        return tMin;
    }
}