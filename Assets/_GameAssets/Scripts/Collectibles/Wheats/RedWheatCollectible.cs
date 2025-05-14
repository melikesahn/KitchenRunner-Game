using UnityEngine;

public class RedWheatCollectible : MonoBehaviour,ICollectible
{
    [SerializeField] private WheatDesignSO _wheatDesingSO;
    [SerializeField] private PlayerController _playerController;
    
    

    public void Collect()
    {
        _playerController.SetJumpForce(_wheatDesingSO.IncreaseDecreaseMultiplier,_wheatDesingSO.ResetBoostDuration);
        Destroy(gameObject);
        
    }
}
