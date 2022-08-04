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
    // [SyncVar(OnChange = nameof(OnScoreChanged))] private int score;

    //[field: SerializeField]
    ////[field: SyncVar]
    //public int score
    //{
    //    get;

    //    [ServerRpc]
    //    set;
    //}
    //[SerializeField] private GameObject cardPrefab;
    [field: SerializeField] public int Score { get; private set; }
    [SerializeField] private TMP_Text scoreText;
    public override void OnStartClient()
    {
        base.OnStartClient();
        scoreText.gameObject.SetActive(false);
        //this.GetComponentInChildren<VerticalLayoutGroup>().gameObject.SetActive(false); 
        if (!IsOwner)
            return;

        scoreText.text = Score.ToString();
        
        scoreText.gameObject.SetActive(true);
        //this.GetComponentInChildren<VerticalLayoutGroup>().gameObject.SetActive(true);
        //var playerCards = new List<GameObject>(12);
        //Transform parentForCards = this.GetComponentInChildren<VerticalLayoutGroup>().transform;
        //for (int i = 0; i < Lobby.Instance.players.Count; i++)
        //{
        //    GameObject card = Instantiate(cardPrefab);
        //    card.transform.SetParent(parentForCards, false);
        //    playerCards.Add(card);
        //}

        // scoreText.gameObject.SetActive(false);
        //scoreText = FindObjectOfType<ScoreboardView>().GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        if (!IsOwner)
            return;
        if (Input.GetKeyUp(KeyCode.Space))
             TargetAddScore(Owner,1);
        // if (Input.GetKeyDown(KeyCode.Tab))
        //     ViewManager.Instance.Show<ScoreboardView>(true);
        if (Input.GetKeyDown(KeyCode.Tab))
            ViewManager.Instance.Show<ScoreboardView>(true);
        if (Input.GetKeyUp(KeyCode.Tab))
            ViewManager.Instance.Show<ScoreboardView>(false);
        //scoreboardView.Show(false);
        //scoreboardView.gameObject.SetActive(false);
    }
    [ServerRpc]
    public void ServerAddScore(int amount)
    {
       // TargetAddScore(connection, amount);
       ObserversAddScore(amount);
    }
    [TargetRpc]
    public void TargetAddScore(NetworkConnection connection,int amount)
    {
        //if (!IsOwner)
        //    return;
        //this.GiveOwnership(LocalConnection);    // gives object ownership to the client that calls the method
        Score += amount;
        scoreText.text = Score.ToString();
       // ScoreboardViewData scoreViewData = new ScoreboardViewData(Score.ToString());
        //scoreView.UpdateView(scoreViewData);
        //OnScoreChanged?.Invoke(score);
    }
    [ObserversRpc(BufferLast = true)]
    public void ObserversAddScore(int amount)
    {
        //if (!IsOwner)
        //    return;
        //this.GiveOwnership(LocalConnection);    // gives object ownership to the client that calls the method
        Score += amount;
        scoreText.text = Score.ToString();

        // ScoreboardViewData scoreViewData = new ScoreboardViewData(Score.ToString());
        //scoreView.UpdateView(scoreViewData);
        //OnScoreChanged?.Invoke(score);
    }
    void OnScoreChanged(int prev,int next,bool onServer)
    {
        //scoreTextUI.text = next.ToString();    
        //ScoreboardViewData scoreViewData = new ScoreboardViewData(next.ToString());
        //scoreView.UpdateView(scoreViewData);
    }
}
//public class PlayerStatistics : NetworkBehaviour
//{
//    // [SyncVar(OnChange = nameof(OnScoreChanged))] private int score;

//    [field: SerializeField]
//    [field: SerializeField]
//    public int Score
//    {
//        get;

//        [ServerRpc]
//        set;
//    }
//    // [SerializeField] private ScoreboardView scoreView;
//    [SerializeField] private TMP_Text scoreText;
//    public override void OnStartClient()
//    {
//        base.OnStartClient();
//        if (!IsOwner)
//            return;
//        ViewManager.Instance.InitAllViews();
//    }
//    private void Update()
//    {
//        if (!IsOwner)
//            return;
//        if (Input.GetKeyUp(KeyCode.Space))
//            AddScore(1);
//    }
//    public void AddScore(int amount)
//    {
//        Score += amount;
//        ScoreboardViewData scoreViewData = new ScoreboardViewData(Score.ToString());
//        scoreText.text = Score.ToString();//.UpdateView(scoreViewData);
//        //OnScoreChanged?.Invoke(score);
//    }
//    void OnScoreChanged(int prev, int next, bool onServer)
//    {
//        // scoreTextUI.text = next.ToString();    
//        //ScoreboardViewData scoreViewData = new ScoreboardViewData(next.ToString());
//        //scoreView.UpdateView(scoreViewData);
//    }
//}
