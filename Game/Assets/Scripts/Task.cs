
using UnityEngine;

public class Task : MonoBehaviour
{
    BoxCollider2D self;
    [SerializeField] BoxCollider2D player;
    bool inRange = false;

    void Awake() {
        self = gameObject.GetComponent<BoxCollider2D>();
    }

    void Update() {
        inRange = self.IsTouching(player);
        InteractIcon.show = inRange;

        if (Input.GetKeyDown(KeyCode.E)) {
            if (TaskManager.inTask) TaskManager.inTask = false;
            else if (inRange && !TaskManager.inTask) TaskManager.inTask = true;
            
        }
    }
}
