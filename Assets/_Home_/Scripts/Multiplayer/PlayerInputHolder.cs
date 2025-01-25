using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputHolder : MonoBehaviour
{
    private PlayerInput _playerInput;
    private PlayerInput playerInput
    {
        get
        {
            if (_playerInput == null) TryGetComponent<PlayerInput>(out _playerInput);
            return _playerInput;
        }
    }
    public enum PlayerRole
    {
        None,
        Boss,
        Bubble
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        playerInput.onDeviceLost += OnDeviceLost;
    }

    private async void OnDeviceLost(PlayerInput lostPlayerInput)
    {
        (await MultiplayerController.GetInstance()).OnPlayerLeft(playerInput);
    }


}