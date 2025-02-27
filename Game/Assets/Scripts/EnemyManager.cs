using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;

    int[,] enemyDifficulty = new int[4,5] { // enemy difficulty increasing for later nights/levels
        {1,4,7,11,14},
        {0,0,0,0,0},
        {0,0,0,0,0},
        {0,0,0,0,0},
    };

    // enemy spawn points
    public static Vector2[] enemySpawns = new Vector2[] {new Vector2(28.5f,32), new Vector2(0,0), new Vector2(0,0), new Vector2(0,0)};

    // activates certain enemies and starts a night, setting their stats like speed and respawn time
    public void StartNight(int night) {
        for (int i = 0; i < enemies.Length; i++) {
            Pathfinding enemy = enemies[i].GetComponent<Pathfinding>();
            if (enemyDifficulty[i,night-1] > 0) {
                enemies[i].SetActive(true);
                enemy.id = i;
                enemy.difficulty = enemyDifficulty[i,night-1];
                enemy.state = 0;
                enemy.SetStats();
            }
            else enemies[i].SetActive(false);
        } 
    }
}
