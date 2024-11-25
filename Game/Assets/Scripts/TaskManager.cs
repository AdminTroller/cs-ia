using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static bool inTask = false;
    [SerializeField] GameObject taskGUI;

    [SerializeField] SpriteRenderer crossRenderer;
    [SerializeField] Sprite cross;
    [SerializeField] Sprite crossHover;
    [SerializeField] BoxCollider2D crossCollider;

    [SerializeField] GameObject numberButtons;

    void Update() {
        taskGUI.SetActive(inTask);
        numberButtons.SetActive(inTask);

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (crossCollider.OverlapPoint(mousePos)) { // hovering cross
            crossRenderer.sprite = crossHover;
            if (Input.GetMouseButtonDown(0)) { // click
                inTask = false;
            }
        }
        else crossRenderer.sprite = cross;
    }
    
}
