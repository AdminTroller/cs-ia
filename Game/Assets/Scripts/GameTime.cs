using TMPro;
using UnityEngine;

public class GameTime : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tDisplay;
    public static float t = 0;
    int tHour;
    string tMinutes;
    float tScale = 0.02f;

    void Update() {
        if (t < 6) t += tScale * Time.deltaTime;
        tHour = Mathf.FloorToInt(t);
        tMinutes = Mathf.FloorToInt((t-tHour)*60).ToString();
        if (tHour == 0) tHour = 12;
        if (tMinutes.Length == 1) tMinutes = "00";
        else tMinutes = tMinutes[0].ToString() + "0";
        tDisplay.text = tHour.ToString() + ":" + tMinutes + " AM";
    }
}
