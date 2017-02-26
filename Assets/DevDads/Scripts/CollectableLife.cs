using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Destroy(this.gameObject);
        }
    }
}
