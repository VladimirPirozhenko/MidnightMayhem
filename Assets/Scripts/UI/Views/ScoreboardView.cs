using System.Collections.Generic;
using UnityEngine;

public class ScoreboardView : BaseView
{
    [SerializeField] PlayerScoreboardCard cardPrefab;
    private readonly Dictionary<string,PlayerScoreboardCard> playerCards = new Dictionary<string, PlayerScoreboardCard>();
    public void RefreshPlayerCards(Player[] players)
    {
        //foreach (var playerCard in playerCards)

        //playerCards.TryGetValue(cardData.playerName, out PlayerScoreboardCard card);
        foreach (var player in players)
        {
            PlayerScoreboardCardData cardData = new PlayerScoreboardCardData(player.Tag, player.PlayerStatistics.Score.ToString());
            AddPlayerCard(cardData);
                    //Show(false);
            //gameObject.SetActive(false);
        }
    }
       
    private void AddPlayerCard(PlayerScoreboardCardData cardData)
    {
        if (playerCards.ContainsKey(cardData.playerName))
            return;
        PlayerScoreboardCard playerScoreboardCard = Instantiate(cardPrefab);
        playerScoreboardCard.transform.SetParent(this.transform, false);
        playerScoreboardCard.UpdateCard(cardData);   
        playerCards.Add(cardData.playerName, playerScoreboardCard);
    }
    public void RemovePlayerCard(string cardTag)
    {
        if (playerCards.ContainsKey(cardTag))
        {
            playerCards.Remove(cardTag);
        }
    }
    public void RefreshPlayerCard(PlayerScoreboardCardData cardData)
    {
        if (playerCards.TryGetValue(cardData.playerName, out PlayerScoreboardCard card))
            card.UpdateCard(cardData);
    }
}
