using Fusion;
using System.Collections.Generic;
using System;
using UnityEngine;
using Fusion.Sockets;

public class ObjectSpawner: SimulationBehaviour, INetworkRunnerCallbacks
{
    NetworkRunner _networkRunner;
    void Awake()
    {
        if (_networkRunner != null)
        {
            _networkRunner.AddCallbacks(this);
        }
    }
    public void OnSceneLoadDone(NetworkRunner runner)
    {
        if (runner.IsSceneAuthority)
        {
            Debug.Log("Connected to server, spawning ball and button...");
            _networkRunner = runner;
            SpawnBall();
            SpawnButton();
        }
    }
    public void SpawnBall()
    {
        _networkRunner.Spawn(GameManager.Instance.BallPrefab, GameManager.Instance.BallSpawnPointTransform.position, Quaternion.identity);
    }
    public void SpawnButton()
    {
        _networkRunner.Spawn(GameManager.Instance.ButtonPrefab, GameManager.Instance.ButtonSpawnPointTransform.position, Quaternion.identity);
    }

    // Unused callbacks (required by the interface)
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) { }
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }
    public void OnInput(NetworkRunner runner, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason reason) { }
    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason netDisconnectReason) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress netAddress, NetConnectFailedReason netConnectFailedReason) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }
    //public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject networkObject, PlayerRef playerRef) { }
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject networkObject, PlayerRef playerRef) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr simulationMessagePtr) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef playerRef, ReliableKey reliableKey, ArraySegment<byte> data) { }
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef playerRef, ReliableKey reliableKey, float f) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef playerRef, NetworkInput networkInput) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionInfos) { }

}
