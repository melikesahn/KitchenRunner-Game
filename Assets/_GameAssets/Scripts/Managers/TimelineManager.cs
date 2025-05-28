using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    private PlayableDirector _playableDirector;

    [SerializeField] private GameManager _gameManager;

   

    private void Awake() 
    {
        _playableDirector = GetComponent<PlayableDirector>();    
    }

    private void OnEnable() 
    {
        _playableDirector.Play();
        _playableDirector.stopped += OnTimelineFinished;   
    }

    private void OnTimelineFinished(PlayableDirector director)
    {
        _gameManager.ChangeGameState(GameState.Play);
    }

    private void OnDisable() 
    {
        _playableDirector.stopped -= OnTimelineFinished;
    }
}
