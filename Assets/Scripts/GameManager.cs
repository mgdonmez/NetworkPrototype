using Fusion;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SimulationBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameManager();
            return _instance;
        }
    }

    [Header("Network References")]
    public NetworkRunner networkRunner;

    [Header("Object Spawn Variables")]
    public GameObject BallPrefab;
    public GameObject ButtonPrefab;
    public Transform BallSpawnPointTransform;
    public Transform ButtonSpawnPointTransform;

    [Header("Ball References")]
    public GameObject ballTargetGameObject;
    public GameObject ball;
    [Networked] public NetworkTransform ballTargetTransform { get; set; }

    [Header("Ball State References")]
    public BallStateManager ballStateManager;
    public BallBaseState currentState;

    [Header("Player References")]
    public GameObject PlayerPrefab;
    public string _playername = null;
    public List<NetworkString<_32>> playerNames = new List<NetworkString<_32>>() { "Michael", "Pam", "Jim", "Dwight", "Stanley", "Kevin", "Angela", "Meredith", "Phyllis" };

    private void Awake()
    {
        _instance = this;
        ballTargetTransform = ballTargetGameObject.GetComponent<NetworkTransform>();
    }

    public void SetPlayerName(string name)
    {
        _playername = name;
    }
}
