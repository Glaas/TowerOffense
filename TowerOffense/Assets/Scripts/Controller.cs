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
    }
    private void Start()
    {
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


}
