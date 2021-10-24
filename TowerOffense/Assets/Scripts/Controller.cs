using UnityEngine;
using UnityEngine.AI;
public class Controller : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    public float distanceToTower;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Tower").transform;
        agent.SetDestination(target.position);
    }
    private void Update()
    {
        distanceToTower = Vector3.Distance(target.position, transform.position);
    }


}
