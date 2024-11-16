using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    private int coinCount = 0;
    private void Awake()
    {
        Instance = this;
    }

    public void AddCoins(int amount)
    {
        coinCount += amount;
    }
}
