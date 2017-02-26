using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableForceShield : MonoBehaviour {

    private PlayerController player;

    void Start() {
        if (player == null)
            player = FindObjectOfType<PlayerController>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            player.setForceShield(true);
            player.deltaInvisFrames = 8;
            Destroy(this.gameObject);
        }
    }
}
