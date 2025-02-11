using Fusion;
using UnityEngine;
using UnityEngine.Playables;

public class BallIdleState: BallBaseState
{
    private bool _atDefaultPos;
    public override void EnterState(BallStateManager ball)
    {
        _atDefaultPos = false;
        GameManager.Instance.ball.GetComponent<PlayableDirector>().Stop();
        GameManager.Instance.ball.GetComponent<GrabbableObject>().isGrabbable = true;
        GameManager.Instance.ball.GetComponent<Collider>().enabled = true;

        //NetworkTransform targetTransform = GameManager.Instance.ballTargetTransform;
        //Vector3 pos = GameManager.Instance.BallSpawnPointTransform.position;
        //targetTransform.Teleport(pos);
        GameManager.Instance.ballTargetTransform.Teleport(GameManager.Instance.BallSpawnPointTransform.position);
    }
    public override void UpdateState(BallStateManager ball)
    {
        if (!_atDefaultPos)
        {
            if (GameManager.Instance.ball.transform.position != GameManager.Instance.ballTargetGameObject.transform.position)
            {
                GameManager.Instance.ball.GetComponent<PlayableDirector>().Play();
            }
            else
            {
                GameManager.Instance.ball.GetComponent<PlayableDirector>().Stop();
                _atDefaultPos = true;
            }
        }
    }
}
