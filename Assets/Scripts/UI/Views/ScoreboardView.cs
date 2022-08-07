﻿using System.Collections.Generic;
using UnityEngine;

public class ScoreboardView : BaseView
{
    public static ScoreboardView Instance { get; private set; }

    [SerializeField] PlayerScoreboardCard cardPrefab;
    private readonly Dictionary<string,PlayerScoreboardCard> playerCards = new Dictionary<string, PlayerScoreboardCard>();

    private void Awake()
    {
        Instance = this;    
    }

    public void RefreshPlayerCards(List<PlayerScoreboardCardData> cardsData)
    {
        foreach (var cardData in cardsData)
        {
            AddPlayerCard(cardData);
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
            playerCards.TryGetValue(cardTag, out PlayerScoreboardCard playerScoreboardCard);
            playerScoreboardCard.gameObject.SetActive(false); //TODO: Pooling
            playerCards.Remove(cardTag);
        }
    }

    public void RefreshPlayerCard(PlayerScoreboardCardData cardData)
    {
        if (playerCards.TryGetValue(cardData.playerName, out PlayerScoreboardCard card))
            card.UpdateCard(cardData);
    }
}
