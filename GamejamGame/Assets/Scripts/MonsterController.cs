using System;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{

    public Transform[] idlePositions;
    private GameObject player;
    public Transform targ;
    private LockerController lockerController;
    private float attackDistance;
    private NavMeshAgent agent;
    private float distance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            lockerController = player.GetComponent<LockerController>();
        }
        if (player != null && lockerController != null){
            attackDistance = 1.5f;
            if (!lockerController.getLocker()){
                targ = player.transform;
                distance = Vector3.Distance(agent.transform.position, targ.position);
                if (distance < attackDistance)
                {
                    agent.isStopped = true;
                }
                else
                {
                    agent.isStopped = false;
                    agent.destination = targ.position;
                }
            }
            else
            {
                attackDistance = 3f;
                distance = Vector3.Distance(agent.transform.position, targ.position);
                if (distance < attackDistance)
                {
                    targ = idlePositions[UnityEngine.Random.Range(0, idlePositions.Length)];
                }
                agent.isStopped = false;
                agent.destination = targ.position;
            }
        }
    }
}
