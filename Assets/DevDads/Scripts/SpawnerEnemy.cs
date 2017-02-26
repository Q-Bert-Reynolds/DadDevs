using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour {

    public EnemyController[] enemyPrefab;
    public float timeToSpawnNewEnemy;

    private float originalTimeToSpawnNewEnemy;
    private float deltaSpawnNewEnemy = 0;
    private Rigidbody rb;
    private Vector3 spawnPosition;

    void Start () {
        rb = GetComponent<Rigidbody>();
        spawnPosition = new Vector3();
        originalTimeToSpawnNewEnemy = timeToSpawnNewEnemy;
        timeToSpawnNewEnemy += Random.Range(0, 2);
	}
	
	void Update () {
        deltaSpawnNewEnemy += Time.deltaTime;

        CheckSpawnNewEnemy();
    }

    void CheckSpawnNewEnemy() {
        
        if (deltaSpawnNewEnemy >= timeToSpawnNewEnemy) {
            deltaSpawnNewEnemy = 0;
            timeToSpawnNewEnemy = originalTimeToSpawnNewEnemy + Random.Range(0, 2);

            SpawnNewEnemy();
        }
    }

    void SpawnNewEnemy() {
        int whichMobToSpawn = 0;
        if (Random.value > 0.9f) {
            whichMobToSpawn = 1;
        } else {
            whichMobToSpawn = 0;
        }

        EnemyController enemy = Instantiate(enemyPrefab[whichMobToSpawn], new Vector3(rb.position.x, 0f, rb.position.z), rb.rotation) as EnemyController;
        enemy.transform.forward = this.transform.forward;
    }
}
