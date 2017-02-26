using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class EnemyMover : MonoBehaviour {
    
    public float speed;
    public float turnSpeed = 1;
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

    public Transform turretTransform;
    public Animator robotAnimator;
    public Transform shotSpawn;
    private bool shooting;
    private Rigidbody rb;
    private Vector3 targetForward;
    
	void Start () {
        rb = GetComponent<Rigidbody>();
        shooting = false;

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

    void Update() {
        if (player != null) {
            targetForward = (player.transform.position - transform.position).normalized;

            if (!shooting) {
                deltaWaitToShoot += Time.deltaTime;
                if (deltaWaitToShoot > timeToWaitCharge) {
                    deltaWaitToShoot = 0;
                    shooting = true;
                }

                moveTowardsPlayer();

            } else {
                robotAnimator.SetFloat("speed", 0);
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
            turretTransform.rotation = Quaternion.LookRotation(targetForward);
        }        
	}

    void moveTowardsPlayer() {
        transform.forward = Vector3.Lerp(transform.forward, targetForward, turnSpeed * Time.deltaTime);

        rb.velocity = targetForward * speed;
        float forward = Vector3.Dot(rb.velocity, transform.forward);
        float right = Vector3.Dot(rb.velocity, transform.right);
        robotAnimator.SetFloat("speed", speed);
        robotAnimator.SetFloat("forward", forward);
        robotAnimator.SetFloat("right", right);
    }

    public void Die() {
        gameController.AddScore(scoreValue);
        gameObject.SetActive(false);
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
