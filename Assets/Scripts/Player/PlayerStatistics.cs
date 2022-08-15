using FishNet;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatistics : NetworkBehaviour
{
    [field: SerializeField] [field: SyncVar(OnChange = nameof(OnScoreChanged))] public int Score { get; private set; }

    [SerializeField] private PlayerHUDView playerHUDViewPrefab;
    private PlayerHUDView playerHUDView; 

    [SyncVar] private Player player;
    private void Awake()
    {
        player = GetComponent<Player>();    
    }
    public override void OnStartClient()
    {
        base.OnStartClient();
        playerHUDView = Instantiate(playerHUDViewPrefab, Vector3.zero, Quaternion.identity);
        playerHUDView.transform.SetParent(PlayerHUDCanvas.Instance.transform, false);
        ViewManager.Instance.Add(playerHUDView);
        playerHUDView.Show(false);  
        if (!IsOwner)
            return;
        playerHUDView.UpdateScore(Score.ToString());
        playerHUDView.Show(true);
    }
    private void Update()
    {
        if (!IsOwner)
            return;

        bool showScoreboard = player.InputReader.IsShowScoreboardButtonPressed();

        if (showScoreboard)
            ViewManager.Instance.Show<ScoreboardView>(true);
        else
            ViewManager.Instance.Show<ScoreboardView>(false);
    }

    [ServerRpc]
    public void ServerAddScore(int amount)
    {
       ObserversAddScore(amount);
    }

    [ObserversRpc(BufferLast = false,IncludeOwner = true)]
    public void ObserversAddScore(int amount)
    {
        Score += amount;
    }
    void OnScoreChanged(int prev,int next,bool onServer)
    {
        playerHUDView.UpdateScore(next.ToString()); 
        //scoreText.text = next.ToString();
        PlayerScoreboardCardData cardData = new PlayerScoreboardCardData(player.Tag, Score.ToString());
        GameSession.Instance.ServerRefreshCardRpc(cardData);
    }
}
