using UnityEngine;

public class DarkWheatCollectible : MonoBehaviour,ICollectible
{
    [SerializeField] private WheatDesignSO _wheatDesingSO;
    [SerializeField] private PlayerController _playerController;
 

    public void Collect()
    {
        _playerController.SetMovementSpeed(_wheatDesingSO.IncreaseDecreaseMultiplier,_wheatDesingSO.ResetBoostDuration);
        Destroy(gameObject);
        
    }
}
