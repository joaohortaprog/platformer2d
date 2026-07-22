using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Personal.Core.Singleton;

public class CollectableManager : Singleton<CollectableManager>
{
    public int coins;

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        coins = 0;
    }

    public void AddCoins(int amount = 1)
    {
        coins += amount;
    }

}
