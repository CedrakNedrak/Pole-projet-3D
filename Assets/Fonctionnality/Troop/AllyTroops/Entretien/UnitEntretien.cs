using UnityEngine;

public class UnitEntretien : MonoBehaviour
{
    [SerializeField] private int goldCostPerSecond = 1;

    public int GoldCostPerSecond => goldCostPerSecond;
}