using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransictionScene : MonoBehaviour
{
    //Deixa a Unity editavel
    [SerializeField]
    private string _HousePlayerInterior;

    public Vector2 PlayerPosition2;
    public VectorValue playerStorage;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            
            interior_House();
        }
    }
    
    private void interior_House() { 

        SceneManager.LoadScene(this._HousePlayerInterior);
    }
}
