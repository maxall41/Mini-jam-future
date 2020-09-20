﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoGlitchArea : MonoBehaviour {
    void OnTriggerEnter2D (Collider2D col) {
        if (col.gameObject.tag == "glitch") {
            Destroy (col.gameObject);
        }
    }
}