using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Laser : MonoBehaviour
{
    private GridLayout gridLayout;
    public Transform LaserEmitterPoint;
    private Tilemap tilemap;
    private float cooldown = 0.8f;
    public float LaserCooldownTime;
    public LayerMask WhatLaserCanHit;

    public float LaserEnd;

    public float LaserLength;

    private float AP;

    public bool Flip = false;

    private float SecondaryCooldownTimer;

    private Vector2 direction;

    public bool AttachedToGlitch = false;

    private bool IsSecretLaser;



    // Start is called before the first frame update
    void Start()
    {
        IsSecretLaser = gameObject.name.Contains("SecretLaser");
        AP = LaserLength;
        gridLayout = GameObject.Find("Grid").GetComponent<Grid>();
        tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
        cooldown = LaserCooldownTime;

        direction = new Vector2(1, 0);

        //if (gameObject.name.Contains("SecretLaser") == true)
        //{
        //    if (GameObject.Find("Player").GetComponent<PlayerMovement>().facingRight == false)
        //    {
        //        Flip = true;
        //        direction = new Vector2(-1, 0);
        //        LaserEnd = -LaserLength;
        //    } else
        //    {
        //        Flip = false;
        //        direction = new Vector2(1, 0);
        //        LaserEnd = LaserLength;
        //    }
        //} else {
        //    direction = new Vector2(1, 0);
        //}
    }

    void LaserDisconnect ()
    {
        AttachedToGlitch = false;
    }

    // Update is called once per frame
    void Update()
    {
        SecondaryCooldownTimer -= Time.deltaTime;
        //? update laser line
        LineRenderer l = gameObject.GetComponent<LineRenderer>();

        List<Vector3> pos = new List<Vector3>();
        pos.Add(LaserEmitterPoint.transform.position);
        if (gameObject.name.Contains("SecretLaser") == true)
        {
            pos.Add(new Vector3(LaserEmitterPoint.transform.position.x - AP, LaserEmitterPoint.transform.position.y));
        } else if (AttachedToGlitch == true)
        {
            pos.Add(new Vector3(AP, LaserEmitterPoint.transform.position.y));
        } else
        {
            pos.Add(new Vector3(LaserEmitterPoint.transform.position.x + AP, LaserEmitterPoint.transform.position.y));
        }
        l.startWidth = 0.3f;
        l.endWidth = 0.3f;
        l.SetPositions(pos.ToArray());
        l.useWorldSpace = true;
        //? run raycasting code
        cooldown -= Time.deltaTime;
        RaycastHit2D hit;
        if (gameObject.name.Contains("SecretLaser") == true)
        {
            hit = Physics2D.Raycast(LaserEmitterPoint.position, direction, AP, WhatLaserCanHit);
        } else
        {
            hit = Physics2D.Raycast(LaserEmitterPoint.position, -direction, AP, WhatLaserCanHit);
        }
            if (hit.collider != null)
            {
                Debug.Log("Hit something by casting from " + gameObject.name);
                //? hit soemthing
                if (hit.collider.gameObject.tag == "player")
                {
                    hit.collider.gameObject.SendMessage("RestartLevel");
                }
                else if (cooldown < 0)
                {
                    Vector3Int cellPosition = gridLayout.WorldToCell(hit.point);
                    tilemap.SetTile(cellPosition, null);
                    cooldown = LaserCooldownTime;
                }
                if (hit.collider.gameObject.tag == "glitch")
                {
                    AP = hit.collider.gameObject.transform.position.x;
                    AttachedToGlitch = true;
                    hit.collider.gameObject.SendMessage("BeingHitByLaser",gameObject);
                }
            }
            else
            {
                Debug.Log("Hit nothing by casting from " + gameObject.name);
                if (Flip == true)
                {
                    LaserEnd = -LaserLength;
                    direction = new Vector2(-1, 0);
                }
                else
                {
                    LaserEnd = LaserLength;
                direction = new Vector2(1, 0);
                }
            }
    }

}
