using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Paraphernalia.Utils;

    public class EnemyMover : MonoBehaviour {
    
    public float speed;
    private PlayerController player;
    public int scoreValue;
    public int enemyType;

    private float deltaWaitToShoot = 0;   
    private float deltaStartShot = 0;
    private float deltaWaitBtwnShots = 0;
    private int shotsFired = 0;
    private GameController gameController;

    public float timeToWaitCharge;
    public float timeToWaitShot;
    public float timeToWaitBtwnShots;
    public float maxShots;
    public MoveForward shotPrefab;

    private Transform shotSpawn;
    private bool shooting;
    private Rigidbody rb;
    
	void Start () {
        rb = GetComponent<Rigidbody>();
        shooting = false;
        shotSpawn = transform.GetChild(0);

        timeToWaitCharge = timeToWaitCharge + Random.Range(0f, 1f);

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
                    deltaWaitBtwnShots += Time.deltaTime;

                    if (deltaWaitBtwnShots >= timeToWaitBtwnShots) {
                        SpawnBullets(enemyType);
                        shotsFired += 1;
                        deltaWaitBtwnShots -= timeToWaitBtwnShots;
                    }

                    if (shotsFired >= maxShots) {
                        deltaWaitBtwnShots = 0;
                        deltaStartShot = 0;
                        shotsFired = 0;
                        shooting = false;                        
                    }                    
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

    void SpawnBullets(int patternNumber) {
        if (patternNumber == 0) {
            MoveForward shot = Instantiate(shotPrefab, shotSpawn.position, shotSpawn.rotation) as MoveForward;
            shot.transform.forward = shotSpawn.transform.forward;
        } else if (patternNumber == 1) {
            for (int i = 0; i < 5; i++) {
                MoveForward shot = Instantiate(shotPrefab, shotSpawn.position, shotSpawn.rotation) as MoveForward;
                shot.speed = shot.speed * 0.5f;
                Vector3 originalAngle = shot.transform.forward;
                Quaternion spreadAngle = Quaternion.AngleAxis((i - 2)*20, new Vector3(0, 1, 0));
                shot.transform.forward = spreadAngle * originalAngle;
            }            
        }  
    }
}
