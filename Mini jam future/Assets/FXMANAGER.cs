using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXMANAGER : MonoBehaviour {
    void Update () {
        if (PlayerPrefs.GetInt ("PrettyMode") == 1) {
            gameObject.transform.GetChild (0).gameObject.SetActive (true);
        } else {
            gameObject.transform.GetChild (0).gameObject.SetActive (false);
        }
    }
}