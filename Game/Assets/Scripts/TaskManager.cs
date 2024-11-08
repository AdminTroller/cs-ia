using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static bool inTask = false;
    [SerializeField] GameObject taskGUI;

    [SerializeField] SpriteRenderer crossRenderer;
    [SerializeField] Sprite cross;
    [SerializeField] Sprite crossHover;

    void Update() {
        taskGUI.SetActive(inTask);
    }
    
}
