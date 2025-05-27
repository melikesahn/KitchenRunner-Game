using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event Action<GameState> OnGameStateChanged;
    [SerializeField] private CatController _catController;
    [SerializeField] private PlayerHealthUI _playerHealthUI;
    [SerializeField] private int _maxEggCount;
    private int _currentEggCount;
    private GameState _currentGameState;
    [SerializeField] private EggCounterUI _eggCounterUI;
     [SerializeField] private WinLoseUI _winLoseUI;
    [SerializeField] private float _delay;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        HealthManager.Instance.OnPlayerHealth += HealthManager_OnPlayerHealth;
        _catController.OnCatCatched += CatController_OnCatCatched;
    }

    private void CatController_OnCatCatched()
    {
        _playerHealthUI.AnimateDamageForAll();
         StartCoroutine(OnGameOver());
    }

    private void HealthManager_OnPlayerHealth()
    {
        StartCoroutine(OnGameOver());
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
    private IEnumerator OnGameOver()
    {
        yield return new WaitForSeconds(_delay);
        //Instantiate(_fightingParticles, playerTransform.position, _fightingParticles.transform.rotation);
        ChangeGameState(GameState.GameOver);
        _winLoseUI.OnGameOver();
        //if(isCatCatched) _audioManager.Play(SoundType.CatSound);
    }
    public GameState GetCurrentGameState()
    {
        return _currentGameState;
    }

    
}
