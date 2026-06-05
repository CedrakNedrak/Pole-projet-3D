using System.Threading;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    bool firstTime = true;
    void Update()
    {
        if(GameTimer.instance.Temps > 5 * 60 && firstTime)
        {
            firstTime = false;
            
            Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
        }
    }
}
