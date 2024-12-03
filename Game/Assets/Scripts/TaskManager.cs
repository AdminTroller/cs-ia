using TMPro;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static bool inTask = false;
    [SerializeField] GameObject taskGUI;

    [SerializeField] SpriteRenderer crossRenderer;
    [SerializeField] Sprite cross;
    [SerializeField] Sprite crossHover;
    [SerializeField] BoxCollider2D crossCollider;

    [SerializeField] GameObject[] tasks;
    public static bool[] tasksCompletion = new bool[] {false};
    public static int currentTask = 0;

    [SerializeField] TextMeshProUGUI instructionsText;
    string[] instructions = {
        "Click in ascending order",
    };

    void Update() {
        taskGUI.SetActive(inTask);
        tasks[0].SetActive(inTask);
        instructionsText.text = instructions[currentTask];

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
