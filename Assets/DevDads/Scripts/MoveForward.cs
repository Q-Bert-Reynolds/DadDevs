using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour {

    public float speed;

    private Transform target;
    private static PlayerController player;
    private static GameController gameController;
    public bool harmfulToPlayer;

    private Vector3 position;

    Rigidbody rb;

	// Use this for initialization
	void Start () {
        if (player == null)
            player = FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody>();
        rb.velocity = rb.transform.forward * speed;
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log(this.tag + " hit " + other.tag);
        if (harmfulToPlayer) {
            if (other.gameObject.tag == "Player" && player.deltaInvisFrames == 0) {
                if (player.deltaInvisFrames == 0) {
                    player.getHit();
                }
                Destroy(this.gameObject);
            }
        } else {
            if (other.gameObject.tag == "Enemy") {
                Debug.Log("here");
                Destroy(this.gameObject);
                Destroy(other.gameObject);
            }
        }

        if (other.gameObject.tag == "Building") {
            Destroy(this.gameObject);
        }
    }

    void OnBecameInvisible() {
        gameObject.SetActive(false);
    }
}
