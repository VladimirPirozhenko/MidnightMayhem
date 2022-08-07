using FishNet.Object;
using FishNet.Object.Synchronizing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby : NetworkBehaviour
{
    public static Lobby Instance { get; private set; } 
    [SyncObject]
    [SerializeField]
    public readonly SyncList<Player> players = new SyncList<Player>();

    public override void OnStartNetwork()
    {
        base.OnStartNetwork();
        Instance = this;
    }
    public override void OnStartServer()
    {
        base.OnStartServer();

    }
    public override void OnStartClient()
    {
        base.OnStartClient();

    }
    public void AddPlayerToLobby(Player player)
    {
        players.Add(player);  
        if (IsServer)
        {
            Debug.Log(players);
        }
    }
}
