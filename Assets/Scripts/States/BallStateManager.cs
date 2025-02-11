using TMPro;
using UnityEngine;

public class BallStateManager : MonoBehaviour
{
    public BallBaseState currentState;
    public BallIdleState idleState = new BallIdleState();
    public BallActiveState activeState = new BallActiveState();
    private bool _isStarted = false;
    private void Start()
    {
        currentState = idleState;
        //currentState.EnterState(this);
        GameManager.Instance.currentState = currentState;
    }
    public void StartManager()
    {
        //currentState = idleState;
        currentState.EnterState(this);
        //GameManager.Instance.currentState = currentState;
        _isStarted = true;
    }
    private void Update()
    {
        if (_isStarted)
        {
            currentState.UpdateState(this);
        }
    }
    public void SwitchState()
    {
        if(currentState == idleState)
        {
            currentState = activeState;
        }
        else
        {
            currentState = idleState;
        }
        GameManager.Instance.currentState = currentState;
        currentState.EnterState(this);
    }
}
