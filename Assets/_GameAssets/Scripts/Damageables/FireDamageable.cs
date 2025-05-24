using UnityEngine;

public class FireDamageable : MonoBehaviour, IDamageable
{
   
    //[SerializeField] private GameObject _hitParticlesPrefab;

    
    [SerializeField] private float _force = 10f;
    //[SerializeField] private float _hitParticlesDestroyDuration = 2f;

    //private HealthManager _healthManager;
    //private AudioManager _audioManager;

    public void GiveDamage(Rigidbody playerRigidbody, Transform playerVisualTransform)
    {
        HealthManager.Instance.Damage(1);
        playerRigidbody.AddForce(-playerVisualTransform.forward * _force, ForceMode.Impulse);
       // _audioManager.Play(SoundType.ChickSound);
        Destroy(gameObject);
    }
}
