using UnityEngine;

public class Minimap : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform you;
    [SerializeField] GameObject minimap;

    [SerializeField] Transform[] taskObjects;
    [SerializeField] GameObject taskIndicator;

    float ratio = 0.1875f;

    void Start() {
        for (int i=0; i<taskObjects.Length; i++) {
            GameObject current = Instantiate(taskIndicator, minimap.transform);
            current.transform.localPosition = taskObjects[i].position * ratio + new Vector3(0.9375f, -1.875f);
            current.transform.localPosition = new Vector3(Mathf.Round(current.transform.localPosition.x*16)/16f, Mathf.Round(current.transform.localPosition.y*16)/16f);
        }
    }

    void Update() {
        you.localPosition = player.position * ratio + new Vector3(0.9375f, -1.875f, 0);

        if (TaskManager.inTask) minimap.SetActive(false);
        else minimap.SetActive(Input.GetKey(KeyCode.Tab));
    }
}
