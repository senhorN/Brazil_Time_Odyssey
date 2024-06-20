using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Authentication : MonoBehaviour
{
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    ManagerAutentication manAuth = new ManagerAutentication();

    #region var para login 
    public TMP_InputField loginEmail;
    public TMP_InputField loginPassword;
    #endregion


    public Transform btnLogout;

    public GameObject TelaLogado_Canvas;
    public GameObject TelaLogin_Canvas;
    public GameObject TelaJogo_canvas;
    public GameObject txtUsuOuSenhaIncorreto;

    public void TriggertxtUsuOuSenhaIncorreto(bool v)
    {
        if (txtUsuOuSenhaIncorreto.activeInHierarchy == false)
        {
            txtUsuOuSenhaIncorreto.SetActive(true);
        }
        else
        {
            txtUsuOuSenhaIncorreto.SetActive(false);
        }
    }

    //Teaser
    public GameObject Teaser_Canvas;
    bool teaserEstaSendoExibido = true;

    //public RawImage rawImageTeaser;
    //bool teaserEstaSendoExibido = true;
    
#region métodos dos paineis 

    public VideoPlayer videoPlayer;
    //Methods
    public void MostrarPainelLogin()
    {

        MostraPaineis(TelaLogin_Canvas);

    }
    #region Tela de jogo
        public void MostrarPainelLogado()
        {
            MostraPaineis(TelaJogo_canvas);
        }

        public void SignOut(){
            
            auth.SignOut();
            Debug.Log("Usuário desautenticado.");

            MostraPaineis(TelaLogin_Canvas);
            
        }
    #endregion
    public void TelaConinuarConta(){
        
        MostraPaineis(TelaLogado_Canvas);
    }
    public void MostraPaineis(GameObject painel)
    {
        TelaLogin_Canvas.SetActive(false);
        TelaJogo_canvas.SetActive(false);
        Teaser_Canvas.SetActive(false);
        TelaLogado_Canvas.SetActive(false);

        //parameter painel activate
        painel.SetActive(true);
    }
    //durante o jogo o player pode pausar e sair e ir direto a tela de jogo 
    public void SairJogo(){
        
        MostraPaineis(TelaJogo_canvas);
    }

    //teaser 
    public void MostraTeaser()
    {
        MostraPaineis(Teaser_Canvas);
    }

#endregion
    //Main principal 
    void Start()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        StartCoroutine(IniciarJogo());

        manAuth = GetComponent<ManagerAutentication>();
        
    }

    void Update(){

    }
#region Cutscene Video

    public GameObject VideoPlayer2;
    
    //Enumerador que ira contar os segundos do video
    IEnumerator IniciarJogo(){

        MostraTeaser();
        float tempoInicial = Time.time;
        
        // Aguarda os 27 segundos ou o teaser seja fechado
        while (Time.time - tempoInicial < 27f && teaserEstaSendoExibido)
        {
            if(Input.GetKeyDown(KeyCode.Space)){
                
                VideoPlayer2.SetActive (false);
                //MostrarPainelLogin();
                manAuth.openTelaLogado();
            }
             // Aguarda o próximo quadro
            yield return null;
        }
            //MostrarPainelLogin();
            manAuth.openTelaLogado();
    }
    #endregion
}
