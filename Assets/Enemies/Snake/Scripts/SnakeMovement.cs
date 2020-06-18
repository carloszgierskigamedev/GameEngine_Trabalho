using UnityEngine;
using UnityEngine.AI;

public class SnakeMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private SnakeAggro aggroDetection; 
    private bool isAggroed;
    private Transform target;
    private AudioSource audioSource;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        aggroDetection = GetComponentInChildren<SnakeAggro>();
        aggroDetection.OnAggro += AggroDetection_OnAggro;
    }

    private void Update()
    {
        if(isAggroed)
        {
            float speed = navMeshAgent.velocity.magnitude;
            navMeshAgent.SetDestination(target.position);
        }
        
        if(navMeshAgent.velocity.magnitude > 2f && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    private void AggroDetection_OnAggro(Transform target)
    {
        navMeshAgent.Warp(transform.position);
        isAggroed = true;
        this.target = target;
    }
}
