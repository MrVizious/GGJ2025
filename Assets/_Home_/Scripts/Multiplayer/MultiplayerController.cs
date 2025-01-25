using DesignPatterns;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MultiplayerController : Singleton<MultiplayerController>
{
    protected override bool keepOldestInstance => true;
    protected override bool dontDestroyOnLoad => true;
    public PlayerInputHolder[] playerInputHolders = new PlayerInputHolder[4];
    public UnityEvent<PlayerInputHolder> onPlayerJoined = new UnityEvent<PlayerInputHolder>();
    public UnityEvent<PlayerInputHolder> onPlayerLeft = new UnityEvent<PlayerInputHolder>();
    public void OnPlayerJoined(PlayerInput newPlayerInput)
    {
        for (int i = 0; i < playerInputHolders.Length; i++)
        {
            if (playerInputHolders[i] == null)
            {

                playerInputHolders[i] = newPlayerInput.GetComponent<PlayerInputHolder>();
                onPlayerJoined.Invoke(playerInputHolders[i]);
                return;
            }
        }
    }
    public void OnPlayerLeft(PlayerInput leftPlayerInput)
    {
        PlayerInputHolder playerInputHolder = leftPlayerInput.GetComponent<PlayerInputHolder>();
        for (int i = 0; i < playerInputHolders.Length; i++)
        {
            if (playerInputHolders[i] == playerInputHolder)
            {
                playerInputHolders[i] = null;
                if (leftPlayerInput.gameObject != null)
                {
                    onPlayerLeft.Invoke(playerInputHolder);
                    Destroy(playerInputHolder.gameObject);
                }
                return;
            }
        }
    }
}
