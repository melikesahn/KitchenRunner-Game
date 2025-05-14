using UnityEngine;

public class SpatulaBooster : MonoBehaviour, IBoostable
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private Animator _spatulaAnimator;
    //[SerializeField] private GameObject _boostParticlesPrefab;

    private bool _isActivated;
    public void Boost(PlayerController playerController)
    {
        if (_isActivated)  return; 
        PlayBoostAnimation();
        Rigidbody playerRigidbody = playerController.GetPlayerRigidbody();
        playerRigidbody.linearVelocity = new Vector3(playerRigidbody.linearVelocity.x, 0f, playerRigidbody.linearVelocity.z);
        playerRigidbody.AddForce(transform.forward * _jumpForce, ForceMode.Impulse);
        _isActivated = true;
        Invoke(nameof(ResetActivation), 0.2f);
    }
    public void PlayBoostAnimation()
    {
        _spatulaAnimator.SetTrigger(Consts.OtherAnimations.IS_SPATULA_JUMPİNG);
    }
    private void ResetActivation()
    {
        _isActivated = false;
    }
}
