using UnityEngine;

using TMPro; // N'oublie pas d'importer TextMeshPro
public class TimerUI : MonoBehaviour
{      // Durée du timer en secondes
    [SerializeField] private TMP_Text timerText;       // Référence au TextMeshPro de l'UI
    
    void Update()
    {
        GameTimer.Time += Time.deltaTime; // On incrémente le timer

        // Format mm:ss
        int minutes = Mathf.FloorToInt(GameTimer.Time / 60);
        int seconds = Mathf.FloorToInt(GameTimer.Time % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}