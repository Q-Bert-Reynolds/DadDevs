using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour {

    public GameObject player;
    public float cameraYDistanceFromPlayer;
    public float cameraZDistanceFromPlayer;

    private Rigidbody rb;
    private Rigidbody playerRb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        playerRb = player.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (playerRb != null) {
            rb.position = new Vector3(
            playerRb.position.x,
            rb.position.y,
            rb.position.z
            );
        }        
	}
}
