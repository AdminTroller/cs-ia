using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    const float speed = 6.2f;

    void Awake() {
        Application.targetFrameRate = 60;
    }

    void FixedUpdate() {
        int hor = 0;
        int ver = 0;
        if (!TaskManager.inTask) {
            if (Input.GetKey(KeyCode.W)) ver++;
            if (Input.GetKey(KeyCode.S)) ver--;
            if (Input.GetKey(KeyCode.A)) hor--;
            if (Input.GetKey(KeyCode.D)) hor++;
        }
        
        rb.velocity = new Vector2(hor, ver).normalized*speed;
    }
}