using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Paraphernalia.Components;

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
            AudioManager.PlayVariedEffect("pickupCollectable");
            Destroy(this.gameObject);
        }
    }
}
