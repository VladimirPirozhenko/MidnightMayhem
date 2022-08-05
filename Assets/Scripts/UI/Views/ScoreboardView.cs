using FishNet.Object;
using FishNet.Object.Synchronizing;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//public struct ScoreboardViewData 
//{
//    public string scoreText;
//    public ScoreboardViewData(string text)
//    {
//        scoreText = text;   
//    }
//}
public class ScoreboardView : BaseView
{
    [SerializeField] PlayerScoreboardCard cardPrefab;
    private readonly Dictionary<string,PlayerScoreboardCard> playerCards = new Dictionary<string, PlayerScoreboardCard>();
    public override void Init()
    {
        base.Init();
        this.gameObject.SetActive(false);
        Show(false);
    }
    public void RefreshPlayerCards(Player[] players)
    {

        foreach (var player in players)
        {
            PlayerScoreboardCardData cardData = new PlayerScoreboardCardData(player.Tag, player.PlayerStatistics.Score.ToString());
            //player.Value.AddPlayerCard(cardData);
           // ViewManager.Instance.TryGetView(out ScoreboardView scoreboardView);
            AddPlayerCard(cardData);
            Show(false);
            gameObject.SetActive(false);
        }
    }
       
    public void AddPlayerCard(PlayerScoreboardCardData cardData)
    {
        if (playerCards.ContainsKey(cardData.playerName))
            return;
        PlayerScoreboardCard playerScoreboardCard = Instantiate(cardPrefab);
        playerScoreboardCard.transform.SetParent(this.transform, false);
        playerScoreboardCard.UpdateCard(cardData);   
        playerCards.Add(cardData.playerName, playerScoreboardCard);
    }
    public void UpdateView(PlayerScoreboardCardData cardData)
    {
        playerCards.TryGetValue(cardData.playerName, out PlayerScoreboardCard card);
        //GameSession.Instance.TryGetPlayerByTag(viewTag,out Player player);
        //PlayerScoreboardCardData cardData = new PlayerScoreboardCardData(player);
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
