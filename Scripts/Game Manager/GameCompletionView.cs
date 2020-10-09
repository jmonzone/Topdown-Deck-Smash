using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCompletionView : MonoBehaviour
{
    [SerializeField] private Text gameCompletionText;

    private void Awake()
    {
        var gameCompletion = GetComponent<GameCompletion>();
        gameCompletion.OnGameComplete += OnGameComplete;
    }

    private void OnGameComplete(PlayerView player)
    {
        gameCompletionText.text = $"{player.Nickname} has won!";
        gameCompletionText.gameObject.SetActive(true);
    }
}
