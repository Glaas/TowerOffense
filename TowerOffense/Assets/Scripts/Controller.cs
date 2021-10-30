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
        target = GameObject.Find("Tower").transform;
        state = ENEMY_STATE.APPROACHING;
    }
    private void Start()
    {
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


}
