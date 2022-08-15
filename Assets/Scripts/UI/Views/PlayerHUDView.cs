using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]    
public class PlayerHUDView : BaseView
{
    //public static PlayerHUDView Instance { get; private set; }

    [SerializeField] private TMP_Text scoreText;
    private void Awake()
    {
        //Instance = this;    
    }
    public override void Init()
    {
        base.Init();
        transform.position = Vector3.zero;
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.localPosition = Vector3.zero;
    }

    public void UpdateScore(string score)
    {
        scoreText.text = $"Score: {score} "; 
    }
}
