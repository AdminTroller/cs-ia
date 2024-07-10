using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform player;

    void Update() {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }
}
