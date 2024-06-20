using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUi : MonoBehaviour
{

  //  Image background;
    public TextMeshProUGUI nameText;
  //  TextMeshProUGUI talkText;

    
    public void SetName(string name)
    {
        nameText.text = name;
    }

}
