
using UnityEngine;

public class Task : MonoBehaviour
{
    BoxCollider2D self;
    [SerializeField] BoxCollider2D player;

    void Awake() {
        self = gameObject.GetComponent<BoxCollider2D>();
    }

    void Update() {
        if (self.IsTouching(player)) {
            InteractIcon.show = true;
        }
    }
}
