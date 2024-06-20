using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogUI_C : MonoBehaviour
{
    //  Image background;
    public TextMeshProUGUI nameText;
    //  TextMeshProUGUI talkText;


    public void SetName(string name)
    {
        nameText.text = name;
    }
}
