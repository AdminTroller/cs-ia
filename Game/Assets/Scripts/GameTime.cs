using TMPro;
using UnityEngine;

public class GameTime : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tDisplay;
    public static float t = 6;
    int tHour;
    string tMinutes;
    float tScale = 0.02f;

    void Update() {
        tHour = Mathf.FloorToInt(t);
        tMinutes = Mathf.FloorToInt((t-tHour)*60).ToString();
        if (tMinutes.Length == 1) tMinutes = "00";
        else tMinutes = tMinutes[0].ToString() + "0";
        if (t < 12) {
            t += tScale * Time.deltaTime;
            tDisplay.text = tHour.ToString() + ":" + tMinutes + " PM";
        }
        else tDisplay.text = "12:00 AM";
    }
}
