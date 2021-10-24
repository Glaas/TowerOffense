using UnityEngine;
using UnityEngine.AI;
public class Controller : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target.position);
    }


}
