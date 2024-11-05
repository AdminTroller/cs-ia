using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static bool inTask = false;
    [SerializeField] SpriteRenderer spriteRenderer;

    void Update() {
        spriteRenderer.enabled = inTask;
    }
    
}
