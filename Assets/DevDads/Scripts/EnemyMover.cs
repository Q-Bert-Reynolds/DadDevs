using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Paraphernalia.Utils;

    public class EnemyMover : MonoBehaviour {
    
    public float speed;
    private PlayerController player;
    public int scoreValue;

    private float deltaWaitToShoot;   
    private float deltaStartShot;
    private GameController gameController;
    public float timeToWaitCharge;
    public float timeToWaitShot;
    public MoveForward shotPrefab;

    private Transform shotSpawn;
    private bool shooting;
    private Rigidbody rb;
    
	void Start () {
        rb = GetComponent<Rigidbody>();
        shooting = false;
        shotSpawn = transform.GetChild(0);
        if (player == null) {
            player = FindObjectOfType<PlayerController>();
        }
        if (gameController == null)
            gameController = FindObjectOfType<GameController>();
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player") {
            if (player.deltaInvisFrames == 0) {
                player.getHit();
            }           
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "PlayerPrimaryBullet") {
            Die();
        }
    }

        void FixedUpdate() {
        if (player != null) {
            if (!shooting) {
                deltaWaitToShoot += Time.deltaTime;
                if (deltaWaitToShoot > timeToWaitCharge) {
                    deltaWaitToShoot = 0;
                    shooting = true;
                }
                moveTowardsPlayer(player.gameObject);
            } else {
                deltaStartShot += Time.deltaTime;
                if (deltaStartShot > timeToWaitShot) {
                    deltaStartShot = 0;
                    shooting = false;
                    MoveForward shot = Instantiate(shotPrefab, shotSpawn.position, shotSpawn.rotation) as MoveForward;
                    shot.transform.forward = shotSpawn.transform.forward;
                }
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

    public void Die() {
        gameController.AddScore(scoreValue);
        Destroy(this.gameObject);
    }
}
