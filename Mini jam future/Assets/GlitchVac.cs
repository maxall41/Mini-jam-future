using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class GlitchVac : MonoBehaviour {
    public Vector2 range = new Vector2 (2.3f, 0.3f);
    private Vector2 Position;

    public float speed;

    public AudioSource vacuamSFX;

    public Vector2 OffsetPosition;

    private bool GlitchVacOn = false;

    public TextMeshProUGUI GlitchText;

    public GameObject glitch;

    private bool facingRight = true;

    private float GlitchCooldown = 0.2f;

    private bool MovingToPlayer = false;

    private float InternalCooldown = 0.2f;

    private bool VacTest = false;

    public bool SuckerHasBeenActivated = false;

    public float JCGT = -10f;

    private bool JCGT_tick = false;

    private GameObject TempHolder1_CanceledPullingGlitch;

    // Update is called once per frame
    void Update () {
        if (JCGT_tick == true) {
            JCGT -= Time.deltaTime;
        }
        if (JCGT < 0) {
            JCGT_tick = false;
        }
        if (SuckerHasBeenActivated == true) {
            InternalCooldown -= Time.deltaTime;
        }
        if (InternalCooldown < 0) {
            try {
                SuckerHasBeenActivated = false;
                InternalCooldown = 0.2f;
                TempHolder1_CanceledPullingGlitch.tag = "glitch";
                Debug.Log ("The suck has been cancelled!");
            } catch {

            }
        }
        GlitchCooldown -= Time.deltaTime;
        //? glitch suck in
        if (Input.GetMouseButton (0)) {
            if (VacTest == false) {
                vacuamSFX.Play ();
                VacTest = true;
            }
            InternalCooldown = 0.2f;
            SuckerHasBeenActivated = true;
            GlitchVacOn = true;
            Collider2D[] Objects = Physics2D.OverlapBoxAll (CalculatePosition (), range, 0);
            int i = 0;
            for (i = 0; i < Objects.Length; i++) {
                GameObject Object = Objects[i].gameObject;
                float step = speed * Time.deltaTime;
                if (Object.tag == "glitch" || Object.tag == "NONACTIVEGLITCH") {
                    TempHolder1_CanceledPullingGlitch = Object;

                    Object.tag = "NONACTIVEGLITCH";

                    // move sprite towards the target location
                    Object.transform.position = Vector2.MoveTowards (Object.transform.position, transform.position, step);
                    MovingToPlayer = true;
                }
            }
        } else {
            //? glitch expel out
            if (Input.GetMouseButtonDown (1)) {
                GlitchVacOn = true;
                var finalresult = Regex.Match (GlitchText.text, @"\d+").Value;
                if (int.Parse (finalresult) > 0) {
                    if (facingRight == true) {
                        GameObject NewGlitch = Instantiate (glitch, new Vector3 (transform.position.x + 1.5f, transform.position.y, 0), Quaternion.identity);
                        Rigidbody2D rb = NewGlitch.GetComponent<Rigidbody2D> ();
                        rb.velocity = new Vector2 (1 * speed, rb.velocity.y);
                        rb = null;
                    } else {
                        GameObject NewGlitch = Instantiate (glitch, new Vector3 (transform.position.x - 1.5f, transform.position.y, 0), Quaternion.identity);
                        NewGlitch.transform.SetParent (GameObject.Find ("LevelManager").GetComponent<LevelLoader> ().LastLevel.transform);
                        Rigidbody2D rb = NewGlitch.GetComponent<Rigidbody2D> ();
                        rb.velocity = new Vector2 (-1 * speed, rb.velocity.y);
                        rb = null;
                    }
                    JCGT = 0.5f;
                    JCGT_tick = true;

                    GlitchText.text = (((int.Parse (finalresult) - 1).ToString ()) + " GLITCHES");
                }
            } else {
                vacuamSFX.Stop ();
                VacTest = false;
                GlitchVacOn = false;
            }
        }

    }

    private void OnDrawGizmosSelected () {
        Gizmos.DrawWireCube (CalculatePosition (), range);
    }

    private Vector2 CalculatePosition () {
        return new Vector3 (transform.position.x + OffsetPosition.x, transform.position.y + OffsetPosition.y, 0);
    }

    public void FlipGlitchVac () {
        facingRight = !facingRight;
        Vector3 Scaler = OffsetPosition;
        Scaler.x *= -1;
        OffsetPosition = Scaler;

    }

    void OnTriggerEnter2D (Collider2D col) {
        if (col.gameObject.tag == "NONACTIVEGLITCH" && GlitchVacOn == true && GlitchCooldown < 0) {
            col.gameObject.SendMessage ("CutLaser"); //? disconnect lasers
            GlitchCooldown = 0.2f;

            var finalresult = Regex.Match (GlitchText.text, @"\d+").Value;

            GlitchText.text = (((int.Parse (finalresult) + 1).ToString ()) + " GLITCHES");
            Destroy (col.gameObject);
        }
    }
}