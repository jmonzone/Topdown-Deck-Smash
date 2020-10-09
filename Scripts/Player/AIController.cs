using UnityEngine;

public class AIController : MonoBehaviour
{
    private PlayerView player;
    private PlayerMovement playerMovement;
    private Vector2 targetPosition;

    private void Awake()
    {
        enabled = false;

        var gameStart = GetComponent<GameStartView>();
        gameStart.OnGameStartComplete += () => enabled = true;

        var gameCompletion = GetComponent<GameCompletion>();
        gameCompletion.OnGameComplete += (args) => enabled = false;
    }

    public void Init(PlayerView player)
    {
        this.player = player;
        targetPosition = Random.insideUnitCircle.normalized * 3.0f;
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (!player) return;

        if (Vector2.Distance(targetPosition, player.transform.position) > 0.1f)
        {
            var direction = targetPosition - (Vector2)player.transform.position;
            playerMovement.Move(direction);
        }
        else
        {
            targetPosition = Random.insideUnitCircle.normalized * 3.0f;
        }
    }
}
