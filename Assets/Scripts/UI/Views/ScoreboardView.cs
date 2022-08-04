using FishNet.Object;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public struct ScoreboardViewData 
{
    public string scoreText;
    public ScoreboardViewData(string text)
    {
        scoreText = text;   
    }
}
public class ScoreboardView : BaseView
{
    [SerializeField] PlayerScoreboardCard cardPrefab;
    private readonly Dictionary<string,PlayerScoreboardCard> playerCards = new Dictionary<string, PlayerScoreboardCard>();
    public override void Init()
    {
        base.Init();
        this.gameObject.SetActive(false);
        Show(false);
        //var players = GameSession.Instance.GetPlayersDict();
        //foreach (var player in players)
        //{
        //    PlayerScoreboardCardData cardData = new PlayerScoreboardCardData(player.Value);
        //    PlayerScoreboardCard playerScoreboardCard = Instantiate(cardPrefab);
        //    playerScoreboardCard.UpdateCard(cardData);  
        //    playerCards.Add(player.Key, playerScoreboardCard);
        //}
    }
    //[ServerRpc]
    public void AddPlayerCardServer(Player player)
    {
        AddPlayerCard(player);
    }
    //[ObserversRpc(BufferLast = true)]
    public void AddPlayerCard(Player player)
    {
        PlayerScoreboardCardData cardData = new PlayerScoreboardCardData(player);
        PlayerScoreboardCard playerScoreboardCard = Instantiate(cardPrefab);
        //Transform parentForCards = this.GetComponentInChildren<VerticalLayoutGroup>().transform;
        playerScoreboardCard.transform.SetParent(this.transform, false);
        playerScoreboardCard.UpdateCard(cardData);  
        playerCards.Add(player.Tag, playerScoreboardCard);
    }
    public void UpdateView(string viewTag)
    {
        playerCards.TryGetValue(viewTag, out PlayerScoreboardCard card);
        GameSession.Instance.TryGetPlayerByTag(viewTag,out Player player);
        PlayerScoreboardCardData cardData = new PlayerScoreboardCardData(player);
        card.UpdateCard(cardData);
        //
        //foreach (var card in playerCards)
        //{
        //    card.UpdateCard(cardData);
        //}
        //for (int i = 0; i < playerCards.Count; i++)
        //{

        //   // PlayerScoreboardCardData cardData = new PlayerScoreboardCardData(data.scoreText,);
        //    //playerCards[i].UpdateCard();
        //}
    }
}
