using UnityEngine;

public class GamePlayingState : GameManagerState
{
    public GamePlayingState(GameManagerStateMachine gameManagerStateMachine) : base(gameManagerStateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        GameManager.Instance.LoadScene(GameManager.Instance.currentSceneName);
    }

    public override void Update()
    {
        base.Update();
        // score check
        Debug.Log("Checking player score...");

        // life check
        Debug.Log("Checking player health...");
    }

    public override void Exit()
    {
        base.Exit();
        GameManager.Instance.UnloadCurrentScene();
    }
}
