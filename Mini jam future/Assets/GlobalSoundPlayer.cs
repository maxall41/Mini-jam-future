using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSoundPlayer : MonoBehaviour {
    public AudioSource teleportSFX;

    public void PlayTeleportSFX () {
        teleportSFX.Play ();
    }
}