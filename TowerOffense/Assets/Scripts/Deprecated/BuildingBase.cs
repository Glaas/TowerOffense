// public class BuildingBase : MonoBehaviour //On every building, manages life and death
// {
//     public GameObject buildingPrefab;

//     public int cost;
//     public int maxHealth;
//     public int currentHealth;
//     public GameObject explosionPrefab;

//     private void Start()
//     {
//         InitHealth();
//     }

//     void InitHealth() => currentHealth = maxHealth;
//     public void DecreaseHealth(int amount)
//     {
//         currentHealth--;
//         if (currentHealth <= 0)
//         {
//             DOTween.Kill(this);
//             var obj = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
//             Destroy(obj, 2f);
//             GameObject.Find("TurretDestroySFX").GetComponent<AudioSource>().Play();
//             print($"{name} current health has reached zero and is destroyed now");
//             Destroy(gameObject);
//         }
//     }
// }
