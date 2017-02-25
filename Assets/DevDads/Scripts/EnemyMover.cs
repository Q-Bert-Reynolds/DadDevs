using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Paraphernalia.Utils;

    public class EnemyMover : MonoBehaviour {

    public bool pursues;
    public float speed;
    public PlayerController player;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player") {
            if (player.deltaInvisFrames == 0) {
                player.getHit();
            }           
        }
    }

        // Update is called once per frame
        void FixedUpdate() {
        if (player != null) {
            if (pursues) {
                moveTowardsPlayer(player.gameObject);
                //rb.velocity = Steering.Pursue(rb, player.body, speed);
            } else {
                moveTowardsPlayer(player.gameObject);
                //rb.velocity = Steering.Seek(rb, player.transform.position, speed);
            }
        }        
	}

    void moveTowardsPlayer(GameObject target) {
        transform.position = Vector3.MoveTowards(
            transform.position, 
            target.GetComponent<Rigidbody>().position,
            speed * Time.deltaTime
        );
    }
}
