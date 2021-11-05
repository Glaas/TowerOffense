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

    public void InitTarget(Transform target)
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
        distanceToTarget = Vector3.Distance(transform.position, target.transform.position);      
        transform.Translate(Vector3.Normalize(target.position - transform.position) * speed * Time.deltaTime);

    }

}
