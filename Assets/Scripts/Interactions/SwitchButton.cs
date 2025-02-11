using Fusion;
using UnityEngine;
using static Fusion.NetworkBehaviour;

public class SwitchButton : NetworkBehaviour
{
    [Networked]  public NetworkBool isSwitchActive {  get; set; }
    private ChangeDetector changeDetector;
    public override void Spawned()
    {
        changeDetector = GetChangeDetector(ChangeDetector.Source.SimulationState);
        isSwitchActive = false;
        base.Spawned();
    }

    public override void Render()
    {
        foreach (var change in changeDetector.DetectChanges(this, out var previous, out var next))
        {
            switch (change)
            {
                case nameof(isSwitchActive):
                    SwitchChanged();
                    break;
            }
        }
    }
    public void ToggleSwitchActive()
    {
        isSwitchActive = !isSwitchActive;
    }
    private void SwitchChanged()
    {
        GameManager.Instance.ballStateManager.SwitchState();
    }
}
