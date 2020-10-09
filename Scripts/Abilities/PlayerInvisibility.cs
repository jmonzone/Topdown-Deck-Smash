using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAbility
{
    public AbilityId Id { get; private set; }
    public Sprite Sprite { get; private set; }

    protected PlayerView player;
    private readonly Camera cam;

    protected PlayerAbility(PlayerView player, AbilityId id)
    {
        cam = Camera.main;
        Id = id;
        Sprite = Resources.Load<Sprite>("Ability Sprites/" + id.ToString());
        this.player = player;
    }

    public abstract void Cast();
    protected Vector2 GetTargetPosition => cam.ScreenToWorldPoint(Input.mousePosition);
    protected Vector2 GetTargetDirection => (GetTargetPosition - (Vector2)player.transform.position).normalized;
}

public class PlayerInvisibility : PlayerAbility
{
    public PlayerInvisibility(PlayerView player, AbilityId spriteName) : base(player, spriteName)
    {
    }

    public override void Cast()
    {
        player.SetVisible(false);
        player.StartCoroutine(InvisibilityUpdate());
    }
    private IEnumerator InvisibilityUpdate()
    {
        yield return new WaitForSeconds(1.5f);
        player.SetVisible(true);
    }
}
