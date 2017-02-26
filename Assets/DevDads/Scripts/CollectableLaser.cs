using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Paraphernalia.Components;

public class CollectableLaser : MonoBehaviour {
    
    private PlayerController player;

	void Start () {
        if (player == null)
            player = FindObjectOfType<PlayerController>();
	}
	
	void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            player.energy = player.maxEnergy;
            AudioManager.PlayVariedEffect("pickupCollectable");
            Destroy(this.gameObject);
        }
    }
}