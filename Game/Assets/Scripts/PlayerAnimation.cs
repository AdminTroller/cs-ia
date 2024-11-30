using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Sprite[] front;
    [SerializeField] Sprite[] back;
    [SerializeField] Sprite[] left;
    [SerializeField] Sprite[] right;
    string currentDir = "front";
    Vector2 dir;
    float animationTimer = 0;

    void Update() {
        dir = PlayerMovement.dir;

        if (dir.x < 0) { // move left
            currentDir = "left";
        }
        else if (dir.x > 0) { // move right
            currentDir = "right";
        }
        else if (dir.y < 0) { // move down
            currentDir = "front";
        }
        else if (dir.y > 0) { // move up
            currentDir = "back";
        }

        if (currentDir == "front") sr.sprite = front[Mathf.FloorToInt(animationTimer)];
        if (currentDir == "back") sr.sprite = back[Mathf.FloorToInt(animationTimer)];
        if (currentDir == "left") sr.sprite = left[Mathf.FloorToInt(animationTimer)];
        if (currentDir == "right") sr.sprite = right[Mathf.FloorToInt(animationTimer)];

        if (dir.x != 0 || dir.y != 0) {
            animationTimer += Time.deltaTime * 6;
            if (animationTimer >= 4) animationTimer -= 4;
        }
        else animationTimer = 0;
    }
}
