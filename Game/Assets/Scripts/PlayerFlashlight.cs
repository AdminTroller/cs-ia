using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerFlashlight : MonoBehaviour
{
    [SerializeField] Light2D flashlight;
    [SerializeField] AudioSource sound;
    [SerializeField] AudioClip on;
    [SerializeField] AudioClip off;
    public static bool toggle = true;

    [SerializeField] SpriteRenderer white;
    [SerializeField] AudioClip flash;
    float flashOpacity = 0;
    float flashTimer = 0;
    bool inFlash = false;

    void Update() {
        Vector2 displacement = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotate;
        rotate = -Mathf.Atan(displacement.x/displacement.y) * Mathf.Rad2Deg;
        if (displacement.y < 0) rotate -= 180;
        flashlight.transform.eulerAngles = Vector3.forward * rotate;

        
        if (Input.GetKeyDown(KeyCode.F) && !Battery.batEmpty && !inFlash) Toggle();
        flashlight.enabled = toggle;

        if (Input.GetKeyDown(KeyCode.Space) && Battery.bat > 15 && !inFlash) Flash();
        if (inFlash) White();
    }

    public void Toggle() {
        toggle = !toggle;
            if (toggle) sound.clip = on;
            else sound.clip = off;
            sound.Play();
    }

    void Flash() {
        inFlash = true;
        flashTimer = 0;
        Battery.bat -= 15;
        sound.clip = flash;
        sound.Play();
    }

    void White() {
        flashTimer += Time.deltaTime;

        if (flashTimer > 0.2f && flashTimer < 1) {
            if (flashOpacity < 1) flashOpacity += 4 * Time.deltaTime;
            else flashOpacity = 1;
        }
        else if (flashTimer > 3) {
            if (flashOpacity > 0) flashOpacity -= 5 * Time.deltaTime;
            else {
                flashOpacity = 0;
                inFlash = false;
            }
        }

        white.color = new Color(255,255,255,flashOpacity);
    }
}
