using UnityEngine;

using TMPro; // N'oublie pas d'importer TextMeshPro 
public class TimerUI : MonoBehaviour
{
    public float timer = 10f;        // Durée du timer en secondes
    public TMP_Text timerText;       // Référence au TextMeshPro de l'UI

    void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime; // Décrémenter le temps
            timer = Mathf.Max(timer, 0f); // Empêche de devenir négatif

            // Affichage format mm:ss
            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            timerText.text = "00:00";
            // Ici tu peux déclencher une action quand le timer est fini
        }
    }
}