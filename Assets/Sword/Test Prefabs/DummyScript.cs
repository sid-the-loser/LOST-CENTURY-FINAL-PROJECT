using System.Collections;
using UnityEngine;

namespace Sword.Test_Prefabs
{
    public class DummyScript : MonoBehaviour
    {
        private SpriteRenderer _spriteComponent;

        [SerializeField] private AudioSource _swordHit;
        [SerializeField] private AudioSource _sealBreak;

        // Start is called before the first frame update
        void Start()
        {
            _spriteComponent = GetComponent<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("PlayerMelee") || other.gameObject.CompareTag("SwordProjectile"))
            {

                _swordHit.Play();
                _spriteComponent.color = Color.red;
                StartCoroutine(ChangeBack());
            }

        }

        IEnumerator ChangeBack()
        {
            yield return new WaitForSeconds(0.1f);
            _spriteComponent.color = Color.white;
        }
    }
}