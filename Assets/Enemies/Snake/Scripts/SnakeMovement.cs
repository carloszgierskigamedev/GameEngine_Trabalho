using UnityEngine;
using UnityEngine.AI;

public class SnakeMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private SnakeAggro aggroDetection; 

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        aggroDetection = GetComponent<SnakeAggro>();
        aggroDetection.OnAggro += AggroDetection_OnAggro;
    }

    private void AggroDetection_OnAggro(Transform target)
    {
        navMeshAgent.SetDestination(target.position);
    }
}
