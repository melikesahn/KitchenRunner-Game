using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Image[] _playerHealthImages;

    [SerializeField] private Sprite _playerHealthySprite;
    [SerializeField] private Sprite _playerUnhealthySprite;
    [SerializeField] private float _scaleDuration;

    private RectTransform[] _playerHealthTransforms;
    private void Awake()
    {
        _playerHealthTransforms = new RectTransform[_playerHealthImages.Length];

        for (int i = 0; i < _playerHealthImages.Length; ++i)
        {
            _playerHealthTransforms[i] = _playerHealthImages[i].gameObject.GetComponent<RectTransform>();
        }
    }
    //test için
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            AnimateDamage();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            AnimateDamageForAll();
        }
    }

    public void AnimateDamage()
    {
        for (int i = 0; i < _playerHealthImages.Length; ++i)
        {
            if (_playerHealthImages[i].sprite == _playerHealthySprite)
            {
                AnimateDamageSprite(_playerHealthImages[i], _playerHealthTransforms[i]);
                break;
            }
        }
    }
    // 3 canı da bitirme
    public void AnimateDamageForAll()
    {
        for (int i = 0; i < _playerHealthImages.Length; ++i)
        {
            AnimateDamageSprite(_playerHealthImages[i], _playerHealthTransforms[i]);
        }
    }
    
    private void AnimateDamageSprite(Image activeImage, RectTransform activeImageTransform)
    {
        activeImageTransform.DOScale(0f, _scaleDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            activeImage.sprite = _playerUnhealthySprite;
            activeImageTransform.DOScale(1f, _scaleDuration).SetEase(Ease.OutBack);
        });
    }
}
