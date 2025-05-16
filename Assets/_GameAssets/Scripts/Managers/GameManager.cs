using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private int _maxEggCount;
    private int _currentEggCount;
    [SerializeField] private EggCounterUI _eggCounterUI;

    private void Awake()
    {
        Instance = this;
    }
    public void OnEggCollected()
    {
        _currentEggCount++;
         _eggCounterUI.SetEggCounterText(_currentEggCount,  _maxEggCount);


        if (_currentEggCount == _maxEggCount)
        {
            //win
            _eggCounterUI.SetEggCompleted();
        }
        
    }
}
