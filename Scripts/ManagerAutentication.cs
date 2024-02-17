using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;


public class ManagerAutentication : MonoBehaviour
{
    //instancia 
    Authentication authentication;

    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;


    void Start()
    {
        authentication = GetComponent<Authentication>();
        
        InicializeFirebase();
    }

    void Update()
    {
        
    }

    //fun��o com a chamada do firebase
    void InicializeFirebase() {

        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AutoStateChanged;
        AutoStateChanged(this, null);
    }
    private void AutoStateChanged(object sender, EventArgs eventArgs) {
        if (auth.CurrentUser != user) {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (signedIn && user != null){
                Debug.Log($"Usu�rio se desconectou {user.UserId}" );
            }
            user = auth.CurrentUser;
            if (signedIn)
            { 
                Debug.Log($"Us�rio logado {user.UserId}");
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
        if (email == "" || password == "") {
            
            Debug.Log("Usu�rio ou senha n�o foi informado");
            authentication.TriggertxtUsuOuSenhaIncorreto(true);
            
            return;
        }
        else{

            authentication.TriggertxtUsuOuSenhaIncorreto(false);
            
        }
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {

                if (task.IsCanceled) {
                    Debug.Log("A��o cancelada!");
                    return;
                }
                if (task.IsFaulted) {

                    Debug.Log($"Ocorreum erro de login {task.Exception}");
                    // Exibe a mensagem de erro no texto
                    return;
                }
                Firebase.Auth.AuthResult authResult = task.Result;
                Firebase.Auth.FirebaseUser newUser = authResult.User;
                Debug.Log($" Usu�rio conectado {newUser.UserId}");
                Debug.Log($" Usu�rio conectado {newUser.Email}");
                
            });
        
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

        Application.OpenURL("https://www.youtube.com/");
    }
    #endregion
}
