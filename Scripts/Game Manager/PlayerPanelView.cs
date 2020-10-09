using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanelView : MonoBehaviour
{
    [SerializeField] private Text damageTakenText;
    [SerializeField] private Text nicknameText;
    [SerializeField] private Transform storedAbilityImageParent;
    [SerializeField] private Transform livesLeftImageParent;

    private PlayerView player;
    private PlayerDamage playerDamage;

    private readonly List<Image> storedAbilityImages = new List<Image>();
    private readonly List<Image> livesLeftImages = new List<Image>();

    private void Awake()
    {
        storedAbilityImageParent.GetComponentsInChildren(includeInactive: true, storedAbilityImages);
        livesLeftImageParent.GetComponentsInChildren(includeInactive: true, livesLeftImages);
    }

    public void Init(PlayerView player)
    {
        this.player = player;
        nicknameText.text = player.Nickname;

        playerDamage = player.GetComponent<PlayerDamage>();
        playerDamage.OnDamageTakenChanged += UpdateDamageTakenText;

        player.Deck.OnStoredAbilitiesChanged += UpdateStoredAbilityImages;
        gameObject.SetActive(true);

        var lives = player.GetComponent<PlayerLives>();
        lives.OnLivesChanged += UpdateLivesLeftImages;
    }

    private void UpdateLivesLeftImages(int lives)
    {
        int i;
        for (i = 0; i < lives; i++)
        {
            livesLeftImages[i].gameObject.SetActive(true);
        }

        while (i < 3)
        {
            livesLeftImages[i].gameObject.SetActive(false);
            i++;
        }
    }

    private void UpdateStoredAbilityImages()
    {
        int i = 0;
        foreach(var ability in player.Deck.storedAbilities)
        {
            storedAbilityImages[i].sprite = ability.Sprite;
            storedAbilityImages[i].gameObject.SetActive(true);
            i++;
        }

        while(i < 3)
        {
            storedAbilityImages[i].gameObject.SetActive(false);
            i++;
        }
    }

    private void UpdateDamageTakenText()
    {
        damageTakenText.text = playerDamage.DamageTaken.ToString() + "%";
    }
}
