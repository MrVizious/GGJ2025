using EventfulData;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MultiplayerRoleStateUI : MonoBehaviour
{
    public Transform bossPosition, nonePosition, bubblePosition;
    public Image image;
    public Sprite pressStartSprite, controllerSprite;
    public Color color;
    public EventfulClass<PlayerInputHolder> playerInputHolder = new EventfulClass<PlayerInputHolder>(null);
    private MultiplayerRoleSelection _multiplayerRoleSelection;
    private MultiplayerRoleSelection multiplayerRoleSelection
    {
        get
        {
            if (_multiplayerRoleSelection == null) _multiplayerRoleSelection = FindFirstObjectByType<MultiplayerRoleSelection>();
            return _multiplayerRoleSelection;
        }
    }

    private void Awake()
    {
        playerInputHolder.onValueChanged += UpdateState;
        ApplyColor();
        UpdateState();
    }

    private void SubscribeToUIActions()
    {
        if (playerInputHolder.value == null) return;
        PlayerInput playerInput = playerInputHolder.value.playerInput;
        // Access the Gameplay action map
        playerInput.SwitchCurrentActionMap("UI");
        InputActionMap bossActionMap = playerInput.actions.FindActionMap("UI");

        if (bossActionMap == null)
        {
            Debug.LogError("Gameplay action map not found!");
            return;
        }

        // Subscribe to specific actions
        bossActionMap["Navigate"].performed += MoveController;
    }

    private void OnDestroy()
    {
        PlayerInput playerInput = playerInputHolder.value.playerInput;
        InputActionMap bossActionMap = playerInput.actions.FindActionMap("UI");
        bossActionMap["Navigate"].performed -= MoveController;
    }

    private void MoveController(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>().normalized;
        if (direction.x < -0.1f)
        {
            if (playerInputHolder.value.playerRole == PlayerInputHolder.PlayerRole.None)
            {
                if (multiplayerRoleSelection.SetRole(playerInputHolder, PlayerInputHolder.PlayerRole.Bubble)) ;
            }
            else if (playerInputHolder.value.playerRole == PlayerInputHolder.PlayerRole.Boss)
            {
                multiplayerRoleSelection.SetRole(playerInputHolder, PlayerInputHolder.PlayerRole.None);
            }
        }
        else if (direction.x > 0.1f)
        {
            if (playerInputHolder.value.playerRole == PlayerInputHolder.PlayerRole.None)
            {
                multiplayerRoleSelection.SetRole(playerInputHolder, PlayerInputHolder.PlayerRole.Boss);
            }
            else if (playerInputHolder.value.playerRole == PlayerInputHolder.PlayerRole.Bubble)
            {
                multiplayerRoleSelection.SetRole(playerInputHolder, PlayerInputHolder.PlayerRole.None);
            }
        }
        UpdateState();

    }

    [Button]
    private void ApplyColor()
    {
        image.color = color;
    }
    private void UpdateState(PlayerInputHolder newPlayerInputHolder = null)
    {
        Debug.Log($"Updating state", this);
        if (newPlayerInputHolder == null) newPlayerInputHolder = playerInputHolder;
        if (newPlayerInputHolder == null)
        {
            image.sprite = pressStartSprite;
            image.transform.position = nonePosition.position;
        }
        else
        {
            if (newPlayerInputHolder.playerRole == PlayerInputHolder.PlayerRole.None)
            {
                image.sprite = controllerSprite;
                image.transform.position = nonePosition.position;
            }
            else if (newPlayerInputHolder.playerRole == PlayerInputHolder.PlayerRole.Boss)
            {
                image.sprite = controllerSprite;
                image.transform.position = bossPosition.position;
            }
            else if (newPlayerInputHolder.playerRole == PlayerInputHolder.PlayerRole.Bubble)
            {
                image.sprite = controllerSprite;
                image.transform.position = bubblePosition.position;
            }
        }
        SubscribeToUIActions();
    }
}
