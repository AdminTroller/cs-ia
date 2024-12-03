
using UnityEngine;

public class Task : MonoBehaviour
{
    BoxCollider2D self;
    [SerializeField] BoxCollider2D player;
    bool inRange = false;
    public int id;

    void Awake() {
        self = gameObject.GetComponent<BoxCollider2D>();
    }

    void Update() {
        if (TaskManager.tasksCompletion[id]) inRange = false;
        else inRange = self.IsTouching(player);
        InteractIcon.show = inRange;

        if (Input.GetKeyDown(KeyCode.E)) {
            TaskManager.currentTask = id;
            if (TaskManager.inTask) TaskManager.inTask = false;
            else if (inRange && !TaskManager.inTask) TaskManager.inTask = true;
        }
    }
}
