using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Goto : MonoBehaviour
{
    public void Play ()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void GotoSettings()
    {
        //SceneManager.LoadScene
    }
}
