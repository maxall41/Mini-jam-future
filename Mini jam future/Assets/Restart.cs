using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour {
    public void RestartLevel () {
        GameObject.Find ("Player").SendMessage ("RestartLevel");
    }
}