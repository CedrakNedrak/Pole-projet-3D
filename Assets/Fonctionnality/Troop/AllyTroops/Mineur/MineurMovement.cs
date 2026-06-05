using System.Collections;
using UnityEngine;

public class MineurMovement : AllyTroopMovement
{
    [SerializeField] private float pauseWhenMinining = 0.5f;
    private bool isMining = false;
    public MineurMovement() : base(TroopType.MiningTroop) { }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Map")&& !isMining)
        {
            StartCoroutine(WaitBeforeDestroying(collision.gameObject));
        }
    }

    private IEnumerator WaitBeforeDestroying(GameObject collision)
    {
        isMining = true;
        Debug.Log("here");
        StopTween();

        yield return new WaitForSeconds(pauseWhenMinining);

        isMining = false;
        Vector3Int cell = new Vector3Int(
            Mathf.RoundToInt(collision.transform.position.x),
            Mathf.RoundToInt(collision.transform.position.y),
            0
        );
        Debug.Log(tweenEnCours);
        if (tweenEnCours + 1 < path.Count)
        {
           
            Vector3 direction = Vector3Int.RoundToInt(transform.position) - path[tweenEnCours + 1];
            int compteur = 0;
            
            tweenEnCours -= compteur;
            direction = path[tweenEnCours - compteur - 1] - Vector3Int.RoundToInt(transform.position);
            TileGenerator.tileGenerator.DigCell(cell);
        
        }

        if (path != null && path.Count > tweenEnCours)
        {

            if (tweenEnCours + 1  < path.Count)
            {
                Rotate();
                StartTween(path[tweenEnCours]);
            }
        }
    }
}