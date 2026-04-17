using UnityEngine;

public class BuildTownButton : MonoBehaviour
{
    [SerializeField] TownUI townUI;

    private void BuildTown()
    {
        townUI.townData.BuildTown.Build();
    }
}
