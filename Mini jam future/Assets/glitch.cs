using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glitch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localPosition = new Vector3(0, 0, 0);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("col");
       if (col.gameObject.tag == "player")
        {
            Debug.Log("Hit player");
            GameObject[] glitches = GameObject.FindGameObjectsWithTag("glitch");
            Debug.Log(glitches);
            if (gameObject.transform.parent.gameObject.tag != "NONACTIVEGLITCH")
            {
                Vector3 GlitchTeleportPos = GetClosest(glitches).position;
                col.gameObject.transform.position = new Vector3(GlitchTeleportPos.x - 2f, GlitchTeleportPos.y, 0);
            }
        }
    }

    Transform GetClosest(GameObject[] glitches)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject g in glitches)
        {
            Transform t = g.transform;
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist && dist > 5f)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }
}
