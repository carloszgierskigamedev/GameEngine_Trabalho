using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SnakeAttack : MonoBehaviour
{
    [SerializeField] private float attackDamage = 1;
    private NavMeshAgent navMeshAgent;
    PlayerHealth playerHealth;
    Rigidbody rigidBody;

    void Awake()
    {
        navMeshAgent = GetComponentInParent<NavMeshAgent>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
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

        yield return new WaitForSeconds(1);

        navMeshAgent.speed = 4;
    }
}
