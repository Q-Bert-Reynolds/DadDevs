using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Paraphernalia.Utils;
using Paraphernalia.Components;

public class MoveForward : MonoBehaviour {

    public float speed;
    public string onDestroySound = "laserHit";
    public string onHitParticles = "HitParticles";
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
        PlayEffects();

        if (harmfulToPlayer) {
            if (other.gameObject.tag == "Player" && player.deltaInvisFrames == 0) {
                if (player.deltaInvisFrames == 0) {
                    player.getHit();
                }
                Destroy(this.gameObject);
            }
        } else {
            if (other.gameObject.tag == "Enemy") {
                Destroy(this.gameObject);
                Destroy(other.gameObject);
            }
        }

        if (other.gameObject.tag == "Building") {
            Destroy(this.gameObject);
        }

    }

    void PlayEffects () {
        ParticleManager.Play(onHitParticles, transform.position);
        AudioManager.PlayVariedEffect(onDestroySound);
    }

    void OnBecameInvisible() {
        gameObject.SetActive(false);
    }
}
