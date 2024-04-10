using TarodevController;
using UnityEngine;

public class FlyingMonster : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public float maintainDistance = 7f;
    public float distanceTolerance = 0.5f;

    public GameObject homingMissilePrefab;
    public float missileSpeed = 3f;
    public float fireRate = 2f;
    private float fireTimer;
    public float spawnDistance = 1f; // Distance in front of the player to spawn the missile
    public float missileLifetime = 5f; // Lifetime of the missile in seconds
    public float missileRotationSpeed = 200f; // Rotation speed of the missile
    private bool isFacingRight;

    private SpriteRenderer _homingGuy;

    [SerializeField] private AudioSource _swordHitSound;

    private void Start()
    {
        _homingGuy = GetComponent<SpriteRenderer>();

    }
    private void Update()
    {
        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Move towards or away from the player to maintain the desired distance, or stop if within the tolerance range
        if (distanceToPlayer > maintainDistance + distanceTolerance)
        {
            // Move towards the player
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        else if (distanceToPlayer < maintainDistance - distanceTolerance)
        {
            // Move away from the player
            Vector3 direction = (transform.position - player.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        // If the monster is within the tolerance range of the maintain distance, it stops moving

        // Handle missile firing
        if (fireTimer > 0)
        {
            fireTimer -= Time.deltaTime;
        }
        else
        {
            FireHomingMissile();
            fireTimer = fireRate; // Reset the fire timer
        }
    }

    private void FireHomingMissile()
    {

        // Calculate the spawn position in front of the monster, considering the facing direction
        Vector3 spawnDirection = transform.right * (isFacingRight ? 1 : -1); // Adjust based on the monster's facing direction
        Vector3 spawnPosition = transform.position + spawnDirection * spawnDistance;

        // Instantiate the missile at the spawn position
        GameObject missile = Instantiate(homingMissilePrefab, spawnPosition, Quaternion.identity);

        // Set the missile to automatically destroy after a certain time
        Destroy(missile, missileLifetime);

        // Assign homing behavior to the missile
        missile.AddComponent<HomingBehavior>().Initialize(player, gameObject, missileSpeed, missileRotationSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerMelee") || other.CompareTag("SwordProjectile"))
        {
            _swordHitSound.Play();
            _homingGuy.color = Color.red;
            FindObjectOfType<PlayerController>().HealPlayer();
            Destroy(gameObject, 0.25f);
           _homingGuy.color = Color.white;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerRange"))
        {
            
            Destroy(gameObject); // Destroy this monster when it exits the player's range
        }
    }
}