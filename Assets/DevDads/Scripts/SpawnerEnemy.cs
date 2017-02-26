using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour {

    public EnemyController[] enemyPrefab;
    public float timeToSpawnNewEnemy;
    public int maxEnemiesAliveAtOnce;

    private float originalTimeToSpawnNewEnemy;
    private float deltaSpawnNewEnemy = 0;
    private Rigidbody rb;
    private Vector3 spawnPosition;
    private List<EnemyController> enemyList;

    void Start () {
        rb = GetComponent<Rigidbody>();
        spawnPosition = new Vector3();
        originalTimeToSpawnNewEnemy = timeToSpawnNewEnemy;
        timeToSpawnNewEnemy += Random.Range(0, 2);
        enemyList = new List<EnemyController>();
	}
	
	void Update () {
        deltaSpawnNewEnemy += Time.deltaTime;

        PruneList();

        CheckSpawnNewEnemy();        
    }

    void CheckSpawnNewEnemy() {
        if (deltaSpawnNewEnemy >= timeToSpawnNewEnemy && enemyList.Count < maxEnemiesAliveAtOnce) {
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
        enemyList.Add(enemy);
    }

    void PruneList() {
        for (int i = enemyList.Count - 1; i > 0; i--) {
            if (enemyList[i] == null)
                enemyList.Remove(enemyList[i]);
        }
    }
}
