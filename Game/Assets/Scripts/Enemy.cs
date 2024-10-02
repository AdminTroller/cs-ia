using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static bool seen;
    [SerializeField] SpriteRenderer sprite;

    void Update() {
        if (seen) {
            if (sprite.color.a < 1) sprite.color += new Color(0,0,0, 5 * Time.deltaTime);
            else sprite.color = new Color(255,255,255, 1);
        }
        else {
            if (sprite.color.a > 0) sprite.color -= new Color(0,0,0, 5 * Time.deltaTime);
            else sprite.color = new Color(255,255,255, 0);
        }
    }
}
