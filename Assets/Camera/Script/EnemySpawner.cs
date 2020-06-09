using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnStart = 0f;
    [SerializeField] private int waveOneQuantity = 6;
    [SerializeField] private int waveTwoQuantity = 10;
    [SerializeField] private int waveThreeQuantity = 15;
    [SerializeField] private int waveFourQuantity = 20;
    [SerializeField] private int waveFiveQuantity = 25;
    private float spawnPointX;
    private float spawnPointY;
    private float spawnPointZ;
    private Vector3 spawnPosition;
    private SnakeHealth snakeHealth;
    private Collider[] collider;
    private NavMeshAgent navMeshAgent;


    void Awake()
    {
        //Wave 1
        WaveStarter(waveOneQuantity);
        snakeHealth = enemyPrefab.GetComponent<SnakeHealth>();
        navMeshAgent = enemyPrefab.GetComponent<NavMeshAgent>();
        //collider = enemyPrefab.GetComponent<Collider>();
    }

    void Update()
    {
        if (snakeHealth.Killed == 5)
        {
            WaveStarter(waveTwoQuantity);
        }
        if (snakeHealth.Killed == 15)
        {
            WaveStarter(waveThreeQuantity);
        }
        if (snakeHealth.Killed == 30)
        {
            WaveStarter(waveFourQuantity);
        }
        if (snakeHealth.Killed == 50)
        {
            WaveStarter(waveFiveQuantity);
        }
        if (snakeHealth.Killed == 75)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void SpawnEnemy()
    {
        spawnPointX = Random.Range(-323, 301);
        spawnPointY = -54f;
        spawnPointZ = Random.Range(945, -271);
        spawnPosition = new Vector3(spawnPointX, spawnPointY, spawnPointZ);
        collider = Physics.OverlapSphere(spawnPosition, 2f);
        
        if (collider.Length <= 1)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
        else 
        {
            Debug.Log(collider.Length);
            Debug.Log(collider[2].name);
            SpawnEnemy();
        }
    }

    IEnumerator Spawn()
    {

        yield return new WaitForSeconds(spawnStart);

        SpawnEnemy();
    }

    private void WaveStarter(int enemyQuantity)
    {
        for(int i = 0; i <= enemyQuantity; i++)
        {
            StartCoroutine(Spawn());
        }
    }
}
