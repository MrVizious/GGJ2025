using EventfulData;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerRoleStateUI : MonoBehaviour
{
    public Transform bossPosition, nonePosition, bubblePosition;
    public Image image;
    public Sprite pressStartSprite, controllerSprite;
    public Color color;
    public EventfulClass<PlayerInputHolder> playerInputHolder = new EventfulClass<PlayerInputHolder>(null);

    private void Awake()
    {
        playerInputHolder.onValueChanged += UpdateState;
        ApplyColor();
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
    }
}
