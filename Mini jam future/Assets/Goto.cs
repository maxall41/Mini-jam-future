using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Goto : MonoBehaviour {
    public void Play () {
        SceneManager.LoadScene ("SampleScene");
    }
    public void GotoCredits () {
        SceneManager.LoadScene ("Credits");
    }

    public void GotoMenu () {
        SceneManager.LoadScene ("menu");
    }
}