using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Utils : MonoBehaviour {

    public static Bounds BoundsUnion(Bounds b0, Bounds b1) {
        if (b0.size == Vector3.zero && b1.size != Vector3.zero) {
            return b1;
        } else if (b0.size != Vector3.zero && b1.size == Vector3.zero) {
            return b0;
        }
        b0.Encapsulate(b1.min);
        b0.Encapsulate(b1.max);
        return b0;
    }

    public static Bounds CombineBoundsOfChildren(GameObject go) {
        Bounds b = new Bounds(Vector3.zero, Vector3.zero);
        if (go.GetComponent<Renderer>() != null) {
            b = BoundsUnion(b, go.GetComponent<Renderer>().bounds);
        }
        if (go.GetComponent<Collider>() != null) {
            b = BoundsUnion(b, go.GetComponent<Collider>().bounds);
        }
        foreach(Transform t in go.transform) {
            b = BoundsUnion(b, CombineBoundsOfChildren(t.gameObject));
        }

        return b;
    }

}
