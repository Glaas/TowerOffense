using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public Controller controller;

    public string[] possibleNames = new string[] { "Marie", "Isabella", "Mika", "Marco", "Kais", "Ottavio", "Luca", "Maria", "Boyan", "Samartha", "David", "Kelly", "Jana", "Seb", "Steven", "Sunny", "Ousama", "Azat", "Aleks", "Devarya", "Jose Maria" };
    public int maxHealth = 5;
    public int currentHealth;
    public bool isAttacking;
    public bool isBeingTargeted = false;
    public GameObject coinPrefab;
    public GameObject deathPS;

    private void Awake()
    {
        controller = GetComponent<Controller>();

    }
    private void OnEnable()
    {
        name = possibleNames[Random.Range(0, possibleNames.Length)];
        InitHealth();
    }


    void InitHealth() => currentHealth = maxHealth;

    public void TakeDamage(int dmgAmount)
    {
        currentHealth -= dmgAmount;
        if (currentHealth <= 0)
        {
            Debug.Log(name + "got destroyed");
            Instantiate(coinPrefab, new Vector3(transform.position.x, transform.position.y + 1.2f, transform.position.z), Quaternion.identity);
            var deathPSObj = Instantiate(deathPS, new Vector3(transform.position.x, transform.position.y + 1.2f, transform.position.z), Quaternion.identity);
            Destroy(deathPSObj, 3f);
            Destroy(gameObject);
        }
    }
}
