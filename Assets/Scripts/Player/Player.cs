using System.Collections;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;

[RequireComponent(typeof(PlayerStatistics))]  
public class Player : NetworkBehaviour
{
    //public static Player Instance { get; private set; }
    [field: SyncVar]
    public string userName { get; private set; }   
    public PlayerStatistics PlayerStatistics { get; private set; }  

    //[field: SerializeField]
    //public GameSession Session { get; private set; }    
    [field:SyncVar]
    public string Tag { get; private set; }

    private void Awake()
    {
        PlayerStatistics = GetComponent<PlayerStatistics>();
    }
    public override void OnStartServer()
    {
        base.OnStartServer();
        Tag = this.Owner.ClientId.ToString();
        GameSession.Instance.RegisterPlayer(Tag, this);
        //GameSession.Instance.RegisterPlayer(Tag, this);
    }
    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {
            //if (!IsServer)
            //{
            //    Tag = this.Owner.ClientId.ToString();
            //    GameSession.Instance.RegisterPlayer(Tag, this);
            //}
       
            //GameSession.Instance.RegisterPlayer(Tag, this);
            // ViewManager.Instance.InitAllViews();
            //ViewManager.Instance.TryGetView(out ScoreboardView scoreboardView);
            //scoreboardView.AddPlayerCard(this);
            //var players = GameSession.Instance.GetPlayersDict();
            //foreach (var player in players)
            //{
            //    PlayerScoreboardCardData cardData = new PlayerScoreboardCardData(player.Value.Tag, player.Value.PlayerStatistics.Score.ToString());
            //    AddPlayerCardServerRpc(cardData);
            //}
            //GameSession.Instance.UpdatePlayerCardsRpc();
            //Lobby.Instance.AddPlayerToLobby(this.GetComponent<Player>());
        }
        {
            gameObject.GetComponent<Player>().enabled = false;

        }
    }

    //[ServerRpc]
    //public void RegisterPlayerRpc()
    //{
    //    RegisterPlayer();
    //}
    //[ObserversRpc]
    //public void RegisterPlayer()
    //{
    //    GameSession.Instance.RegisterPlayer(Tag, this);
    //}
    //[ObserversRpc]
    //public void AddPlayerCard(PlayerScoreboardCardData cardData)
    //{
    //    ViewManager.Instance.TryGetView(out ScoreboardView scoreboardView);
    //    scoreboardView.AddPlayerCard(cardData);
    //    scoreboardView.Show(false);
    //    scoreboardView.gameObject.SetActive(false);
    //}

}
