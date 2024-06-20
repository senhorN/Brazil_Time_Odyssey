using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMode : MonoBehaviour
{
    //Instancia da classe authentication
    ManagerAutentication managerAuth_ = new ManagerAutentication();
    public Transform pauseButton;
    public Transform retomarButton;
    public Transform SairButton;
    public Transform Configuracao;
    public Transform modalSimButton;
    public Transform modalNaoButton;
    public Transform setaVoltarbtn;
    public GameObject telapause;
    public GameObject modalConfirmação;
    public GameObject telaConfiguracao;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            
            Pause();
        }
    }
    
#region botões da tela de pause 
    //Outros de botoes
    public void Pause(){
        
        telapause.SetActive(true);
        Time.timeScale = 0;
    }
    public void Retomar(){
        
        telapause.SetActive(false);
        Time.timeScale = 1;

    }
    public void ButtonExit(){
        
        modalConfirmação.SetActive(true);
    }

    public void ConfiguracaoButton(){
        
        //teste
        telaConfiguracao.SetActive(true);
    }
#endregion

#region modal de confirmação
    //botoes da modal de confirmação
    public void bntModalSim(){
        
        managerAuth_.Quit();
    }

    public void bntModalNao(){
        
        modalConfirmação.SetActive(false);
    }
#endregion

#region Tela de configuração
    public void btnConfigVoltar(){
        
        telaConfiguracao.SetActive(false);
        
    }
#endregion
}