using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RotateLeft();
    }

    void RotateLeft ()
    {
        LeanTween.rotateLocal(gameObject, new Vector3(0, 0, 10f), 1.5f).setOnComplete(RotateRight);
    }

    void RotateRight ()
    {
        LeanTween.rotateLocal(gameObject, new Vector3(0,0,-10f), 1.5f).setOnComplete(RotateLeft);
    }
}
