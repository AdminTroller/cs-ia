using UnityEngine;
using TMPro;

public class Battery : MonoBehaviour
{
    [SerializeField] SpriteRenderer batIcon;
    [SerializeField] Sprite[] batStates;
    public Color[] batColors;
    [SerializeField] TextMeshProUGUI batNum;
    [SerializeField] PlayerFlashlight playerFlashlight;

    public static float bat = 100; // flashlight battery %
    public static bool batEmpty = false;
    const float drainRate = 0.4f; // how fast flashlight battery decreases

    void Update() {
        Drain();
        UI();
    }

    void Drain() {
        if (PlayerFlashlight.toggle && bat > 0) {
            bat -= drainRate * Time.deltaTime; // drain battery when flashlight is on
        }

        if (bat <= 0 && !batEmpty) { // disable flashlight when battery is 0%
            batEmpty = true;
            bat = 0;
            playerFlashlight.Toggle();
        }
    }

    void UI() { // show battery UI display on screen
        batIcon.sprite = batStates[Mathf.CeilToInt(bat/25)];
        batNum.text = Mathf.CeilToInt(bat).ToString() + "%";
        batNum.color = batColors[Mathf.CeilToInt(bat/25)];
    }
}
