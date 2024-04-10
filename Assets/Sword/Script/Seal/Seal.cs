using UnityEngine;

namespace Sword.Script.Seal
{
    public class Seal : MonoBehaviour
    {
        private SpriteRenderer _spriteComponent;
    
        [SerializeField] private AudioSource _swordHit;
        [SerializeField] private AudioSource _sealBreak;

        private bool sealBroken;
    
        void Start()
        {
            _spriteComponent = GetComponent<SpriteRenderer>();
        }
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((other.gameObject.CompareTag("PlayerMelee") || other.gameObject.CompareTag("SwordProjectile")) && !sealBroken)
            {
                _swordHit.Play();
                _spriteComponent.color = Color.red;
                _sealBreak.Play();
                sealBroken = true;
                FindObjectOfType<GameManager>().sealsBroken++;
            }

        }
    }
}
