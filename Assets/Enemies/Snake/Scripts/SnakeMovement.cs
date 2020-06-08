using UnityEngine;
using UnityEngine.AI;

public class SnakeMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private SnakeAggro aggroDetection; 
    private bool isAggroed;
    private Transform target;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        aggroDetection = GetComponentInChildren<SnakeAggro>();
        aggroDetection.OnAggro += AggroDetection_OnAggro;
    }

    private void Update()
    {
        if(isAggroed)
        {
            navMeshAgent.SetDestination(target.position);
        }
    }

    private void AggroDetection_OnAggro(Transform target)
    {
        navMeshAgent.Warp(transform.position);
        isAggroed = true;
        this.target = target;
    }
}
