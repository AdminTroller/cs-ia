using UnityEngine;

public class NumberButtons : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    void Start() {
        for (float x = -7.2f; x <= 7.201f; x+=2.4f) {
            for (float y = -3.6f; y <= 3.61f; y+=2.4f) {
                GameObject current = Instantiate(prefab, gameObject.transform, false);
                current.transform.localPosition = new Vector2(x,y);
            }
        }
        
    }
}
