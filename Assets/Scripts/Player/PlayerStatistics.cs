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

    [SerializeField] private TMP_Text scoreText;

    private Player player;
    private void Awake()
    {
        player = GetComponent<Player>();    
    }
    public override void OnStartClient()
    {
        base.OnStartClient();
        scoreText.gameObject.SetActive(false);
        if (!IsOwner)
            return;
        scoreText.text = Score.ToString();
        scoreText.gameObject.SetActive(true);
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

    [ObserversRpc(BufferLast = true)]
    public void ObserversAddScore(int amount)
    {
        Score += amount;
    }
    void OnScoreChanged(int prev,int next,bool onServer)
    {
        scoreText.text = next.ToString();
        PlayerScoreboardCardData cardData = new PlayerScoreboardCardData(player.Tag, Score.ToString());
        GameSession.Instance.RefreshCardRpc(cardData);
    }
}
