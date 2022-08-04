using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using System.Collections;
using UnityEngine;


public class GameSession : NetworkBehaviour
{
    [SerializeField] Spawner spawner;

    [SerializeField]
    [SyncObject]
    private readonly SyncDictionary<string, Player> players = new SyncDictionary<string, Player>();
    public static GameSession Instance { get; private set; }
    public override void OnStartNetwork()
    {
        base.OnStartNetwork();
        Instance = this;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        for (int i = 0; i < 100; i++)
            spawner.Spawn();
    }
 
    public void RegisterPlayer(string clientId,Player player)
    {
        players.Add(clientId, player);
    }
    public SyncDictionary<string, Player> GetPlayersDict()
    {
        return players;
    }
    public bool TryGetPlayerByTag(string tag,out Player player)
    {
        if (players.TryGetValue(tag, out player))
        {
           return true;
        }
        else
        {
            return false;
        }
    }
}
