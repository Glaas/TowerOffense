using UnityEngine;

public class TurretRotation : MonoBehaviour
{
    public GameObject gun;
    public Transform target;
    public TurretAttack turretAttack;
    void Awake()
    {
        turretAttack = GetComponent<TurretAttack>();
    }

    void Update()
    {
        if (turretAttack.currentTarget == null) return;
        Quaternion lookOnLook = Quaternion.LookRotation(turretAttack.currentTarget.transform.position - gun.transform.position);
        gun.transform.rotation = Quaternion.Slerp(gun.transform.rotation, lookOnLook, Time.deltaTime * 10);
    }
}
