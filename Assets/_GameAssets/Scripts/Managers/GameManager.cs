using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event Action<GameState> OnGameStateChanged;
    [SerializeField] private int _maxEggCount;
    private int _currentEggCount;
    private GameState _currentGameState;
    [SerializeField] private EggCounterUI _eggCounterUI;
     [SerializeField] private WinLoseUI _winLoseUI;

    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        ChangeGameState(GameState.Play);

    }
    public void ChangeGameState(GameState gameState)
    {
        OnGameStateChanged?.Invoke(gameState);
        _currentGameState = gameState;
        Debug.Log($"Game State: {gameState}");
    }
    public void OnEggCollected()
    {
        _currentEggCount++;
        _eggCounterUI.SetEggCounterText(_currentEggCount, _maxEggCount);


        if (_currentEggCount == _maxEggCount)
        {
            //win
            _eggCounterUI.SetEggCompleted();
            ChangeGameState(GameState.GameOver);
            _winLoseUI.OnGameWin();
        }

    }
    public GameState GetCurrentGameState()
    {
        return _currentGameState;
    }

    
}
