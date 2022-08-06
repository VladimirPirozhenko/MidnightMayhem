using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using System.Collections;
using System.Linq;
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

    [ServerRpc(RequireOwnership = false)]
    public void RegisterPlayer(string clientId, Player player)
    {
       
        players.Add(clientId, player);
        UpdatePlayerCardsRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    public void UnregisterPlayerRpc(string clientId)
    {
        UnregisterPlayer(clientId);
    }

    [ObserversRpc(IncludeOwner = true, BufferLast = true)]
    public void UnregisterPlayer(string clientId)
    {
        players.Remove(clientId);
        RemovePlayerCardRpc(clientId);
        UpdatePlayerCardsRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    public void RemovePlayerCardRpc(string cardTag)
    {
        RemovePlayerCard(cardTag);
    }

    [ObserversRpc(IncludeOwner = true, BufferLast = true)]
    private void RemovePlayerCard(string cardTag)
    {
        ViewManager.Instance.TryGetView(out ScoreboardView scoreboardView);
        scoreboardView.RemovePlayerCard(cardTag);
    }
    //[ServerRpc(RequireOwnership = false)]
    //public void UnregisterPlayer(string clientId)
    //{
    //    players.Remove(clientId);
    //    UpdatePlayerCardsRpc();
    //}

    [ServerRpc(RequireOwnership = false)]
    public void UpdatePlayerCardsRpc()
    {
        var playersDict = players.GetCollection(true);
        var playersArr = playersDict.Values.ToArray();
        UpdatePlayerCards(playersArr);
    }

    [ObserversRpc(IncludeOwner = true, BufferLast = true)]
    private void UpdatePlayerCards(Player[] players)
    {
        ViewManager.Instance.TryGetView(out ScoreboardView scoreboardView);
        scoreboardView.RefreshPlayerCards(players);
    }

    [ServerRpc(RequireOwnership = false)]
    public void RefreshCardRpc(PlayerScoreboardCardData cardData)
    {
        RefreshCard(cardData);
    }

    [ObserversRpc(IncludeOwner = true, BufferLast = true)]
    private void RefreshCard(PlayerScoreboardCardData cardData)
    {
        ViewManager.Instance.TryGetView(out ScoreboardView scoreboardView);
        scoreboardView.RefreshPlayerCard(cardData);
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
