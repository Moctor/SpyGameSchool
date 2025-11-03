using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class Scripts_CoinCount : MonoBehaviour
{
    TMPro.TMP_Text text;
    int count;

    private void Awake()
    {
        text = GetComponent<TMPro.TMP_Text>();
    }

    private void OnEnable()
    {
        Scripts_Coin.OnCollected += OnCoinCollected;
    }

    private void OnDisable()
    {
        Scripts_Coin.OnCollected -= OnCoinCollected;
    }

    private void OnCoinCollected()
    {
        count++;
        UpdateCount();
    }

    private void UpdateCount()
    {
        text.text = $"{count}";
    }
}
