using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDetector : MonoBehaviour
{
    private float InternalTimer = 9999999f;
    public bool Powered;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InternalTimer -= Time.deltaTime;
        if (InternalTimer < 0)
        {
            //? Laser got disonnected
            Powered = false;
        }
        gameObject.GetComponent<Animator>().SetBool("IsOnline", Powered);
    }

     void Connected()
    {
        Powered = true;
        InternalTimer = 0.2f;
    }
}
