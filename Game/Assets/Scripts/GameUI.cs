using System;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] SpriteRenderer batIcon;
    [SerializeField] Sprite[] batStates;
    [SerializeField] TextMeshProUGUI batNum;
    public float bat = 100;

    void Update() {
        batIcon.sprite = batStates[Mathf.CeilToInt(bat/25)];
        batNum.text = Mathf.RoundToInt(bat).ToString() + "%";
    }
}