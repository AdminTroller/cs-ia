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
            if (Input.GetMouseButtonDown(0) && NumberButtons.currentNumber == number) { // click
                pressed = true;
                NumberButtons.currentNumber++;
            }
            else if (Input.GetMouseButton(0) && NumberButtons.currentNumber != number) {
                button.sprite = buttonSprites[3];
            }
        }
        else button.sprite = buttonSprites[0];
    }
}
