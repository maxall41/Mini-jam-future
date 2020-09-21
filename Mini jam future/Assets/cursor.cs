using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursor : MonoBehaviour {
    private Camera cam;

    void Start () {
        Cursor.visible = false;
        cam = Camera.main;
    }

    void Update () {
        gameObject.transform.position = new Vector3 (cam.ScreenToWorldPoint (Input.mousePosition).x, cam.ScreenToWorldPoint (Input.mousePosition).y, -10);
    }
}