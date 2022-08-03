using FishNet.Object;
using System.Collections;
using UnityEngine;


public class Ground : MonoBehaviour
{
    //[SerializeField] PlayerStatistics playerStatistics;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Box box))
        {
            if (box.Tag == null)
                return;
            Player player;
            if (GameSession.Instance.TryGetPlayerByTag(box.Tag,out player))
            {
                PlayerStatistics playerStatistics = player.GetComponent<PlayerStatistics>();
                playerStatistics.ServerAddScore(1);
                box.gameObject.SetActive(false);
            }
        }
    }
}
