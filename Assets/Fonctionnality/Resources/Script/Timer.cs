using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
 
    public static GameTimer instance;
    public float Temps;
    public void Awake() {
        instance = this;
        Temps = 0f;
    }   
    void Update()
    {
        if (StateManager.State == StateManager.GameState.Game)
        {
            Temps += Time.deltaTime;

            int minutes = Mathf.FloorToInt(Temps / 60);
            int secondes = Mathf.FloorToInt(Temps % 60);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, secondes);
        }
    }
}