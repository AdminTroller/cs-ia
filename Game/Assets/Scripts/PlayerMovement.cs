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
        horizontal = Input.GetAxis("Horizontal");
        Debug.Log(rb.velocity);
        Debug.Log(horizontal);
        rb.velocity = new Vector2(horizontal*speed, rb.velocity.y);
    }
}
