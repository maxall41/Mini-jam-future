using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBottom : MonoBehaviour {
    void OnCollisionEnter2D (Collision2D col) {
        if (col.gameObject.tag == "player") {
            // GameObject.Find ("LevelManager").GetComponent<LevelLoader> ().RebootLevel ();
        } else {
            Destroy (col.gameObject);
        }
    }
}