using UnityEngine;
using UnityEngine.AI;
public class Controller : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    public float distanceToTower;
    public enum ENEMY_STATE { APPROACHING, ATTACKING, DEAD }
    public ENEMY_STATE state;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        target = GameObject.Find("Tower").transform;
        state = ENEMY_STATE.APPROACHING;
        agent.SetDestination(target.position);
    }
    private void Update()
    {
        ComputeDistanceToTower();


        //TODO implement attack behaviour
        //  switch (state) 
        //  {
        //
        //      default:
        // }

    }
    void ComputeDistanceToTower() => distanceToTower = Vector3.Distance(target.position, transform.position);

    private void OnDrawGizmosSelected()
    {
        foreach (var item in agent.path.corners)
        {
            Gizmos.DrawWireSphere(item, .25f);
        }
        for (int i = 0; i < agent.path.corners.Length; i++)
        {
            if (i==0)continue;
            if (i+1>=agent.path.corners.Length)continue;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(agent.path.corners[i], agent.path.corners[i+1]);
        }
    }
}
