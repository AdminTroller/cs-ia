using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform player;

    void Update() {
        transform.position = new Vector3(Mathf.Clamp(player.position.x, -30f, 17f), Mathf.Clamp(player.position.y, -17f, 32f), -10);
    }
}
