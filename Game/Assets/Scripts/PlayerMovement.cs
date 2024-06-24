using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    float horizontal = 0;

    const float speed = 5f;

    void Awake() {
        Application.targetFrameRate = 60;
    }

    void FixedUpdate() {
        int hor = 0;
        int ver = 0;
        if (Input.GetKey(KeyCode.UpArrow)) ver++;
        if (Input.GetKey(KeyCode.DownArrow)) ver--;
        if (Input.GetKey(KeyCode.LeftArrow)) hor--;
        if (Input.GetKey(KeyCode.RightArrow)) hor++;
        
        rb.velocity = new Vector2(hor*speed, ver*speed);
    }
}
