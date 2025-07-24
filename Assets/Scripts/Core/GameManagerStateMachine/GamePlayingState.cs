using UnityEngine;

public class GamePlayingState : GameManagerState
{
    public GamePlayingState(GameManagerStateMachine gameManagerStateMachine) : base(gameManagerStateMachine)
    {

    }

    public override void Update()
    {
        base.Update();
        // score check
        Debug.Log("Checking player score...");

        // life check
        Debug.Log("Checking player health...");
    }
}
