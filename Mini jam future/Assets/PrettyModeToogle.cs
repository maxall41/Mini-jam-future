using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class PrettyModeToogle : MonoBehaviour {
    public bool mode = true;
    private TextMeshProUGUI self;

    public void Toogle () {
        if (gameObject.GetComponent<DragAndDrop> ().InternalTimer > 0) {

            if (PlayerPrefs.GetInt ("PrettyMode") == 1) {
                mode = false;
                PlayerPrefs.SetInt ("PrettyMode", 0);
            } else {
                mode = true;
                PlayerPrefs.SetInt ("PrettyMode", 1);
            }

            if (mode == true) {
                self.text = "PRETTY MODE:ON";
            } else {
                self.text = "PRETTY MODE:OFF";
            }
        }
    }

    void Start () {
        self = gameObject.GetComponent<TextMeshProUGUI> ();
        if (PlayerPrefs.GetInt ("PrettyMode") == 1) {
            mode = true;
            self.text = "PRETTY MODE:ON";
        } else {
            mode = false;
            self.text = "PRETTY MODE:OFF";
        }
    }
}