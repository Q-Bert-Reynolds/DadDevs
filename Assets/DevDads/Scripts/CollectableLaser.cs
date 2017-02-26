using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableLaser : MonoBehaviour {

    private Rigidbody rb;
    private PlayerController player;

	void Start () {
        rb = GetComponent<Rigidbody>();
        if (player == null)
            player = FindObjectOfType<PlayerController>();
	}
	
	void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            player.energy = player.maxEnergy;
            Destroy(this.gameObject);
        }
    }
}