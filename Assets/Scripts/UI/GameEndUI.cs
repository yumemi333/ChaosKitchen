using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameEndUI : MonoBehaviour
{
    [SerializeField] private Button playAgainButton;
    [SerializeField] private GameObject gameEndMenuObj;

    private void Start()
    {
        playAgainButton.onClick.AddListener(LoadScene);
        KitchenGameManager.Instance.OnStateChanged += Instance_OnStateChanged;
    }

    private void Instance_OnStateChanged(object sender, System.EventArgs e)
    {
        gameEndMenuObj.SetActive(KitchenGameManager.Instance.IsGameEnd);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("Game");
    }
}
