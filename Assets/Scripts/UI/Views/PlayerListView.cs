using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListView : BaseView
{
    [SerializeField] PlayerLobbyCard cardPrefab;
	private List<PlayerLobbyCard> playerCards;
    public override void Init()
    {
        base.Init();
		playerCards = new List<PlayerLobbyCard>(12);	
		Transform parentForCards = this.GetComponentInChildren<VerticalLayoutGroup>().transform;
		for (int i = 0; i < 12; i++)
        {
			PlayerLobbyCard card = Instantiate(cardPrefab);
			card.transform.SetParent(parentForCards, false);
			playerCards.Add(card);
		}
		//Button newButton = (Button)Instantiate(buttonPrefab);
		//newButton.transform.SetParent(this.transform, false);
	}
	public void RefreshClientRpc(Player[] players)
	{
		int index = 0;
		for (int i = 0; i < players.Length; i++)
		{
			playerCards[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = players[i].userName;
			//playerCards[i].GetComponentInChildren<Toggle>().isOn = players[i].isReady;
		}
		index++;

		if (index < playerCards.Count)
		{
			for (int i = index; i < playerCards.Count; i++)
			{
				playerCards[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Empty slot...";
				playerCards[i].GetComponentInChildren<Toggle>().isOn = false;
			}
		}
	}
}
