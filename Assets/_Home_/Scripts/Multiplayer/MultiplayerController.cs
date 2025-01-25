using DesignPatterns;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MultiplayerController : Singleton<MultiplayerController>
{
    protected override bool dontDestroyOnLoad => true;
    [SerializeField]
    PlayerInput[] playerInputs = new PlayerInput[4];
    [SerializeField]
    Image[] images = new Image[4];
    private void Start()
    {
        foreach (Image image in images)
        {
            if (image == null) continue;
            image.enabled = false;
        }
    }
    public void OnPlayerJoined(PlayerInput newPlayerInput)
    {
        for (int i = 0; i < playerInputs.Length; i++)
        {
            if (playerInputs[i] == null)
            {
                playerInputs[i] = newPlayerInput;
                images[i].enabled = true;
                Debug.Log($"Added player Input {newPlayerInput}", this);
                return;
            }
        }
    }
    public void OnPlayerLeft(PlayerInput leftPlayerInput)
    {
        Debug.Log($"Player Input has left: {leftPlayerInput}", this);
        for (int i = 0; i < playerInputs.Length; i++)
        {
            if (playerInputs[i] == leftPlayerInput)
            {
                playerInputs[i] = null;
                images[i].enabled = false;
                if (leftPlayerInput.gameObject != null)
                {
                    Destroy(leftPlayerInput.gameObject);
                }
                return;
            }
        }
    }
}
