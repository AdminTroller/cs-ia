using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool seen;
    [SerializeField] SpriteRenderer sprite;
    const int fadeSpeed = 7;

    void Update() {
        if (seen) {
            if (sprite.color.a < 1) sprite.color += new Color(0,0,0, fadeSpeed * Time.deltaTime);
            else sprite.color = new Color(255,255,255, 1);
        }
        else {
            if (sprite.color.a > 0) sprite.color -= new Color(0,0,0, fadeSpeed * Time.deltaTime);
            else sprite.color = new Color(255,255,255, 0);
        }
    }
}
