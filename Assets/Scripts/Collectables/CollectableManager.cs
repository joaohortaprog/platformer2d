using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Personal.Core.Singleton;

public class CollectableManager : Singleton<CollectableManager>
{
    public SOInt coins;

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        coins.value = 0;
    }

    public void AddCoins(int amount = 1)
    {
        coins.value += amount;
    }

}
