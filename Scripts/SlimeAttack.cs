using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttack : MonoBehaviour
{
    public int slimeDamage = 5; // Dano causado pelo slime ao atacar

    public string _tagTargetDetection = "Player"; // Tag do objeto que o slime deve atacar

    public List<Collider2D> areaAttack = new List<Collider2D>(); // Lista de colisores na área de ataque do slime

    // Método chamado quando um objeto entra na área de colisão deste objeto
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto que entrou na área de colisão tem a tag de destino
        if (collision.gameObject.tag == _tagTargetDetection)
        {
            // Adiciona o colisor à lista de área de ataque
            areaAttack.Add(collision);

            // Obtém o componente PlayerController do objeto colidido
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                // Se o PlayerController existir, aplica o dano ao jogador
                playerController.TakeDamage(slimeDamage);
            }
        }
    }

    // Método chamado quando um objeto sai da área de colisão deste objeto
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Verifica se o objeto que saiu da área de colisão tem a tag de destino
        if (collision.gameObject.tag == _tagTargetDetection)
        {
            // Remove o colisor da lista de área de ataque
            areaAttack.Remove(collision);
        }
    }
}
