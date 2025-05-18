using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LosePopup : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private Button _tryAgainButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private TimerUI _timerUI;

    private void OnEnable()
    {
        _timerText.text = _timerUI.GetFinalTime();
        _tryAgainButton.onClick.AddListener(OnTryAgainButtonClicked);
    }
    private void OnTryAgainButtonClicked()
    {
        SceneManager.LoadScene(Consts.SceneNames.GAME_SCENE);
    }
}
