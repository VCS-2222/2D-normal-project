using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public bool isSpawning = false;

    private void Update()
    {
        if (isSpawning)
            return;

        StartCoroutine(Wave());
    }

    IEnumerator Wave()
    {
        isSpawning = true;
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, _sp.position, _sp.rotation);

        yield return new WaitForSeconds(0.1f);

        isSpawning = false;
    }
}
