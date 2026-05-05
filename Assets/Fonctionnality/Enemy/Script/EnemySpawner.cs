using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int enemyCap = 1;
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
                Vector3 position = transform.position;
                position.y--;

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
