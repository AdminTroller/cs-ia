using UnityEngine;

public class InteractIcon : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    public static bool show = false;

    void Update() {
        spriteRenderer.enabled = show;
        show = false;
    }
}
