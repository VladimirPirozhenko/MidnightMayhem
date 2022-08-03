using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


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
    private List<PlayerScoreboardCard> playerCards;
    public override void Init()
    {
        base.Init();
    }
    public void UpdateView(ScoreboardViewData data)
    {
        for (int i = 0; i < playerCards.Count; i++)
        {
           // PlayerScoreboardCardData cardData = new PlayerScoreboardCardData(data.scoreText,);
            //playerCards[i].UpdateCard();
        }
    }
}
