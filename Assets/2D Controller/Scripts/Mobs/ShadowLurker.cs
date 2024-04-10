using TarodevController;
using UnityEngine;

public class ShadowLurker : MonoBehaviour
{
    public Transform player;
    public float attackRange = 5f;
    public float moveSpeed = 3f;

    private float attackCooldown = 5f;
    public float attackCooldownDuration = 3f;

    private Collider2D myCollider;

    [SerializeField] private AudioSource _swordHitSoundA;

    private SpriteRenderer _assassinBro;

    // Invisibility properties
    public float invisibleDuration = 2f;
    private float invisibilityTimer;
    private void Start()
    {
        myCollider = GetComponent<Collider2D>(); // Get the collider component
        _assassinBro = GetComponent<SpriteRenderer>(); 
    }

    private void Update()
    {
        // Handle Invisibility Timer
        if (invisibilityTimer > 0)
        {
            invisibilityTimer -= Time.deltaTime;
            if (invisibilityTimer <= 0)
            {
                GetComponent<SpriteRenderer>().enabled = true; // Make the enemy visible again
                myCollider.enabled = true;
                transform.position = player.position + new Vector3(0, 0.945f,-2); // Teleport behind the player
                _assassinBro = GetComponent <SpriteRenderer>();
            }
        }

        // Check if the enemy is in attack range
        if (Vector3.Distance(transform.position, player.position) <= attackRange && attackCooldown <= 0)
        {
            InvisibilityAttack();
            attackCooldown = attackCooldownDuration;
        }
        else
        {
            // Move towards the player
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }

        // Cooldown for attacks
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    public void InvisibilityAttack()
    {
        GetComponent<SpriteRenderer>().enabled = false; // Make the enemy invisible
        myCollider.enabled = false;
        invisibilityTimer = invisibleDuration;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerMelee") || other.CompareTag("SwordProjectile"))
        {
            _swordHitSoundA.Play();
           _assassinBro.color = Color.red;
           FindObjectOfType<PlayerController>().HealPlayer();
            Destroy(gameObject, 0.27f);
           _assassinBro.color = Color.white;
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
