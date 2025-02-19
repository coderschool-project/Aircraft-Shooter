using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinCount : MonoBehaviour
{
    public TextMeshProUGUI coinCountText;
    int coinCount = 0;
    public TextMeshProUGUI coinText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coinCountText.text = coinCount.ToString();
        coinText.text = "Coins:"+ coinCount.ToString();
    }
    public void AddCoin()
    {
        coinCount++;
    }

}
