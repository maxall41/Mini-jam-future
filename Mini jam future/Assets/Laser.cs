using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Laser : MonoBehaviour {
    private GridLayout gridLayout;
    public Transform LaserEmitterPoint;
    private Tilemap tilemap;
    private float cooldown = 0.8f;
    public float LaserCooldownTime;
    public LayerMask WhatLaserCanHit;

    public float LaserEnd;

    public float LaserLength;

    private float AP;

    private float SecondaryCooldownTimer;

    private Vector2 direction;

    public bool AttachedToGlitch = false;

    private bool IsSecretLaser;

    public bool AttachedToLaser;

    public float InternalLaserTimeout1 = 99999999f;

    private float FlipTimeOut;

    public bool FlipRaycast = false;

    public float invincibility = -999999999f;

    // Start is called before the first frame update
    void Start () {
        IsSecretLaser = gameObject.name.Contains ("SecretLaser");
        AP = LaserLength;
        gridLayout = GameObject.Find ("Grid").GetComponent<Grid> ();
        tilemap = GameObject.Find ("Tilemap").GetComponent<Tilemap> ();
        cooldown = LaserCooldownTime;

        direction = new Vector2 (1, 0);
    }

    void LaserDisconnect () {
        AttachedToGlitch = false;
    }

    // Update is called once per frame
    void Update () {
        FlipTimeOut -= Time.deltaTime;
        invincibility -= Time.deltaTime;
        FlipTimeOut -= Time.deltaTime;
        SecondaryCooldownTimer -= Time.deltaTime;
        InternalLaserTimeout1 -= Time.deltaTime;
        if (InternalLaserTimeout1 < 0) {
            AttachedToLaser = false;
        }
        //? update laser line
        LineRenderer l = gameObject.GetComponent<LineRenderer> ();

        List<Vector3> pos = new List<Vector3> ();
        pos.Add (LaserEmitterPoint.transform.position);
        if (AttachedToLaser == false && FlipRaycast == false) {
            pos.Add (new Vector3 (LaserEmitterPoint.transform.position.x + AP, LaserEmitterPoint.transform.position.y));
        } else if (AttachedToLaser == false && FlipRaycast == true) {
            pos.Add (new Vector3 (LaserEmitterPoint.transform.position.x - AP, LaserEmitterPoint.transform.position.y));
        } else {
            pos.Add (new Vector3 (AP, LaserEmitterPoint.transform.position.y));
        }
        l.startWidth = 0.3f;
        l.endWidth = 0.3f;
        l.SetPositions (pos.ToArray ());
        l.useWorldSpace = true;
        //? run raycasting code
        cooldown -= Time.deltaTime;
        RaycastHit2D hit;
        if (FlipRaycast == false) {
            hit = Physics2D.Raycast (LaserEmitterPoint.position, direction, AP, WhatLaserCanHit);
        } else {
            hit = Physics2D.Raycast (LaserEmitterPoint.position, -direction, AP, WhatLaserCanHit);
        }
        if (hit.collider != null) {
            Debug.Log ("Hit something by casting from " + gameObject.name);
            //? hit player
            if (hit.collider.gameObject.tag == "player") {
                if (GameObject.Find ("GlitchVac").GetComponent<GlitchVac> ().JCGT > 0 && FlipTimeOut < 0) {
                    FlipRaycast = true;
                    invincibility = 1f;
                    FlipTimeOut = 1f;
                } else if (invincibility < 0) {
                    hit.collider.gameObject.SendMessage ("RestartLevel");
                }
            }
            //? hit tilemap
            else if (cooldown < 0) {
                try {
                    Vector3Int cellPosition = gridLayout.WorldToCell (hit.point);
                    tilemap.SetTile (cellPosition, null);
                    cooldown = LaserCooldownTime;
                } catch {
                    gridLayout = GameObject.Find ("Grid").GetComponent<Grid> ();
                    tilemap = GameObject.Find ("Tilemap").GetComponent<Tilemap> ();
                }
            }
            //? hit glitch
            if (hit.collider.gameObject.tag == "glitch") {
                AP = hit.collider.gameObject.transform.position.x;
                AttachedToGlitch = true;
                hit.collider.gameObject.SendMessage ("BeingHitByLaser", gameObject);
            }
            //? hit laser detector
            else if (hit.collider.gameObject.tag == "LaserDetector") {
                AttachedToLaser = true;
                AP = hit.collider.gameObject.transform.position.x;
                hit.collider.gameObject.SendMessage ("Connected");
                InternalLaserTimeout1 = 0.2f;
            }
            //? hit nothing
        }
    }

    // void ResetVectors () {
    //     Debug.Log ("resetting vectors");
    //     FlipRaycast = false;
    //     // direction *= -1;
    //     // AP = LaserLength;
    // }

}