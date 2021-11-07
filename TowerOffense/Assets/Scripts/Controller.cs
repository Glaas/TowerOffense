using UnityEngine;
using UnityEngine.AI;
public class Controller : MonoBehaviour
{
    public enum ENEMY_STATE { APPROACHING, ATTACKING, DEAD }
    public ENEMY_STATE state;
    public NavMeshAgent agent;
    public Transform target;
    public Animator animator;
    public float distanceToTower;
    public bool isAttacking = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        target = GameObject.Find("--Target--").transform;
        state = ENEMY_STATE.APPROACHING;
        agent.SetDestination(target.position);
        InvokeRepeating(nameof(CheckState), 1, .5f);

    }

    private void CheckState()
    {
        print(name + " checking state");
        switch (state)
        {
            case ENEMY_STATE.APPROACHING:
                ComputeDistanceToTower();
                if (distanceToTower <= 2)
                {
                    state = ENEMY_STATE.ATTACKING;
                    CancelInvoke(nameof(CheckState));
                    CheckState();
                }
                break;
            case ENEMY_STATE.ATTACKING:
                agent.isStopped = true;
                print(name + "is attacking");
                isAttacking = true;
                animator.SetBool("isAttacking", true);
                InvokeRepeating(nameof(AttackTower), 1, 2f);
                break;
            case ENEMY_STATE.DEAD:
                break;
            default: throw new System.Exception("Invalid state");
        }

    }

    void AttackTower()
    {
        print(name + " is damaging tower");
        GameObject.Find("Tower").GetComponentInParent<BuildingStats>().TakeDamage(2);
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
            if (i == 0) continue;
            if (i + 1 >= agent.path.corners.Length) continue;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(agent.path.corners[i], agent.path.corners[i + 1]);
        }
    }
}
