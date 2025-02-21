using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] GameObject gameObject;

    void Awake()
    {
        text = GetComponent<TMP_Text>();    
    }

    void Start()
    {
        text.text = $"coins en inventario: {Coin.count} / {Coin.maxCount}";
    }

    void Update()
    {
        text.text = $"coins en inventario: {Coin.count} / {Coin.maxCount}";
    }

    private void FixedUpdate()
    {
        if (Coin.count == Coin.maxCount)
        {
            gameObject.SetActive(true);
            Coin.ResetCoins();
            Invoke("LoadMainMenu", 3f);
        }
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
