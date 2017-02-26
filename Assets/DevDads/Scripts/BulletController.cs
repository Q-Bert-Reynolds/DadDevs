using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Paraphernalia.Utils;
using Paraphernalia.Components;

public class BulletController : MonoBehaviour {

    public float speed;
    public string onDestroySound = "laserHit";
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
        if (harmfulToPlayer) {
            if (other.gameObject.tag == "Player") {
                if (player.forceFieldActive) {
                    harmfulToPlayer = false;
                    gameObject.tag = "PlayerPrimaryBullet";
                    gameObject.layer = LayerMask.NameToLayer("PlayerBullet");
                    rb.velocity = -rb.transform.forward * speed;
                } else {
                    if (player.deltaInvisFrames == 0) {
                        player.getHit();
                    }
                    Destroy(this.gameObject);
                }                
            }
        } else {            
            if (other.gameObject.tag == "Enemy") {
                Destroy(this.gameObject);
                Destroy(other.gameObject);
                AudioManager.PlayVariedEffect(onDestroySound);
            }
        }

        if (other.gameObject.tag == "Building") {
            Destroy(this.gameObject);
            AudioManager.PlayVariedEffect(onDestroySound);
        }
    }

    void OnBecameInvisible() {
        gameObject.SetActive(false);
    }
}
