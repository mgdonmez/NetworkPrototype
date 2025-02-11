using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            Debug.Log("Connected to server, spawning local player...");
            GameManager.Instance.SetPlayerName(GameManager.Instance.playerNames[Random.Range(0, GameManager.Instance.playerNames.Count)].ToString());
            Runner.Spawn(GameManager.Instance.PlayerPrefab, new Vector3(0, 2, 0), Quaternion.identity);
        }
        else
        {
            Debug.Log("Player joined, spawning new player...");
        }
    }
}
