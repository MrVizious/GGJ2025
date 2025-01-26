using DesignPatterns;
using UnityEngine;
using UnityEngine.InputSystem;
using EventfulData;
using UltEvents;

public class GameController : Singleton<GameController>
{
    protected override bool dontDestroyOnLoad => false;
    public Transform playerSpawnTransform;
    private timerController _timer;
    private timerController timer
    {
        get
        {
            if (_timer == null) _timer = FindFirstObjectByType<timerController>();
            return _timer;
        }
    }
    private BossController _bossController;
    [SerializeField]
    private int _lives;
    public int lives
    {
        get
        {
            return _lives;
        }
        set
        {
            _lives = value;
            onLivesChanged.Invoke(_lives.ToString());
        }
    }
    public UltEvent<string> onLivesChanged = new UltEvent<string>();
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

    public async void TakeDamage()
    {
        lives -= 1;
        if (lives <= 0)
        {
            (await sceneController.GetInstance()).GoToBurbujasScene();
        }
    }

    private void Start()
    {
        PreparePlayerInputs();
        lives = CalculatePlayers() * 3;
        timer.beginTimer(5);
        timer.onTimerEnded.AddListener(async () => (await sceneController.GetInstance()).GoToLooseScene());
    }

    private int CalculatePlayers()
    {
        int numberOfPlayers = 0;
        foreach (PlayerInputHolder playerInputHolder in multiplayerController.playerInputHolders)
        {
            if (playerInputHolder == null) continue;
            if (playerInputHolder.playerRole == PlayerInputHolder.PlayerRole.Bubble)
            {
                numberOfPlayers++;
            }
        }
        return numberOfPlayers;
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
        bossActionMap["Monster"].performed += bossController.GetComponent<BossStateMachine>().ProcessInputEvent;
        bossActionMap["Monster"].canceled += bossController.GetComponent<BossStateMachine>().ProcessInputEvent;
    }
}