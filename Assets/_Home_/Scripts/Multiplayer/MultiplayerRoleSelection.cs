using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MultiplayerRoleSelection : MonoBehaviour
{
    private PlayerInputHolder bossPlayer = null;
    public MultiplayerRoleStateUI[] multiplayerRoleStateUIs = new MultiplayerRoleStateUI[4];

    private async void Start()
    {
        (await MultiplayerController.GetInstance()).DisconnectAllPlayers();
        (await MultiplayerController.GetInstance()).onPlayerJoined.AddListener(PlayerJoined);
        (await MultiplayerController.GetInstance()).onPlayerLeft.AddListener(PlayerLeft);
    }

    private void PlayerJoined(PlayerInputHolder newPlayerInputHolder)
    {
        for (int i = 0; i < multiplayerRoleStateUIs.Length; i++)
        {
            if (multiplayerRoleStateUIs[i].playerInputHolder.value == null)
            {
                multiplayerRoleStateUIs[i].playerInputHolder.value = newPlayerInputHolder;
                return;
            }
        }
    }
    private void PlayerLeft(PlayerInputHolder newPlayerInput)
    {
        for (int i = 0; i < multiplayerRoleStateUIs.Length; i++)
        {
            if (multiplayerRoleStateUIs[i].playerInputHolder.value == newPlayerInput)
            {
                multiplayerRoleStateUIs[i].playerInputHolder.value = null;
                return;
            }
        }
    }
    public bool SetRole(PlayerInputHolder playerInputHolder, PlayerInputHolder.PlayerRole newRole)
    {
        if (playerInputHolder == null) return false;
        if (newRole == PlayerInputHolder.PlayerRole.Boss)
        {
            if (bossPlayer == null)
            {
                playerInputHolder.playerRole.value = newRole;
                bossPlayer = playerInputHolder;
                return true;
            }
            return false;
        }
        if (playerInputHolder == bossPlayer)
        {
            bossPlayer = null;
        }
        playerInputHolder.playerRole.value = newRole;
        return true;
    }

    public async void GoToGame()
    {
        if (bossPlayer != null)
        {
            foreach (MultiplayerRoleStateUI roleState in multiplayerRoleStateUIs)
            {
                if (roleState.playerInputHolder.value != null && roleState.playerInputHolder.value.playerRole == PlayerInputHolder.PlayerRole.Bubble)
                {
                    (await sceneController.GetInstance()).GoToGameplayScene();
                    return;
                }
            }
        }
    }
}
