using UnityEngine;
using UnityEngine.UI;

public class PlayerOffstageView : MonoBehaviour
{
    private PlayerOffstage offstage;
    private Slider offstageTimerSlider;
    private Camera cam;

    private void Awake()
    {
        offstageTimerSlider = GetComponent<Slider>();
        offstageTimerSlider.minValue = 0;
        offstageTimerSlider.maxValue = PlayerOffstage.MAX_OFFSTAGE_TIMER;

        cam = Camera.main;

        gameObject.SetActive(false);
    }

    public void Init(PlayerView player)
    {
        offstage = player.GetComponent<PlayerOffstage>();
        offstage.OnInArenaChanged += OnInArenaChanged;
        offstage.OnOffstageTimerChanged += OnOffstageTimerChanged;

        var lives = player.GetComponent<PlayerLives>();
        lives.OnDeath += () => gameObject.SetActive(false);
    }

    private void OnOffstageTimerChanged()
    {
        offstageTimerSlider.value = offstage.OffstageTimer;
    }

    private void OnInArenaChanged()
    {
        gameObject.SetActive(!offstage.InArena);
    }

    private void Update()
    {
        var targetPosition = cam.WorldToScreenPoint(offstage.transform.position) + Vector3.up * 50f;
        transform.position = targetPosition;
    }
}
