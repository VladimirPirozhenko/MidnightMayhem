using System.Collections;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;

[RequireComponent(typeof(PlayerStatistics))]  
public class Player : NetworkBehaviour
{
    //public static Player Instance { get; private set; }
    public string userName { get; private set; }   
    public PlayerStatistics PlayerStatistics { get; private set; }  

    //[field: SerializeField]
    //public GameSession Session { get; private set; }    
    public string Tag { get; private set; }

    private void Awake()
    {
        PlayerStatistics = GetComponent<PlayerStatistics>();
    }
    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {
            Tag = this.Owner.ClientId.ToString();  
            GameSession.Instance.RegisterPlayer(Tag, this);
            ViewManager.Instance.InitAllViews();
            ViewManager.Instance.TryGetView(out ScoreboardView scoreboardView);
            scoreboardView.AddPlayerCard(this);
            scoreboardView.Show(false);
            scoreboardView.gameObject.SetActive(false); 
            //Lobby.Instance.AddPlayerToLobby(this.GetComponent<Player>());
        }
        {
            gameObject.GetComponent<Player>().enabled = false;
        }
    }

}
