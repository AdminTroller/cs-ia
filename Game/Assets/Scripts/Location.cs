using TMPro;
using UnityEngine;

public class Location : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI locationText;
    [SerializeField] BoxCollider2D[] areas;
    [SerializeField] BoxCollider2D player;
    // list of areas in game map
    string[] areaNames = {"Office","Storage","Server Room","Right Generator","Ventilation","Bathrooms","Laboratory","Cellar","Left Generator","Exit"};
    string areaName = "Office";

    void Update() {
        for (int i=0; i<areas.Length; i++) {
            if (player.IsTouching(areas[i])) {
                areaName = areaNames[i];
                break;
            }
            areaName = "";
        }
        locationText.text = areaName; // shows with UI element which area of the map the player is in
    }

}
