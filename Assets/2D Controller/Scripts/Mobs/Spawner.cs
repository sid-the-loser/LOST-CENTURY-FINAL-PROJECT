using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] monsterPrefabs;
    public Transform player; // Variable to hold the player object

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerRange"))
        {
            SpawnMonster();
        }
    }

    private void SpawnMonster()
    {
        // Randomly decide whether to spawn a monster
        if (Random.value < 0.5f) // 50% chance not to spawn anything, adjust as needed
        {
            int randomIndex = Random.Range(0, monsterPrefabs.Length);

            Vector3 spawnPosition = transform.position;
            GameObject spawnedMonster = Instantiate(monsterPrefabs[randomIndex], spawnPosition, Quaternion.identity);
            AssignPlayerToMonster(spawnedMonster);
        }
    }


    // Function to assign the player object to the spawned monster
    private void AssignPlayerToMonster(GameObject monster)
    {
        if (monster != null && player != null)
        {
            MagmaBrute magmaBrute = monster.GetComponent<MagmaBrute>();
            FlyingMonster flyingMonster = monster.GetComponent<FlyingMonster>();
            ShadowLurker shadowLurker = monster.GetComponent<ShadowLurker>();

            if (magmaBrute != null)
            {
                magmaBrute.player = player;
            }
            else if (flyingMonster != null)
            {
                flyingMonster.player = player;
            }
            else if (shadowLurker != null)
            {
                shadowLurker.player = player;
            }
        }
    }
}
