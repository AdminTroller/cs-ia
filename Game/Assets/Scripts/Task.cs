
using UnityEngine;

public class Task : MonoBehaviour
{
    BoxCollider2D self;
    [SerializeField] BoxCollider2D player;

    void Awake() {
        self = gameObject.GetComponent<BoxCollider2D>();
    }

    void Update() {
        // if (self.GetContacts(0) == player) {
        //     Debug.Log("a");
        // }
    }
}
