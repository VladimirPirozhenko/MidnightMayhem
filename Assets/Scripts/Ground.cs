using FishNet.Object;
using System.Collections;
using UnityEngine;


public class Ground : NetworkBehaviour
{
    public void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Box box))
        {
            if (box.Tag == null)
                return;
            Player player;
            if (GameSession.Instance.TryGetPlayerByTag(box.Tag,out player))
            {
                PlayerStatistics playerStatistics = player.PlayerStatistics;
                //if (!IsOwner)
                playerStatistics.ServerAddScore(1);
                //ViewManager.Instance.TryGetView<ScoreboardView>(out ScoreboardView scoreboardView);
                PlayerScoreboardCardData cardData = new PlayerScoreboardCardData(player.Tag, playerStatistics.Score.ToString());
                //GameSession.Instance.UpdatePlayerCardsRpc();
                GameSession.Instance.RefreshCardRpc(cardData);
               
                //scoreboardView.UpdateView(scoreboardData);
                box.gameObject.SetActive(false);
            }
        }
    }
    
}
