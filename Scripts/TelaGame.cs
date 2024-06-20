using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;
using TMPro;
public class TelaGame : MonoBehaviour
{
    public TextMeshProUGUI buttonTxt;

    public void OnClickButtonGame(){
        
        SceneManager.LoadScene("Fora-Cutscene");
        //SceneManager.LoadScene("SampleScene");
    }

}
