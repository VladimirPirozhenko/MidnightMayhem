using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerHUDView : BaseView
{
    [SerializeField] private TMP_Text scoreText;
    public override void Init()
    {
        base.Init();
    }
    public void UpdateScore(string score)
    {
        scoreText.text = score;
    }
}
