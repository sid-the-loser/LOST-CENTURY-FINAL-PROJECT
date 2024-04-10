using UnityEngine;

public class WormBoss : MonoBehaviour
{
    public float xSpeed = 5f;  // Speed of the worm boss on the x-axis
    public float ySpeed = 2f;  // Speed of the worm boss on the y-axis

    private GameObject player;  // Reference to the player

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");  // Find the player by tag
    }

    void Update()
    {
        if (player != null)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        // Get the player's position
        Vector2 playerPosition = player.transform.position;

        // Determine the direction to move on the x-axis
        float xDirection = playerPosition.x > transform.position.x ? 1 : -1;

        // Determine the direction to move on the y-axis
        float yDirection = playerPosition.y > transform.position.y ? 1 : -1;

        // Calculate the new position for the worm boss
        Vector2 newPosition = new Vector2(transform.position.x + xDirection * xSpeed * Time.deltaTime, transform.position.y + yDirection * ySpeed * Time.deltaTime);

        // Move the worm boss to the new position
        transform.position = newPosition;
    }
}
