using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowOnHoverSmall : MonoBehaviour {
    private Vector3 NormalScale;

    public void OnMouseOver () {
        NormalScale = gameObject.transform.localScale;
        LeanTween.scale (gameObject, new Vector3 (gameObject.transform.localScale.x + 0.00002f, gameObject.transform.localScale.y + 0.3f, gameObject.transform.localScale.z + 0.3f), 0.3f);
    }

    public void OnMouseExit () {
        LeanTween.scale (gameObject, new Vector3 (NormalScale.x, NormalScale.y, NormalScale.y), 0.3f);
    }
    // 0.0232474
}