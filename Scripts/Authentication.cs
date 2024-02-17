using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Authentication : MonoBehaviour
{
    #region var para login 
    public TMP_InputField loginEmail;
    public TMP_InputField loginPassword;
    #endregion

    public GameObject TelaLogin_Canvas;
    public GameObject TelaJogo_canvas;

    
    public GameObject txtUsuOuSenhaIncorreto;
    public void TriggertxtUsuOuSenhaIncorreto(bool v) {
        if (txtUsuOuSenhaIncorreto.activeInHierarchy == false)
        {
            txtUsuOuSenhaIncorreto.SetActive(true);
        }
        else {
            txtUsuOuSenhaIncorreto.SetActive(false);
        }
    }
    

 
    void Start()
    {
        MostrarPainelLogin();
    }

    
    void Update()
    {
        
    }

    //Methods
    public void MostrarPainelLogin() {
        
        MostraPaineis(TelaLogin_Canvas);

    }

    public void MostrarPainelLogado() {

        MostraPaineis(TelaJogo_canvas);

    }

    public void MostraPaineis(GameObject painel)
    {
        TelaLogin_Canvas.SetActive(false);
        TelaJogo_canvas.SetActive(false);

        //parameter painel activate
        painel.SetActive(true);
    }

}
