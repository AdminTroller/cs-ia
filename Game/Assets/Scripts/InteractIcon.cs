using UnityEngine;

public class InteractIcon : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    public static bool show = false;

    void Update() { // shows interact icon for task if player is near
        spriteRenderer.enabled = show;
        show = false;
    }
}
