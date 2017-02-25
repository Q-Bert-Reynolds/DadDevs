using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Paraphernalia.Utils;

public class MoveForward : MonoBehaviour {

    public float speed;
    public GameObject target;
    public PlayerController player;

    private Vector3 position;

    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.transform.LookAt(target.transform);
    }
	
	// Update is called once per frame
	void Update () {
        if (target != null && speed > 0) {
            rb.velocity = rb.transform.forward * speed;
        }
    }

    void OnTriggerEnter(Collider other) {
        if (this.tag == "EnemyBullet") {
            if (other.gameObject.tag == "Player" && player.deltaInvisFrames == 0) {
                if (player.deltaInvisFrames == 0) {
                    player.getHit();
                }
                Destroy(this.gameObject);
            }                

            if (other.gameObject.tag == "Building") {
                Destroy(this.gameObject);
            }
        }
    }

    void OnBecameInvisible() {
        gameObject.SetActive(false);
    }
}
