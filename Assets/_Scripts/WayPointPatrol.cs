using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class WayPointPatrol : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;

    public Transform[] wayPoints;

    private int currentWayPointIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.SetDestination(wayPoints[0].position);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_navMeshAgent.remainingDistance < _navMeshAgent.stoppingDistance)
        {
            currentWayPointIndex = (currentWayPointIndex + 1) % wayPoints.Length;
            _navMeshAgent.SetDestination(wayPoints[currentWayPointIndex].position);
        }
    }
}
