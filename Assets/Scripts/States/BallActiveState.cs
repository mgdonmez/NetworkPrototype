using Timeline.Samples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class BallActiveState: BallBaseState
{
    public override void EnterState(BallStateManager ball)
    {
        GameManager.Instance.ball.GetComponent<GrabbableObject>().isGrabbable = false;
        GameManager.Instance.ball.GetComponent<PlayableDirector>().Play();
        GameManager.Instance.ball.GetComponent<Collider>().enabled = false;
    }
    public override void UpdateState(BallStateManager ball)
    {
        if (GameManager.Instance.ball.GetComponent<PlayableDirector>().state != PlayState.Playing)
        {
            GameManager.Instance.ballTargetTransform.Teleport(new Vector3(Random.Range(-25.5f, 25.5f), Random.Range(0f, 10f), Random.Range(-25.5f, 25.5f)));
            GameManager.Instance.ball.GetComponent<PlayableDirector>().Play();
        }
    }
}
