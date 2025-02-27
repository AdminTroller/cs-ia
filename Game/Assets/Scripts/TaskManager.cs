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

    // stores all different tasks
    [SerializeField] GameObject[] tasks;
    public static bool[] tasksCompletion = new bool[] {false};
    public static int currentTask = 0;

    // stores all task instructions
    [SerializeField] TextMeshProUGUI instructionsText;
    string[] instructions = {
        "Click in ascending order",
    };

    void Update() {
        // open up task UI
        taskGUI.SetActive(inTask);
        tasks[0].SetActive(inTask);
        instructionsText.text = instructions[currentTask];

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // close task
        if (crossCollider.OverlapPoint(mousePos)) { // hovering cross
            crossRenderer.sprite = crossHover;
            if (Input.GetMouseButtonDown(0)) { // click
                inTask = false;
            }
        }
        else crossRenderer.sprite = cross;
    }
    
}
