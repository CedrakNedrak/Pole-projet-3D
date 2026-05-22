using System.Collections;
using UnityEngine;

public class MineurMovement : AllyTroopMovement
{
    [SerializeField] private float pauseWhenMinining = 0.5f;

    public MineurMovement() : base(TroopType.MiningTroop) { }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");
        if (collision.gameObject.CompareTag("Map"))
        {
            Debug.Log("collision with map");
            StartCoroutine(WaitBeforeDestroying(collision.gameObject));
        }
    }

    private IEnumerator WaitBeforeDestroying(GameObject collision)
    {
        Debug.Log("here");
        StopTween();

        yield return new WaitForSeconds(pauseWhenMinining);

        Vector3Int cell = new Vector3Int(
            Mathf.RoundToInt(collision.transform.position.x),
            Mathf.RoundToInt(collision.transform.position.y),
            0
        );

        if(tweenEnCours + 1 < path.Count)
        {
           
            Vector3 direction = Vector3Int.RoundToInt(transform.position) - path[tweenEnCours + 1];
            int compteur = 0;
            while (direction.x != 0 && direction.y != 0)
            {
                compteur += 1;
                direction = path[tweenEnCours - compteur] - Vector3Int.RoundToInt(transform.position);
            }
            tweenEnCours -= compteur;
            direction = path[tweenEnCours - compteur - 1] - Vector3Int.RoundToInt(transform.position);
            TileGenerator.tileGenerator.DigCell(cell);
            if (direction.y != 0)
            {
                TileGenerator.tileGenerator.DigCell(cell + new Vector3Int(1, 0, 0));
                TileGenerator.tileGenerator.DigCell(cell - new Vector3Int(1, 0, 0));
            }
            if (direction.x != 0)
            {
                TileGenerator.tileGenerator.DigCell(cell + new Vector3Int(0, 1, 0));
                TileGenerator.tileGenerator.DigCell(cell - new Vector3Int(0, 1, 0));
            }
        }

        if (path != null && path.Count > tweenEnCours)
        {
            tweenEnCours++;

            if (tweenEnCours  < path.Count)
            {
                Rotate();
                StartTween(path[tweenEnCours]);
            }
        }
    }
}