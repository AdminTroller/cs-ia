using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    public static Vector2 dir;

    // fixed player movement speed
    const float speed = 6.2f;

    void Awake() {
        Application.targetFrameRate = 60; // set target FPS to 60
    }

    void FixedUpdate() { // check for movement key inputs from user
        int hor = 0;
        int ver = 0;
        if (!TaskManager.inTask && !PlayerFlashlight.inFlashStart) {
            if (Input.GetKey(KeyCode.W)) ver++;
            if (Input.GetKey(KeyCode.S)) ver--;
            if (Input.GetKey(KeyCode.A)) hor--;
            if (Input.GetKey(KeyCode.D)) hor++;
        }
        dir = new Vector2(hor, ver);
        
        rb.velocity = dir.normalized*speed; // normalise diagonal movement speed
    }
}