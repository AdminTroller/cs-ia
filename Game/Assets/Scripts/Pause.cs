// UNUSED
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool paused = false;
    [SerializeField] GameObject fade;
    [SerializeField] GameObject textCanvas;

    void Update() {

        if (Input.GetKeyDown(KeyCode.Escape) && GameTime.t > 6 && !PlayerDeath.dead) paused = !paused;
        if (paused) Time.timeScale = 0; // freeze time when paused
        else Time.timeScale = 1;

        fade.SetActive(paused);
        textCanvas.SetActive(paused);
    }
    
}
