using System.Collections.Generic;
using UnityEngine;

public class NPC_01 : MonoBehaviour
{
    // Refer�ncia ao objeto de dica
    public GameObject hint;

    // Tag do objeto que o NPC detecta
    public string _tagTargetDetection = "Player";

    // Lista de objetos detectados pelo NPC
    public List<Collider2D> detectedObjs = new List<Collider2D>();

    // Refer�ncia ao sistema de di�logo
    private DialogSystem dialogSystem;

    // Refer�ncia ao objeto UI
    public GameObject UI;

    // Refer�ncia ao controlador do Slime
    private SlimeController slimeController;

    private void Awake()
    {
        // Obt�m uma refer�ncia ao sistema de di�logo
        dialogSystem = FindObjectOfType<DialogSystem>();
    }

    void Start()
    {
        // Obt�m e configura refer�ncias aos componentes
        slimeController = GetComponent<SlimeController>();
        hint.SetActive(false); // Desativa a dica inicialmente
    }

    private void Update()
    {
        // Remove todos os itens nulos da lista de objetos detectados
        detectedObjs.RemoveAll(item => item == null);

        // Verifica se h� objetos detectados e se a tecla E foi pressionada
        if (detectedObjs.Count > 0 && Input.GetKeyDown(KeyCode.E))
        {
            HandleInteraction(); // Manipula a intera��o
        }
    }

    // M�todo para lidar com a intera��o do jogador
    private void HandleInteraction()
    {
        UI.SetActive(true);    // Ativa a UI
        dialogSystem.Next();   // Avan�a para o pr�ximo di�logo ap�s ativar a UI
    }

    // M�todo chamado quando um objeto entra no trigger do NPC
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto que entrou tem a tag de detec��o desejada
        if (collision.gameObject.tag == _tagTargetDetection)
        {
            detectedObjs.Add(collision);    // Adiciona o objeto � lista de objetos detectados
            EnableHint();                   // Ativa a dica
        }
    }

    // M�todo chamado quando um objeto sai do trigger do NPC
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Verifica se o objeto que saiu tem a tag de detec��o desejada
        if (collision.gameObject.tag == _tagTargetDetection)
        {
            // Remove o objeto da lista de objetos detectados, se estiver presente
            if (detectedObjs.Contains(collision))
            {
                detectedObjs.Remove(collision);
            }

            DisableHint();  // Desativa a dica
            // Verifica se UI n�o � nulo antes de tentar desativ�-lo
            if (UI != null)
            {
                UI.SetActive(false);    // Desativa a UI
            }
        }
    }

    // M�todo para ativar a dica
    public void EnableHint()
    {
        hint.SetActive(true);
    }

    // M�todo para desativar a dica
    public void DisableHint()
    {
        hint.SetActive(false);
    }
}
