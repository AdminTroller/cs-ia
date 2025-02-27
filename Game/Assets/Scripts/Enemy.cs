using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool seen; // does flashlight see enemy?
    public bool proximitySeen; // is player really close to enemy?
    
    [SerializeField] SpriteRenderer sprite;
    const int fadeSpeed = 7; // how fast enemy fades in/out

    void Update() {
        if (seen || proximitySeen) { // fade in enemy when seen by flashlight or is near player
            if (sprite.color.a < 1) sprite.color += new Color(0,0,0, fadeSpeed * Time.deltaTime);
            else sprite.color = new Color(255,255,255, 1);
        }
        else { // otherwise fade out enemy
            if (sprite.color.a > 0) sprite.color -= new Color(0,0,0, fadeSpeed * Time.deltaTime);
            else sprite.color = new Color(255,255,255, 0);
        }
    }
}
