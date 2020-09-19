using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public GameObject[] levels;
    private int LevelIndexPointer = 0;

    void Start()
    {
        LoadLevel();
    }

    public void NextLevel()
    {
        LoadLevel();
    }

    private void LoadLevel()
    {
        if (LevelIndexPointer <= levels.Length)
        {
            Instantiate(levels[LevelIndexPointer], new Vector3(0, 0, 0), Quaternion.identity);
            LevelIndexPointer++;
        } else
        {
            Debug.LogWarning("NO MORE LEVELS LEFT!");
        }

    }
   
}
