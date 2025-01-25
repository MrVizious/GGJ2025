using DesignPatterns;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : Singleton<GameController>
{
    protected override bool dontDestroyOnLoad => false;
    private BossController _bossController;
    private BossController bossController
    {
        get
        {
            if (_bossController == null) _bossController = FindFirstObjectByType<BossController>();
            return _bossController;
        }
    }
    private MultiplayerController _multiplayerController;
    private MultiplayerController multiplayerController
    {
        get
        {
            if (_multiplayerController == null) _multiplayerController = FindFirstObjectByType<MultiplayerController>();
            return _multiplayerController;
        }
    }

    public PlayerMovement playerPrefab;

    private void Start()
    {
        PreparePlayerInputs();
        PrepareGameData();
    }

    private void PreparePlayerInputs()
    {
        foreach (PlayerInputHolder playerInputHolder in multiplayerController.playerInputHolders)
        {
            if (playerInputHolder.playerRole == PlayerInputHolder.PlayerRole.Boss)
            {
                SubscribeToBossActions(playerInputHolder.playerInput);
            }
            else if (playerInputHolder.playerRole == PlayerInputHolder.PlayerRole.Bubble)
            {
                PrepareBubblePlayer(playerInputHolder.playerInput);
            }
        }
    }

    private async void PrepareGameData()
    {
        PlayerInputHolder[] playerInputHolders = (await MultiplayerController.GetInstance()).playerInputHolders;
        foreach (PlayerInputHolder playerInputHolder in playerInputHolders)
        {
            if (playerInputHolder == null || playerInputHolder.playerInput == null) continue;
            if (playerInputHolder.playerRole == PlayerInputHolder.PlayerRole.Boss)
            {
                SubscribeToBossActions(playerInputHolder.playerInput);
            }
        }
    }

    private void PrepareBubblePlayer(PlayerInput playerInput)
    {
        // Access the Gameplay action map
        playerInput.SwitchCurrentActionMap("Bubble");
        InputActionMap bubbleActionMap = playerInput.actions.FindActionMap("Bubble");



        if (bubbleActionMap == null)
        {
            Debug.LogError("Gameplay action map not found!");
            return;
        }

        PlayerMovement newPlayer = Instantiate(playerPrefab);

        // Subscribe to specific actions
        bubbleActionMap["Move"].performed += newPlayer.UpdateMoveVector;
        bubbleActionMap["Move"].canceled += newPlayer.UpdateMoveVector;

        bubbleActionMap["Dash"].performed += newPlayer.Dash;
    }
    private void SubscribeToBossActions(PlayerInput playerInput)
    {
        // Access the Gameplay action map
        playerInput.SwitchCurrentActionMap("Boss");
        InputActionMap bossActionMap = playerInput.actions.FindActionMap("Boss");

        if (bossActionMap == null)
        {
            Debug.LogError("Gameplay action map not found!");
            return;
        }

        // Subscribe to specific actions
        BossMovement bossMovement = bossController.GetComponent<BossMovement>();
        bossActionMap["Move"].performed += bossMovement.UpdateMoveVector;
        bossActionMap["Move"].canceled += bossMovement.UpdateMoveVector;

        bossActionMap["Cheeto"].performed += bossController.GetComponent<BossStateMachine>().ProcessInputEvent;
        bossActionMap["Cheeto"].canceled += bossController.GetComponent<BossStateMachine>().ProcessInputEvent;
        bossActionMap["Kunai"].performed += bossController.GetComponent<BossStateMachine>().ProcessInputEvent;
        bossActionMap["Kunai"].canceled += bossController.GetComponent<BossStateMachine>().ProcessInputEvent;
        bossActionMap["Dice"].performed += bossController.GetComponent<BossStateMachine>().ProcessInputEvent;
        bossActionMap["Dice"].canceled += bossController.GetComponent<BossStateMachine>().ProcessInputEvent;
        bossActionMap["Fart"].performed += bossController.GetComponent<BossStateMachine>().ProcessInputEvent;
        bossActionMap["Fart"].canceled += bossController.GetComponent<BossStateMachine>().ProcessInputEvent;
    }
}