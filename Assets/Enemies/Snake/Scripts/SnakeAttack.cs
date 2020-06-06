using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SnakeAttack : MonoBehaviour
{
    [SerializeField] private float attackDamage = 1;
    private NavMeshAgent navMeshAgent;
    SnakeMovement snakeMovement;
    PlayerHealth playerHealth;
    Rigidbody rigidBody;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        snakeMovement = GetComponent<SnakeMovement>();
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Colidiu");
        playerHealth = col.gameObject.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.DealDamage(attackDamage, transform.position);
            StartCoroutine(FreezeMovement());
        }
    }

    IEnumerator FreezeMovement()
    {
        navMeshAgent.speed = 0;
        rigidBody.isKinematic = true;

        yield return new WaitForSeconds(1);

        navMeshAgent.speed = 4;
        rigidBody.isKinematic = false;
    }
}
