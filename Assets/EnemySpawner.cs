using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnTime = 0f;
    [SerializeField] private float spawnStart = 0f;
    private float spawnPointX;
    private float spawnPointY;
    private float spawnPointZ;
    private Vector3 spawnPosition;

    void Awake()
    {
        StartCoroutine(Spawn());
    }

    void Update()
    {
        
    }

    IEnumerator Spawn()
    {

        yield return new WaitForSeconds(spawnStart);

        while(true)
        {
        
            spawnPointX = Random.Range(-44, 38);
            spawnPointY = 3.15f;
            spawnPointZ = Random.Range(21, 87);
            spawnPosition = new Vector3(spawnPointX, spawnPointY, spawnPointZ);

            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity); 
            yield return new WaitForSeconds(spawnTime);

        }
    }
}
