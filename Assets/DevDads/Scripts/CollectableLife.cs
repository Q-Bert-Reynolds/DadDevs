using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Paraphernalia.Components;

public class CollectableLife : MonoBehaviour {

    private PlayerController player;

    void Start()
    {
        if (player == null)
            player = FindObjectOfType<PlayerController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.lives += 1;
            AudioManager.PlayVariedEffect("pickupCollectable");
            Destroy(this.gameObject);
        }
    }
}
