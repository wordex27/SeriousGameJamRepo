    using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{

    public Transform targ;
    public float attackDistance = 5f;
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
}
