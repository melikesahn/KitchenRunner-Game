using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
     private PlayerController _playerController;
     private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
       // _playerRigidbody = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<ICollectible>(out var collectible))
        {
            collectible.Collect();
        }

    }
     private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.TryGetComponent<IBoostable>(out var boostable))
        {
            boostable.Boost(_playerController);
            //boostable.PlayBoostParticle(transform);
        }
        /* else if(other.gameObject.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.GiveDamage(_playerRigidbody, _playerVisualTransform);
            damageable.PlayHitParticle(transform);
        } */
    }

}
