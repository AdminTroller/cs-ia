using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerFlashlight : MonoBehaviour
{
    [SerializeField] Light2D flashlight;
    [SerializeField] Transform anchor;
    [SerializeField] AudioSource sound;
    [SerializeField] AudioClip on;
    [SerializeField] AudioClip off;
    public static bool toggle = true;

    [SerializeField] SpriteRenderer white;
    [SerializeField] AudioClip flash;
    float flashOpacity = 0;
    float flashTimer = 0;
    bool inFlash = false;

    [SerializeField] Transform enemy1;
    [SerializeField] Collider2D enemy1col;

    void Update() {
        Vector2 displacement = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotate;
        rotate = -Mathf.Atan(displacement.x/displacement.y) * Mathf.Rad2Deg;
        if (displacement.y < 0) rotate -= 180;
        anchor.eulerAngles = Vector3.forward * rotate;
        
        if (Input.GetKeyDown(KeyCode.F) && !Battery.batEmpty && !inFlash) Toggle();
        flashlight.enabled = toggle;

        if (Input.GetKeyDown(KeyCode.Space) && Battery.bat > 15 && !inFlash) Flash();
        if (inFlash) White();

        if (toggle) seeEnemy(displacement, rotate);
    }

    void seeEnemy(Vector2 displacement, float angle) {
        RaycastHit2D rayCenter = Physics2D.Raycast(transform.position, new Vector2(displacement.x, displacement.y).normalized * 30);
        Debug.DrawRay(transform.position, new Vector2(displacement.x, displacement.y).normalized * 30, Color.white);

        if (rayCenter.collider == enemy1col) {
            
        };

        // anti clockwise
        angle += 20;
        if (angle > 90) angle -= 360;
        float x = -Mathf.Tan(angle*Mathf.Deg2Rad);
        int inverter = 1;
        if (angle < -90 && angle > -270) inverter = -1;
        RaycastHit2D rayACW = Physics2D.Raycast(transform.position, new Vector2(x, 1).normalized * 30 * inverter);
        Debug.DrawRay(transform.position, new Vector2(x, 1).normalized * 30 * inverter, Color.red);

        // clockwise
        angle -= 40;
        if (angle < -270) angle += 360;
        x = -Mathf.Tan(angle*Mathf.Deg2Rad);
        inverter = 1;
        if (angle < -90 && angle > -270) inverter = -1;
        RaycastHit2D rayCW = Physics2D.Raycast(transform.position, new Vector2(x, 1).normalized * 30 * inverter);
        Debug.DrawRay(transform.position, new Vector2(x, 1).normalized * 30 * inverter, Color.green);
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
