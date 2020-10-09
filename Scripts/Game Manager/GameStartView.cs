using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameStartView : MonoBehaviour
{
    [SerializeField] private Text startView;

    public event Action OnGameStartComplete;

    private void Awake()
    {
        var gameStart = GetComponent<GameStart>();
        gameStart.OnGameStart += OnGameStart;
    }

    private void OnGameStart()
    {
        StartCoroutine(ViewUpdate());
    }

    private IEnumerator ViewUpdate()
    {
        startView.gameObject.SetActive(true);
        while(startView.fontSize < 250f)
        {
            startView.fontSize += 4;
            yield return new WaitForFixedUpdate();
        }
        startView.gameObject.SetActive(false);

        OnGameStartComplete?.Invoke();
    }
}
