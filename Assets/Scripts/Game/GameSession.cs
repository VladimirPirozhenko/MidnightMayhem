using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class GameSession : NetworkBehaviour
{
    [SerializeField] Spawner spawner;

    [SerializeField]
    [SyncObject]
    public readonly SyncDictionary<string, Player> players = new SyncDictionary<string, Player>();
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
    public void ServerRegisterPlayer(string clientId, Player player)
    {
       
        players.Add(clientId, player);
        ServerRefreshPlayerCards();
    }

    [ServerRpc(RequireOwnership = false)]
    public void ServerUnregisterPlayer(string clientId)
    {
        players.Remove(clientId);
        ObserversUnregisterPlayer(clientId);
    }

    [ObserversRpc(IncludeOwner = true, BufferLast = false)]
    public void ObserversUnregisterPlayer(string clientId)
    {
        players.Remove(clientId);
        RemovePlayerCard(clientId);
    }

    private void RemovePlayerCard(string cardTag)
    {
        ScoreboardView.Instance.RemovePlayerCard(cardTag);  
    }


    [ServerRpc(RequireOwnership = false)]
    public void ServerRefreshPlayerCards()
    {
        var playersDict = players.GetCollection(true);
        var playersArr = playersDict.Values.ToArray();
        List<PlayerScoreboardCardData> scoreboardCardsData = new List<PlayerScoreboardCardData>();
        foreach (var playerItem in players)
        {
            Player player = playerItem.Value;
            PlayerScoreboardCardData cardData = new PlayerScoreboardCardData(player.Tag, player.PlayerStatistics.Score.ToString());
            scoreboardCardsData.Add(cardData);
            
        }
        ObserversRefreshPlayerCards(scoreboardCardsData);
    }

    [ObserversRpc(IncludeOwner = true, BufferLast = true)]
    private void ObserversRefreshPlayerCards(List<PlayerScoreboardCardData> cardsData)
    {
        
        ScoreboardView.Instance.RefreshPlayerCards(cardsData);    
    }

    [ServerRpc(RequireOwnership = false)]
    public void ServerRefreshCardRpc(PlayerScoreboardCardData cardData)
    {
        ObserversRefreshCard(cardData);
        ServerRefreshPlayerCards();
    }

    [ObserversRpc(IncludeOwner = true, BufferLast = true)]
    private void ObserversRefreshCard(PlayerScoreboardCardData cardData)
    {
        ScoreboardView.Instance.RefreshPlayerCard(cardData);
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
