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

        TileGenerator.tileGenerator.DigCell(cell);


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