using UnityEngine;

public class Minimap : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform you;
    [SerializeField] GameObject minimap;

    float ratio = 0.1875f;

    void Update() {
        you.localPosition = player.position * ratio + new Vector3(0.9375f, -1.875f, 0);
        if (TaskManager.inTask) minimap.SetActive(false);
        else minimap.SetActive(Input.GetKey(KeyCode.Tab));
    }
}
