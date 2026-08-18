using TMPro;
using UnityEngine;

public class CollectableHUD : MonoBehaviour
{
    public TextMeshProUGUI coinsText;

    private void Update()
    {
        coinsText.text = CollectableManager.Instance.coins.value.ToString();
    }
}