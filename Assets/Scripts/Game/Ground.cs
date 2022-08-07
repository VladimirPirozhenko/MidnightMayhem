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

            if (GameSession.Instance.TryGetPlayerByTag(box.Tag,out Player player))
            {
                PlayerStatistics playerStatistics = player.PlayerStatistics;
                playerStatistics.ServerAddScore(1);
                box.gameObject.SetActive(false);
            }
        }
    }
    
}
