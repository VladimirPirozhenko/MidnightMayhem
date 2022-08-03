using TMPro;
using UnityEngine;

public struct PlayerScoreboardCardData
{
    public string playerName;
    public string playerScore;
    public PlayerScoreboardCardData(string name,string score)
    {
        playerName = name;
        playerScore = score;    
    }
}

public class PlayerScoreboardCard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI playerScore;
    public void UpdateCard(PlayerScoreboardCardData data)
    {
        playerName.text = data.playerName;
        playerScore.text = data.playerScore;    
    }
}