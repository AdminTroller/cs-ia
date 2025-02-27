using System.Linq;
using UnityEngine;

public class NumberButtons : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    int[] numbers = Enumerable.Range(1,28).ToArray();
    int index = 0;
    public static int currentNumber = 1;

    void Start() {

        // randomly shuffle numbers in an integer array
        for (int i=0; i<numbers.Length; i++) {
            int temp = numbers[i];
            int r = Random.Range(i, numbers.Length);
            numbers[i] = numbers[r];
            numbers[r] = temp;
        }

        // creates task buttons in a grid pattern
        for (float x = -7.2f; x <= 7.201f; x+=2.4f) {
            for (float y = -3.6f; y <= 3.61f; y+=2.4f) {
                GameObject current = Instantiate(prefab, gameObject.transform, false);
                NumberButtonPrefab currentPrefab = current.GetComponent<NumberButtonPrefab>();
                current.transform.localPosition = new Vector2(x,y);
                currentPrefab.number = numbers[index];
                index++;
            }
        }
    }

    void Update() {
        if (currentNumber == 29) {
            TaskManager.tasksCompletion[0] = true;
            TaskManager.inTask = false;
        }
    }
}
