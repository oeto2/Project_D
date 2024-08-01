using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    public PlayerInputaction inputActions { get; private set; }
    public PlayerInputaction.PlayerActions playerActions { get; private set; }

    private void Awake()
    {
        inputActions = new PlayerInputaction();
        playerActions = inputActions.Player;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}
