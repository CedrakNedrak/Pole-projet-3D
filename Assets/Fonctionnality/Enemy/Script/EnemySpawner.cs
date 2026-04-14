using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int enemyCap = 5;
    [SerializeField] private GameObject enemy;
    [SerializeField] private float spawnInterval;
    private int enemyCount = 0;
    private float timer;
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > spawnInterval)
        {
            if (enemyCount < enemyCap)
            {
                Vector3 position = new Vector3(0,0,0);

                Instantiate(enemy, position, enemy.transform.rotation);
                enemyCount += 1;
            }
            timer = 0f;
        }
    }
    
    public void EnemyDestroyed()
    {
        enemyCount -= 1;
    }
}
