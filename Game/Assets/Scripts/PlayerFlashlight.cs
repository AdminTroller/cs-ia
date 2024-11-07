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
    float rotate = 0;

    [SerializeField] SpriteRenderer white;
    [SerializeField] AudioClip flash;
    float flashOpacity = 0;
    float flashTimer = 0;
    bool inFlash = false;

    [SerializeField] Transform enemy1;
    [SerializeField] Collider2D enemy1col;
    [SerializeField] LayerMask mask;

    void Update() {

        if (toggle) seeEnemy(rotate);
        else Enemy.seen = false;
        if (Vector2.Distance(transform.position, enemy1.position) < 3) Enemy.seen = true;

        if (TaskManager.inTask) return;

        Vector2 displacement = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        rotate = -Mathf.Atan(displacement.x/displacement.y) * Mathf.Rad2Deg;
        if (displacement.y < 0) rotate -= 180;
        anchor.eulerAngles = Vector3.forward * rotate;
        
        if (Input.GetKeyDown(KeyCode.F) && !Battery.batEmpty && !inFlash) Toggle();
        flashlight.enabled = toggle;

        if (Input.GetKeyDown(KeyCode.Space) && toggle && Battery.bat > 15 && !inFlash) Flash();
        if (inFlash) White();

    }

    void seeEnemy(float angleCenter) {
        Enemy.seen = false;
        for (int a = -20; a<=20; a+=5) {
            float angle = angleCenter + a;
            if (angle > 90) angle -= 360;
            if (angle < -270) angle += 360;
            float x = -Mathf.Tan(angle*Mathf.Deg2Rad);
            int inverter = 1;
            if (angle < -90 && angle > -270) inverter = -1;

            Debug.DrawRay(flashlight.transform.position, new Vector2(x, 1).normalized * 30 * inverter, Color.red);
            RaycastHit2D ray = Physics2D.Raycast(flashlight.transform.position, new Vector2(x, 1) * inverter, 30, mask);
            if (ray.collider == enemy1col) {
                Enemy.seen = true;
                break;
            } 
        }
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
