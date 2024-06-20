using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using Firebase.Firestore;

[FirestoreData]
public class ManagerAutentication : MonoBehaviour
{
    //instancia 
    Authentication authentication;
    
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    void Start()
    {
        InicializeFirebase();

        authentication = GetComponent<Authentication>();
    }

    void Update()
    {

    }

    //função com a chamada do firebase
    void InicializeFirebase() {

        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AutoStateChanged;
        AutoStateChanged(this, null);
    }
    private void AutoStateChanged(object sender, EventArgs eventArgs) {
        if (auth.CurrentUser != user) {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (signedIn && user != null){
                
                Debug.Log($"Usuário se desconectou {user.UserId}" );
            }
            user = auth.CurrentUser;
            if (signedIn)
            { 
                Debug.Log($"Usuário logado {user.UserId}");
                authentication.MostrarPainelLogado();
            }

        }
    }
    //Encerrado a conex�o
    void OnDestroy()
    {
        auth.StateChanged -= AutoStateChanged;
        
        if (auth.CurrentUser != null)
        {
            auth.SignOut();
        }
    }

#region script login 
    public void OnLoginButtonClick() {
        
        LoginFirebase(authentication.loginEmail.text, authentication.loginPassword.text);
    }
    public void LoginFirebase(string email, string password) {

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {

        Firebase.Auth.AuthResult authResult = task.Result;
        Firebase.Auth.FirebaseUser newUser = authResult.User;
        //Debug.Log($" Usuário conectado {newUser.UserId}");
        //Debug.Log($" Usuário conectado {newUser.Email}");
        
        });
        if (email == "" && password == "") {
            
            authentication.TriggertxtUsuOuSenhaIncorreto(true);
            return; 
        }
        
    }
#endregion

//Vou criar um metodo aqui no ManagerAuthentication 
//faço todo o tratamento aqui e chamo ele no Authentication
#region EnterAccount
    public void openTelaLogado(){

        if(auth.CurrentUser != null){
            authentication.MostrarPainelLogado();
        } else {
            authentication.MostrarPainelLogin();
        }
    }    
#endregion


#region Buttons quit and link 
    public void Quit()
    {
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
        
    }
    public void Link() {

        Application.OpenURL("https://braziltimeodyssey.netlify.app/register");
    }
#endregion
}
