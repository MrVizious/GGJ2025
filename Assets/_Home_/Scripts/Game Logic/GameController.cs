using DesignPatterns;
using UnityEngine;
using UnityEngine.InputSystem;
using EventfulData;

public class GameController : Singleton<GameController>
{
    protected override bool dontDestroyOnLoad => false;
    public Transform playerSpawnTransform;
    private BossController _bossController;
    public sceneController sc;
    public EventfulStruct<int> Lives = new EventfulStruct<int>(6);
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

    public void TakeDamage()
    {
        Lives -= 1;
        Debug.Log(Lives);
        if (Lives <= 0)
        {
            sc.GoToBurbujasScene();
        }
    }

    private void Start()
    {
        PreparePlayerInputs();
    }

    private void PreparePlayerInputs()
    {
        foreach (PlayerInputHolder playerInputHolder in multiplayerController.playerInputHolders)
        {
            if (playerInputHolder == null) continue;
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
    public void RespawnPlayer(PlayerMovement player)
    {
        player.rb.linearVelocity = Vector3.zero;
        player.transform.position = playerSpawnTransform.position;
        player.transform.rotation = playerSpawnTransform.rotation;
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

        RespawnPlayer(newPlayer);
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