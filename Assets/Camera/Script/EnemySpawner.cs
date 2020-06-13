using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnStart = 3f;
    [SerializeField] private int waveOneQuantity = 5;
    [SerializeField] private int waveTwoQuantity = 10;
    [SerializeField] private int waveThreeQuantity = 15;
    [SerializeField] private int waveFourQuantity = 20;
    [SerializeField] private int waveFiveQuantity = 25;
    private float spawnPointX;
    private float spawnPointY;
    private float spawnPointZ;
    private Vector3 spawnPosition;
    private SnakeHealth snakeHealth;
    private new Collider[] collider;
    private NavMeshAgent navMeshAgent;
    private int wave = 0;
    private bool waveStarted = false;
    [SerializeField] private TextMeshProUGUI waveNumber;
    [SerializeField] private LayerMask spawnBlockedLayers;

    void Awake()
    {
        //Wave 1
        wave = 1;
        WaveStarter(waveOneQuantity);
        snakeHealth = enemyPrefab.GetComponent<SnakeHealth>();
        navMeshAgent = enemyPrefab.GetComponent<NavMeshAgent>();
        //collider = enemyPrefab.GetComponent<Collider>();
    }

    void Update()
    {
        if(snakeHealth.Alive == 0 && waveStarted)
        {
            waveStarted = false;
            WaveController();
        }

        waveNumber.text = "Wave " + wave;
    }

    private void WaveController()
    {
        if (wave == 1)
        {
            WaveStarter(waveTwoQuantity);
        }
        else if (wave == 2)
        {
            WaveStarter(waveThreeQuantity);
        }
        else if (wave == 3)
        {
            WaveStarter(waveFourQuantity);
        }
        else if (wave == 4)
        {
            WaveStarter(waveFiveQuantity);
        }
        else if (wave == 5)
        {
            Debug.Log("?");
            SceneManager.LoadScene(0);
        }
        wave++;
    }

    private void SpawnEnemy(int contador)
    {
        if(contador > 30)
        {
            Debug.Log("Maior que Dez");
            return;
        }
        spawnPointX = Random.Range(-323, 301);
        spawnPointY = -54f;
        spawnPointZ = Random.Range(945, -271);
        spawnPosition = new Vector3(spawnPointX, spawnPointY, spawnPointZ);
        collider = Physics.OverlapSphere(spawnPosition, 2f, spawnBlockedLayers);
        
        if (collider.Length <= 1)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            waveStarted = true;
        } 
        else 
        {
            Debug.Log(collider.Length);
            SpawnEnemy(contador+1);
        }
    }

    IEnumerator Spawn()
    {

        yield return new WaitForSeconds(spawnStart + Random.Range(-.5f, .5f));

        SpawnEnemy(0);
    }

    private void WaveStarter(int enemyQuantity)
    {
        for(int i = 0; i < enemyQuantity; i++)
        {
            StartCoroutine(Spawn());
        }
    }
}
