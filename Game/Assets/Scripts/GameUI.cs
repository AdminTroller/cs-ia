using System;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] SpriteRenderer batIcon;
    [SerializeField] Sprite[] batStates;
    public float bat = 100;

    void Update() {
        batIcon.sprite = batStates[Mathf.CeilToInt(bat/25)];
    }
}
