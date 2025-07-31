using UnityEngine;

public class GameManagerStateMachine : StateMachine
{
    public GameStartState gameStartState { get; }
    public GamePlayingState gamePlayingState { get; }
    public GamePausedState GamePausedState { get; }
    public GameOverState gameOverState { get; }
    public GameMainMenuState gameMainMenuState { get; }

    public GameManagerStateMachine()
    {
        gameStartState = new(this);
        gamePlayingState = new(this);
        GamePausedState = new(this);
        gameOverState = new(this);
        gameMainMenuState = new(this);
    }
}
