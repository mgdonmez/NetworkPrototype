using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using static Fusion.NetworkBehaviour;

public class BallStateViewer : NetworkBehaviour, ISpawned
{
    [Networked] public string state {  get; private set; }
 
    [SerializeField] private TextMeshProUGUI _stateLabel;
    private ChangeDetector _changeDetector;

    public override void Spawned()
    {
        _changeDetector = GetChangeDetector(ChangeDetector.Source.SimulationState);
    }
    public void UpdateStateLabel()
    {
        _stateLabel.text = state;
    }
    public override void Render()
    {
        foreach (var change in _changeDetector.DetectChanges(this, out var previous, out var next))
        {
            switch (change)
            {
                case nameof(state):
                    UpdateStateLabel();
                    break;
            }
        }
    }
    public override void FixedUpdateNetwork()
    {
        if (!HasStateAuthority) return;

        if(GameManager.Instance.ballStateManager.currentState != null)
            state = GameManager.Instance.ballStateManager.currentState.ToString();
    }
}
