using System.Collections;
using System.Threading;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    bool firstTime = true;
    private bool pausedSpawn = false; 
    void Update()
    {
        if(GameTimer.instance.Temps > 2* 60 && firstTime)
        {
            firstTime = false;
            
            Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
        }
        if (!firstTime && !pausedSpawn && GameTimer.instance.Temps % 60 < 0.5f)
        {
            pausedSpawn = true;
            StartCoroutine(PauseAfterSpawn());
        }
    }
    public IEnumerator PauseAfterSpawn()
    {
       yield return new WaitForSeconds(1);
       pausedSpawn = true;
       Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Mineur"))
        {
            StateManager.State = StateManager.GameState.Lobby;
            GameTimer.instance.Temps = 0;
            Destroy(gameObject);
        }
    }
}
