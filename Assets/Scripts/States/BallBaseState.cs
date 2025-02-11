using UnityEngine;

public abstract class BallBaseState
{
    public abstract void EnterState(BallStateManager ball);
    public abstract void UpdateState(BallStateManager ball);

}
