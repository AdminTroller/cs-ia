using System;
using UnityEngine;
using TMPro;

public class Battery : MonoBehaviour
{
    [SerializeField] SpriteRenderer batIcon;
    [SerializeField] Sprite[] batStates;
    public Color[] batColors;
    [SerializeField] TextMeshProUGUI batNum;

    public float bat = 100;
    float drainRate = 0.4f;

    void Update() {
        Drain();
        UI();
    }

    void Drain() {
        if (PlayerFlashlight.toggle) {
            bat -= drainRate * Time.deltaTime;
        }
    }

    void UI() {
        batIcon.sprite = batStates[Mathf.CeilToInt(bat/25)];
        batNum.text = Mathf.CeilToInt(bat).ToString() + "%";
        batNum.color = batColors[Mathf.CeilToInt(bat/25)];
    }
}
