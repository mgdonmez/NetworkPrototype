using Fusion;
using System;
using System.Collections;
using System.Linq;
using Timeline.Samples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class GrabbableObject : NetworkBehaviour
{
    [Header("Color")] public MeshRenderer MeshRenderer;
    [Networked] public Color NetworkedColor { get; set; }
    [Networked] public Vector3 destinationPos { get; set; }
    [Networked] public NetworkBool isParticleVFXPlaying { get; set; }
    [Networked] public NetworkBool isGrabbed { get; set; }
    [SerializeField] public NetworkBool isGrabbable { get; set; }
    public ParticleSystem particleVFX { get; set; }

    private Color _grabbedColor = Color.green;
    private Color _droppedColor = Color.red;

    private ChangeDetector _changeDetector;
    private Transform _grabPointTransform;

    public override void Spawned()
    {
        base.Spawned();

        _changeDetector = GetChangeDetector(ChangeDetector.Source.SimulationState);

        particleVFX = GetComponentInChildren<ParticleSystem>();
        destinationPos = GameManager.Instance.BallSpawnPointTransform.position;
        isParticleVFXPlaying = false;
        NetworkedColor = _droppedColor;
        MeshRenderer.material.color = _droppedColor;
        GameManager.Instance.ball = gameObject;
        GameManager.Instance.ballStateManager.StartManager();
        //GameManager.Instance.ballStateManager = GetComponent<BallStateManager>();

        var timelineAsset = GameManager.Instance.ball.GetComponent<PlayableDirector>().playableAsset as TimelineAsset;

        // get the list of output tracks of the target type
        var tracks = timelineAsset.GetOutputTracks().OfType<TweenTrack>();
        foreach (var track in tracks)
        {
            Debug.Log(track.name);

            // go through all the clips
            foreach (var clip in track.GetClips())
            {
                Debug.Log(clip.displayName);
                var tweenClip = clip.asset as TweenClip;
                if (tweenClip != null)
                {
                    // grab the property name from the clip
                    PropertyName prop = tweenClip.endLocation.exposedName;
                    GameManager.Instance.ball.GetComponent<PlayableDirector>().SetReferenceValue(prop, GameManager.Instance.ballTargetGameObject.transform);
                }
            }
        }
    }
    public void Grab(Transform grabPointTransform)
    {
        this._grabPointTransform = grabPointTransform;
        isGrabbed = true;
        destinationPos = grabPointTransform.position;
        NetworkedColor = _grabbedColor;
    }

    public override void Render()
    {
        foreach (var change in _changeDetector.DetectChanges(this, out var previous, out var next))
        {
            switch (change)
            {
                case nameof(NetworkedColor):
                    ColorChanged();
                    break;
                case nameof(destinationPos):
                    StartCoroutine(MoveToPos());
                    break;
                case nameof(isGrabbed):
                    if (isGrabbed)
                    {
                        ParticleVFXTriggered();
                    }
                    break;
            }
        }
    }

    public void Drop()
    {
        this._grabPointTransform = null;
        isGrabbed = false;
        destinationPos = GameManager.Instance.BallSpawnPointTransform.position;
        NetworkedColor = _droppedColor;
    }

    public override void FixedUpdateNetwork()
    {
        if (!HasStateAuthority) return;
        if (_grabPointTransform != null)
        {
            destinationPos = _grabPointTransform.position;
        }
        else
        {
            destinationPos = GameManager.Instance.BallSpawnPointTransform.position;
        }
        if(isParticleVFXPlaying && !isGrabbed)
        {
            isParticleVFXPlaying = false;
        }
        if(!isParticleVFXPlaying && isGrabbed)
        {
            isParticleVFXPlaying = false;
        }
    }

    private void ColorChanged()
    {
        MeshRenderer.material.color = NetworkedColor;
    }
    private void ParticleVFXTriggered()
    {
        if(!isParticleVFXPlaying)
            particleVFX.Play();
    }
    private IEnumerator MoveToPos()
    {
        Vector3 movementVec = destinationPos - transform.position;
        float lerpSpeed = movementVec.magnitude;
        while(transform.position !=  destinationPos)
        {
            transform.position = Vector3.Lerp(transform.position, destinationPos, Time.deltaTime * lerpSpeed);
            yield return null;
        }
        yield return null;
    }
}
