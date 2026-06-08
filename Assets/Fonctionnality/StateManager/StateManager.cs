using UnityEngine;

public class StateManager : MonoBehaviour
{
    public enum GameState { Game, Lobby, Pause };
    public static GameState State { get; set; }

    void Start()
    {
        State = GameState.Game;
    }
}
