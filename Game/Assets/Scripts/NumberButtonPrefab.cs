using TMPro;
using UnityEngine;

public class NumberButtonPrefab : MonoBehaviour
{
    public int number;
    bool pressed = false;
    [SerializeField] TextMeshProUGUI numberText;
    [SerializeField] SpriteRenderer button;
    [SerializeField] Sprite[] buttonSprites;
    [SerializeField] BoxCollider2D buttonCollider;
    [SerializeField] AudioSource click;
    [SerializeField] AudioSource fail;

    void Start() {
        numberText.text = number.ToString();
    }

    void Update() {
        if (pressed) {
            button.sprite = buttonSprites[2];
            return;
        }
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (buttonCollider.OverlapPoint(mousePos)) { // hovering cross
            button.sprite = buttonSprites[1];
            if (Input.GetMouseButtonDown(0)) { // click
                if (NumberButtons.currentNumber == number) { // correct
                    click.Play();
                    pressed = true;
                    NumberButtons.currentNumber++;
                }
                else { // wrong
                    fail.Play();
                    button.sprite = buttonSprites[3];
                }
            }
        }
        else button.sprite = buttonSprites[0];
    }
}
