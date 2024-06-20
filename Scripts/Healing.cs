using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    public int valorCura;
    public string _tagTargetDetection = "Player"; // Tag do objeto que o slime deve curar

    // Método chamado quando um objeto entra na área de colisão deste objeto
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto que entrou na área de colisão tem a tag de destino
        if (collision.gameObject.tag == _tagTargetDetection)
        {
            // Obtém o componente PlayerController do objeto colidido
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.Healing(valorCura);
                Destroy(gameObject);
            }
        }
    }

    // No script do item ou onde você detectar a destruição do item
    private void OnDestroy()
    {
        RespawArea respawnArea = FindObjectOfType<RespawArea>();
        if (respawnArea != null)
        {
            respawnArea.DecreaseItemCount();
        }
    }
}
