using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitHouse : MonoBehaviour
{
    
    [SerializeField] 
    private string _mudarsampleScene;
    public Vector2 PlayerPosition;
    public VectorValue playerStorage;


    private void OnTriggerEnter2D(Collider2D collision){
        
        // Verifica se o objeto que colidiu é o jogador
        if (collision.CompareTag("Player")) 
        {
            
            //collision.transform.position = new Vector3(-171.6f, -98.86f, 0.1095836f);
            playerStorage.InitialValue  = PlayerPosition;
            exitHouse();
        }
    }
    
    private void exitHouse(){
        
        SceneManager.LoadScene(_mudarsampleScene);

    }
}
