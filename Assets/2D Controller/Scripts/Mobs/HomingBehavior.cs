using TarodevController;
using UnityEngine;

public class HomingBehavior : MonoBehaviour
{
    private Transform target;
    private float speed;
    private GameObject owner;
    private float rotationSpeed;

    [SerializeField] private Animator _bullet;



    public void Initialize(Transform target, GameObject owner, float speed, float rotationSpeed)
    {
        this.target = target;
        this.owner = owner;
        this.speed = speed;
        this.rotationSpeed = rotationSpeed;
    }

    private void Update()
    {
        if (target is not null)
        {
            
            Vector3 direction = (new Vector3(target.position.x, target.position.y + 1, target.position.z) - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

            transform.Translate(Vector3.up * (speed * Time.deltaTime));
            
            if (_bullet is not null)
            {
                _bullet.SetBool("_bulletHit", false);
                _bullet.SetBool("_isShooting", true);
            }
        }
        else
        {
            _bullet.SetBool("_isShooting", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(collision.gameObject.CompareTag("Monster") || collision.gameObject.CompareTag("PlayerRange")))
        {
            

            if (collision.gameObject.CompareTag("Player"))
            {
                collision.GetComponent<PlayerController>().DamageHealth();
            }
            
            if (_bullet is not null)
            {
                _bullet.SetBool("_isShooting", false);

                _bullet.SetBool("_bulletHit", true);
                Destroy(gameObject, 0.21f);
            }
        }
    }
}