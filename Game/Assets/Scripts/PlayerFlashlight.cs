using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerFlashlight : MonoBehaviour
{
    [SerializeField] Light2D flashlight;
    [SerializeField] AudioSource sound;
    [SerializeField] AudioClip on;
    [SerializeField] AudioClip off;
    public static bool toggle = true;

    void Update() {
        Vector2 displacement = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotate;
        rotate = -Mathf.Atan(displacement.x/displacement.y) * Mathf.Rad2Deg;
        if (displacement.y < 0) rotate -= 180;
        flashlight.transform.eulerAngles = Vector3.forward * rotate;

        
        if (Input.GetKeyDown(KeyCode.F) && !Battery.batEmpty) Toggle();
        flashlight.enabled = toggle;
    }

    public void Toggle() {
        toggle = !toggle;
            if (toggle) sound.clip = on;
            else sound.clip = off;
            sound.Play();
    }
}
