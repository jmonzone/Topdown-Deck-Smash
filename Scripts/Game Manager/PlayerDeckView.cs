using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeckView : MonoBehaviour
{
    [SerializeField] private Image currentAbilityImage;
    [SerializeField] private Transform nextAbilityImagesParent;
    [SerializeField] private Sprite reloadSprite;

    private readonly List<Image> nextAbilityImages = new List<Image>();

    private AbilityDeck deck;

    private void Awake()
    {
        var playerSpawner = GetComponent<PlayerSpawner>();
        playerSpawner.OnLocalPlayerSpawned += Init;

        nextAbilityImagesParent.GetComponentsInChildren(includeInactive: true, nextAbilityImages);
    }

    private void Init(PlayerView player)
    {
        deck = player.Deck;
        deck.OnCurrentAbilityChanged += UpdateDeckView;

        UpdateDeckView();
    }

    private void UpdateDeckView()
    {
        bool reloadVisible = false;

        if (deck.CurrentAbility != null) currentAbilityImage.sprite = deck.CurrentAbility.Sprite;
        else
        {
            currentAbilityImage.sprite = reloadSprite;
            reloadVisible = true;
        }

        for (int i = 0; i < 3; i++)
        {
            var ability = deck.GetAbility(i + 1);

            nextAbilityImages[i].gameObject.SetActive(!reloadVisible);
            
            if (ability != null) nextAbilityImages[i].sprite = ability.Sprite;
            else
            {
                nextAbilityImages[i].sprite = reloadSprite;
                reloadVisible = true;
            }
        }
    }
}
