using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public Controller controller;

    public string[] possibleNames = new string[] { "Marie", "Isabella", "Mika", "Marco", "Kais", "Ottavio", "Luca", "Maria", "Boyan", "Samartha", "David", "Kelly", "Jana", "Seb", "Steven", "Sunny", "Ousama", "Azat", "Aleks", "Devarya", "Jose Maria" };
    public int maxHealth = 5;
    public int currentHealth;
    public bool isAttacking;

    private void Awake()
    {
        controller = GetComponent<Controller>();
        name = possibleNames[Random.Range(0, possibleNames.Length)];

    }
    void Start()
    {
    }

    void Update()
    {

    }

    void InitHealth() => currentHealth = maxHealth;

    public void TakeDamage(int dmgAmount)
    {
        currentHealth -= dmgAmount;
        if (currentHealth <= 0) Destroy(gameObject);
    }
}
