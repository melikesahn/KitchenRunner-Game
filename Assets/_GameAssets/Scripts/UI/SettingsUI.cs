using DG.Tweening;
using MaskTransitions;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUı : MonoBehaviour
{
    [SerializeField] private GameObject _settingsPopupObject;
    [SerializeField] private GameObject _blackBackgroundObject;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _mainMenuButton;

    
    [SerializeField] private float _scaleDuration;

    private Image _blackBackgroundImage;

    private void Awake()
    {
        _blackBackgroundImage = _blackBackgroundObject.GetComponent<Image>();
        _settingsPopupObject.transform.localScale = Vector3.zero;

        _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        // _musicButton.onClick.AddListener(OnMusicButtonClicked);
        //_soundButton.onClick.AddListener(OnSoundButtonClicked);
        _resumeButton.onClick.AddListener(OnResumeButtonClicked);
        _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked); 
    }
    private void OnMainMenuButtonClicked()
    {
        AudioManager.Instance.Play(SoundType.TransitionSound);
        
        TransitionManager.Instance.LoadLevel(Consts.SceneNames.MENU_SCENE);
    }

    private void OnSettingsButtonClicked()
    {
        AudioManager.Instance.Play(SoundType.ButtonClickSound);
        //_audioManager.Play(SoundType.ButtonClickSound);
        GameManager.Instance.ChangeGameState(GameState.Pause);
        _blackBackgroundObject.SetActive(true);
        _settingsPopupObject.SetActive(true);
        _blackBackgroundImage.DOFade(0.8f, _scaleDuration).SetEase(Ease.Linear);
        _settingsPopupObject.transform.DOScale(1.5f, _scaleDuration).SetEase(Ease.OutBack);

    }
    private void OnResumeButtonClicked()
    {
        AudioManager.Instance.Play(SoundType.ButtonClickSound);
        //_audioManager.Play(SoundType.ButtonClickSound);

        _blackBackgroundImage.DOFade(0f, _scaleDuration).SetEase(Ease.Linear);
        _settingsPopupObject.transform.DOScale(0f, _scaleDuration).SetEase(Ease.OutExpo).OnComplete(() =>
        {
             GameManager.Instance.ChangeGameState(GameState.Resume);
            _settingsPopupObject.SetActive(false);
            _blackBackgroundObject.SetActive(false);
        });
    }
}
