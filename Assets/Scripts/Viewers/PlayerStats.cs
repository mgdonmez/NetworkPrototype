using UnityEngine;
using Fusion;
using TMPro;
using System.Collections;

public class PlayerStats : NetworkBehaviour, ISpawned
{
    [Networked] public string PlayerName {  get; set; }
    [SerializeField] private TextMeshProUGUI _playerNameLabel;
    private ChangeDetector _changeDetector;

    public override void Spawned()
    {
        _changeDetector = GetChangeDetector(ChangeDetector.Source.SimulationState);
    }
    private IEnumerator Start()
    {
        yield return new WaitUntil(() => this.isActiveAndEnabled);
        yield return new WaitUntil(() => this.HasStateAuthority);
        PlayerName = GameManager.Instance._playername;

        yield return new WaitUntil(() => PlayerName.ToString() != null);

        UpdatePlayerName();
    }

    public override void Render()
    {
        foreach (var change in _changeDetector.DetectChanges(this, out var previous, out var next))
        {
            switch (change)
            {
                case nameof(PlayerName):
                    UpdatePlayerName(PlayerName);
                    break;
            }
        }
    }

    public override void FixedUpdateNetwork()
    {
        RPC_Configure(PlayerName);
    }
    private void UpdatePlayerName(string newName)
    {
        _playerNameLabel.text = newName;
    }
    private void UpdatePlayerName()
    {
        _playerNameLabel.text = PlayerName;
    }
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_Configure(string name)
    {
        PlayerName = name;
        _playerNameLabel.text = PlayerName;
    }
}
