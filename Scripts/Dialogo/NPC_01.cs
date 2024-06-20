using System.Collections.Generic;
using UnityEngine;

public class NPC_01 : MonoBehaviour
{
    // Referência ao objeto de dica
    public GameObject hint;

    // Tag do objeto que o NPC detecta
    public string _tagTargetDetection = "Player";

    // Lista de objetos detectados pelo NPC
    public List<Collider2D> detectedObjs = new List<Collider2D>();

    // Referência ao sistema de diálogo
    private DialogSystem dialogSystem;

    // Referência ao objeto UI
    public GameObject UI;

    // Referência ao controlador do Slime
    private SlimeController slimeController;

    private void Awake()
    {
        // Obtém uma referência ao sistema de diálogo
        dialogSystem = FindObjectOfType<DialogSystem>();
    }

    void Start()
    {
        // Obtém e configura referências aos componentes
        slimeController = GetComponent<SlimeController>();
        hint.SetActive(false); // Desativa a dica inicialmente
    }

    private void Update()
    {
        // Remove todos os itens nulos da lista de objetos detectados
        detectedObjs.RemoveAll(item => item == null);

        // Verifica se há objetos detectados e se a tecla E foi pressionada
        if (detectedObjs.Count > 0 && Input.GetKeyDown(KeyCode.E))
        {
            HandleInteraction(); // Manipula a interação
        }
    }

    // Método para lidar com a interação do jogador
    private void HandleInteraction()
    {
        UI.SetActive(true);    // Ativa a UI
        dialogSystem.Next();   // Avança para o próximo diálogo após ativar a UI
    }

    // Método chamado quando um objeto entra no trigger do NPC
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto que entrou tem a tag de detecção desejada
        if (collision.gameObject.tag == _tagTargetDetection)
        {
            detectedObjs.Add(collision);    // Adiciona o objeto à lista de objetos detectados
            EnableHint();                   // Ativa a dica
        }
    }

    // Método chamado quando um objeto sai do trigger do NPC
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Verifica se o objeto que saiu tem a tag de detecção desejada
        if (collision.gameObject.tag == _tagTargetDetection)
        {
            // Remove o objeto da lista de objetos detectados, se estiver presente
            if (detectedObjs.Contains(collision))
            {
                detectedObjs.Remove(collision);
            }

            DisableHint();  // Desativa a dica
            // Verifica se UI não é nulo antes de tentar desativá-lo
            if (UI != null)
            {
                UI.SetActive(false);    // Desativa a UI
            }
        }
    }

    // Método para ativar a dica
    public void EnableHint()
    {
        hint.SetActive(true);
    }

    // Método para desativar a dica
    public void DisableHint()
    {
        hint.SetActive(false);
    }
}
