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
    private bool _isCatCatched;

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
        if (!_isCatCatched)
        {
            _playerHealthUI.AnimateDamageForAll();
            StartCoroutine(OnGameOver(true));
            CameraShake.Instance.ShakeCamera(1.5f, 2f, 0.5f);
            _isCatCatched = true;
        }
       
    }

    private void HealthManager_OnPlayerHealth()
    {
        StartCoroutine(OnGameOver(false));
    }

    private void OnEnable()
    {
        ChangeGameState(GameState.CutScene);
        BackgroundMusic.Instance.PlayBackgroundMusic(true);

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
    private IEnumerator OnGameOver(bool isCatCatched)
    {
        yield return new WaitForSeconds(_delay);
        //Instantiate(_fightingParticles, playerTransform.position, _fightingParticles.transform.rotation);
        ChangeGameState(GameState.GameOver);
        _winLoseUI.OnGameOver();
        if (isCatCatched)
        {
            AudioManager.Instance.Play(SoundType.CatSound);
        } 
    }
    public GameState GetCurrentGameState()
    {
        return _currentGameState;
    }

    
}
