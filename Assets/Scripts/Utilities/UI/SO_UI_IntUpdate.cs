using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SO_UI_IntUpdate : MonoBehaviour
{
    public SOInt sOInt;
    public TextMeshProUGUI uiTextValue;

    // Start is called before the first frame update
    void Start()
    {
        uiTextValue.text = sOInt.value.ToString();
    }

    // Update is called once per frame
    private void Update()
    {
        uiTextValue.text = "TESTE";
    }
}
