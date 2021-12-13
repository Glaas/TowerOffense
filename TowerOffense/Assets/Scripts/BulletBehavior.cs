using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public TurretAttack shooter;
    public Transform target;
    [Range(0.0f, 1.0f)]
    public float speed = 1;
    public float distanceToTarget = float.MaxValue;

    public GameObject explosionPrefab;
    void Start()
    {
        shooter = DetectShooter();
        speed = shooter.bulletSpeed;
    }

    TurretAttack DetectShooter()
    {
        TurretAttack oneRandomGun = FindObjectOfType<TurretAttack>(); //To avoid nullRefs
        TurretAttack[] allGuns = FindObjectsOfType<TurretAttack>();

        foreach (var gun in allGuns)
        {
            if (Vector3.Distance(transform.position, gun.transform.position) < Vector3.Distance(transform.position, oneRandomGun.transform.position))
            {
                oneRandomGun = gun;
            }
        }
        return oneRandomGun;
    }

    public void InitTarget(GameObject target)
    {
        this.target = target.transform;
    }
    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        var targetPos = target.GetComponentInChildren<Collider>().bounds.center;
        distanceToTarget = Vector3.Distance(transform.position, targetPos);
        transform.Translate(Vector3.Normalize(targetPos - transform.position) * speed * Time.deltaTime);
        
        //Self destruct if shooter code is not running
        if (distanceToTarget < 0.5f)
        {
            Destroy(gameObject);
        }

    }
}
