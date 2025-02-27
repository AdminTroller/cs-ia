using TMPro;
using UnityEngine;

public class GameTime : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tDisplay;
    public static float t = 6; // game timer in hours represented by a float
    int tHour;
    string tMinutes;
    float tScale = 0.02f; // how fast game timer progresses

    void Update() {
        tHour = Mathf.FloorToInt(t); // takes out hour value from game timer
        tMinutes = Mathf.FloorToInt((t-tHour)*60).ToString(); // takes out minute value from game timer
        if (tMinutes.Length == 1) tMinutes = "00";
        else tMinutes = tMinutes[0].ToString() + "0";
        if (t < 12) {
            t += tScale * Time.deltaTime; // increment time
            tDisplay.text = tHour.ToString() + ":" + tMinutes + " PM"; // display time on screen
        }
        else tDisplay.text = "12:00 AM";
    }
}
