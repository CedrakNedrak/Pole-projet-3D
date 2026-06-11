using System.Collections;
using UnityEngine;

public class Digger : EnemyTroopMovement
{
    [SerializeField] private float pauseWhenMinining = 0.5f;
    private bool isMining = false;
    [SerializeField] float degatsParCoup = 5f;
    [SerializeField] float tempsEntreCoups = 1f;
    private float timer;
    private TownData cible;

    public Digger() : base(TroopType.MiningTroop, Quaternion.identity) { }
    public override void ReStartMoving()
    {
        Vector3 mainPos = TileGenerator.tileGenerator.MainTownPosition;
        base.StartMoving(transform.position, mainPos);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Map") && !isMining)
        {
            StartCoroutine(WaitBeforeDestroying(collision.gameObject));
        }

    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Town"))
        {
            timer += Time.deltaTime;
            if (timer >= tempsEntreCoups)
            {
                Debug.Log("Toucher");
                collision.gameObject.GetComponent<TownData>().TownHealth.TakeDamage(degatsParCoup);
                timer = 0f;
            }
        }
    }

    private IEnumerator WaitBeforeDestroying(GameObject collision)
    {
        isMining = true;
        StopTween();

        yield return new WaitForSeconds(pauseWhenMinining);

        isMining = false;
        Vector3Int cell = new Vector3Int(
            Mathf.RoundToInt(collision.transform.position.x),
            Mathf.RoundToInt(collision.transform.position.y),
            0
        );

        TileGenerator.tileGenerator.DigCell(cell);

        if (path != null && path.Count > tweenEnCours)
        {

            if (tweenEnCours + 1 < path.Count)
            {
                Rotate();
                StartTween(path[tweenEnCours]);
            }
        }
    }


}

