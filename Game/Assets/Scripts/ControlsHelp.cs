using UnityEngine;

public class ControlsHelp : MonoBehaviour
{
    [SerializeField] GameObject display;
    [SerializeField] GameObject keybind;
    bool toggle = false;

    void Update() { // show game controls UI
        toggle = Input.GetKey(KeyCode.Q);
        display.SetActive(toggle);
        keybind.SetActive(!toggle);
    }
}
