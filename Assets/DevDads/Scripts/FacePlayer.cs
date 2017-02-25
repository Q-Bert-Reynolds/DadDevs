using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour {

    public GameObject player;

    private Vector3 direction;

    void Start() {
        player = GameObject.FindWithTag("Player");
    }

    void Update() {
        //face the player
        if (player != null) transform.forward = player.transform.position - transform.position;
    }
}
