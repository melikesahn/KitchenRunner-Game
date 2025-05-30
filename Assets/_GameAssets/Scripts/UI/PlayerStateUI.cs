using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class PlayerStateUI : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private RectTransform _playerWalkingTransform;
    [SerializeField] private RectTransform _playerSlidingTransform;
    [SerializeField] private RectTransform _boosterSpeedTransform;
    [SerializeField] private RectTransform _boosterJumpTransform;
    [SerializeField] private RectTransform _boosterSlowTransform;

    [SerializeField] private PlayableDirector _playableDirector;



    [SerializeField] private Image _goldBoosterWheatImage;
    [SerializeField] private Image _redBoosterWheatImage;
    [SerializeField] private Image _darkBoosterWheatImage;


    [SerializeField] private Sprite _playerWalkingActiveSprite;
    [SerializeField] private Sprite _playerWalkingPassiveSprite;
    [SerializeField] private Sprite _playerSlidingActiveSprite;
    [SerializeField] private Sprite _playerSlidingPassiveSprite;

    [SerializeField] private float _moveDuration;
    [SerializeField] private Ease _moveEase;

    public RectTransform GetBoosterSpeedTransform => _boosterSpeedTransform;
    public RectTransform GetBoosterJumpTransform => _boosterJumpTransform;
    public RectTransform GetBoosterSlowTransform => _boosterSlowTransform;
    public Image GetGoldBoosterWheatImage => _goldBoosterWheatImage;
    public Image GetRedBoosterWheatImage => _redBoosterWheatImage;
    public Image GetDarkBoosterWheatImage => _darkBoosterWheatImage;


    private Image _playerWalkingImage;
    private Image _playerSlidingImage;

    private void Awake()
    {
        _playerWalkingImage = _playerWalkingTransform.GetComponent<Image>();
        _playerSlidingImage = _playerSlidingTransform.GetComponent<Image>();
    }

    private void Start()
    {
        _playerController.OnPlayerStateChanged += PlayerController_OnPlayerStateChanged;
        _playableDirector.stopped += OnTimelineFinished;
       

    }

    private void OnTimelineFinished(PlayableDirector director)
    {
         SetStateUserInterfaces(_playerWalkingActiveSprite, _playerSlidingPassiveSprite, _playerWalkingTransform, _playerSlidingTransform);
    }

    private void PlayerController_OnPlayerStateChanged(PlayerState playerState)
    {
        switch (playerState)
        {
            case PlayerState.Idle:
            case PlayerState.Move:
                SetStateUserInterfaces(_playerWalkingActiveSprite, _playerSlidingPassiveSprite, _playerWalkingTransform, _playerSlidingTransform);
                break;

            case PlayerState.Slide:
            case PlayerState.SlideIdle:
                SetStateUserInterfaces(_playerWalkingPassiveSprite, _playerSlidingActiveSprite, _playerSlidingTransform, _playerWalkingTransform);
                break;
        }
    }
    private void SetStateUserInterfaces(Sprite playerWalkingSprite, Sprite playerSlidingSprite,
       RectTransform activeTransform, RectTransform passiveTransform)
    {
        _playerWalkingImage.sprite = playerWalkingSprite;
        _playerSlidingImage.sprite = playerSlidingSprite;

        activeTransform.DOAnchorPosX(-25f, _moveDuration).SetEase(_moveEase);
        passiveTransform.DOAnchorPosX(-90f, _moveDuration).SetEase(_moveEase);
    }
    private IEnumerator SetBoosterUserInterfaces(RectTransform activeTransform, Image boosterImage, Image wheatImage,
        Sprite activeSprite, Sprite passiveSprite, Sprite activeWheatSprite, Sprite passiveWheatSprite, float duration)
    {
        //sprite ları açtım
        boosterImage.sprite = activeSprite;
        wheatImage.sprite = activeWheatSprite;
        activeTransform.DOAnchorPosX(25f, _moveDuration).SetEase(_moveEase);
        //belli süre bekledi
        yield return new WaitForSeconds(duration);
        //sprite ları kapattım
        boosterImage.sprite = passiveSprite;
        wheatImage.sprite = passiveWheatSprite;
        activeTransform.DOAnchorPosX(90f, _moveDuration).SetEase(_moveEase);
    }
    //kartları aktif pasif yapma
    public void PlayBoosterUIAnimations(RectTransform activeTransform, Image boosterImage, Image wheatImage,
        Sprite activeSprite, Sprite passiveSprite, Sprite activeWheatSprite, Sprite passiveWheatSprite, float duration)
    {
        StartCoroutine(SetBoosterUserInterfaces(activeTransform, boosterImage, wheatImage, activeSprite,
            passiveSprite, activeWheatSprite, passiveWheatSprite, duration));
    }
}
