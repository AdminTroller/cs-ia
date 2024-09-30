using System;
using UnityEngine;
using TMPro;

public class Battery : MonoBehaviour
{
    [SerializeField] SpriteRenderer batIcon;
    [SerializeField] Sprite[] batStates;
    public Color[] batColors;
    [SerializeField] TextMeshProUGUI batNum;
    [SerializeField] PlayerFlashlight playerFlashlight;

    float bat = 100;
    public static bool batEmpty = false;
    // float drainRate = 0.4f;
    float drainRate = 10f;

    void Update() {
        Drain();
        UI();
    }

    void Drain() {
        if (PlayerFlashlight.toggle && bat > 0) {
            bat -= drainRate * Time.deltaTime;
        }
        
        if (bat <= 0 && !batEmpty) {
            batEmpty = true;
            bat = 0;
            playerFlashlight.Toggle();
        }
    }

    void UI() {
        batIcon.sprite = batStates[Mathf.CeilToInt(bat/25)];
        batNum.text = Mathf.CeilToInt(bat).ToString() + "%";
        batNum.color = batColors[Mathf.CeilToInt(bat/25)];
    }
}
