using UnityEngine;

public class RedWheatCollectible : MonoBehaviour,ICollectible
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private float _forceIncreaseSpeed;
    [SerializeField] private float _resetBoostDuration;
    

    public void Collect()
    {
        _playerController.SetJumpForce(_forceIncreaseSpeed, _resetBoostDuration);
        Destroy(gameObject);
        
    }
}
