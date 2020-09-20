using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {
    public GameObject[] levels;

    public GameObject LastLevel;

    public TextMeshProUGUI GlitchText;

    private int LevelIndexPointer = 0;

    void Start () {
        LoadLevel ();
    }

    public void NextLevel () {
        LevelIndexPointer++;
        LoadLevel ();
    }

    private void LoadLevel () {
        if (LevelIndexPointer < levels.Length) {
            LastLevel = Instantiate (levels[LevelIndexPointer], new Vector3 (0, 0, 0), Quaternion.identity);
        } else {
            Debug.LogWarning ("NO MORE LEVELS LEFT!");
            SceneManager.LoadScene ("ThanksForPlaying");
        }

    }

    public void RestartLevel () {
        GlitchText.text = "0 GLITCHES";
        Destroy (LastLevel);
        LoadLevel ();
    }

}