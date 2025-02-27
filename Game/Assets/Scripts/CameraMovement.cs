using TMPro;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] TextMeshProUGUI fpsTest;

    void Update() { // make camera track player position
        transform.position = new Vector3(Mathf.Clamp(player.position.x, -30f, 17f), Mathf.Clamp(player.position.y, -17f, 32f), -10);
        fpsTest.text = "FPS: " + (Mathf.Round(100 / Time.deltaTime)/100).ToString(); // debug FPS display
    }
}
