using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverController : MonoBehaviour
{
    ManagerAutentication managerAuth_ = new ManagerAutentication();

    public Transform btnSim;
    public Transform btnNao;

    public GameObject screenGameOver; 
    
    [SerializeField] 
    private string _reiniciarampleScene;

    public void validationGameOver(bool validation, int dano){

        if(dano <= 0 ){
            validation = true;
            GameOver(1);
        }
        if(dano >= 1){
            GameOver(0);
            validation = false;
        }
    }

    public void GameOver(int a){

        if(a == 1){
            
            screenGameOver.SetActive(true);
            
            //Time.timeScale = 0;
        } 
        if(a != 1){
            
            screenGameOver.SetActive(false);
        }    
    }
    
    public void Reiniciar(){
        
        screenGameOver.SetActive(false);
        
        
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1;
    }

    //botões
    public void BtnSim(){
        
        Reiniciar();
    }

    public void BtnNao(){
        
        managerAuth_.Quit();
    }
}
