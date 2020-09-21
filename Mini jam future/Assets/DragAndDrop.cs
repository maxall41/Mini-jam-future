using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour {
    bool canMove;
    bool dragging;
    Collider2D collider;
    public float InternalTimer = -999999f;
    void Start () {
        collider = GetComponent<Collider2D> ();
        canMove = false;
        dragging = false;

    }
    // Update is called once per frame
    void Update () {
        InternalTimer -= Time.deltaTime;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

        if (Input.GetMouseButtonDown (0)) {
            if (collider == Physics2D.OverlapPoint (mousePos)) {
                canMove = true;
            } else {
                canMove = false;
            }
            if (canMove) {
                dragging = true;
                InternalTimer = 0.2f;
            }

        }
        if (dragging) {
            gameObject.GetComponent<Rigidbody2D> ().MovePosition (mousePos);
        }
        if (Input.GetMouseButtonUp (0)) {
            canMove = false;
            dragging = false;
        }
    }
}