using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour {
    private HashSet<GameObject> collidingLasers = new HashSet<GameObject>();

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Ascend Beam(Clone)") {
            collidingLasers.Add(other.gameObject);
        }
    }

    public void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.name == "Ascend Beam(Clone)") {
            collidingLasers.Remove(other.gameObject);
        }
    }

    public int GetNumLasersColliding() {
        return collidingLasers.Count;
    }
}