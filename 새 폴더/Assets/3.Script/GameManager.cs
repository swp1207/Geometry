using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public GameObject GameoverUI;
    public GameObject GameClearUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("게임매니저 이미 있음");
            Destroy(gameObject);
        }
    }

    public bool isGameover = false;

    private void Update()
    {
        if (isGameover&& (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)))
        {
            SceneManager.LoadScene("MainGame");
        }
    }

    public void PlayerDead()
    {
        isGameover = true;
        GameoverUI.SetActive(true);
    }

    public void GameClear()
    {
        GameClearUI.SetActive(true);
    }

}
