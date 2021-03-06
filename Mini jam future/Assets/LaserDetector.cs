﻿using System.Collections;
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
        gameObject.transform.GetChild(2).gameObject.SetActive(!Powered);
    }

     void Connected()
    {
        Powered = true;
        InternalTimer = 0.2f;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "player" && Powered == true)
        {
            GameObject.Find("LevelManager").SendMessage("NextLevel");
        }
    }
}
