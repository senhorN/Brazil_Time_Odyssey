using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttack : MonoBehaviour
{
    public int slimeDamage; // Dano causado pelo slime ao atacar

    public string _tagTargetDetection = "Player"; // Tag do objeto que o slime deve atacar

    public List<Collider2D> areaAttack = new List<Collider2D>(); // Lista de colisores na �rea de ataque do slime

    // M�todo chamado quando um objeto entra na �rea de colis�o deste objeto
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto que entrou na �rea de colis�o tem a tag de destino
        if (collision.gameObject.tag == _tagTargetDetection)
        {
            // Adiciona o colisor � lista de �rea de ataque
            areaAttack.Add(collision);

            // Obt�m o componente PlayerController do objeto colidido
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                // Se o PlayerController existir, aplica o dano ao jogador
                playerController.TakeDamage(slimeDamage);
            }
        }
    }

    // M�todo chamado quando um objeto sai da �rea de colis�o deste objeto
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Verifica se o objeto que saiu da �rea de colis�o tem a tag de destino
        if (collision.gameObject.tag == _tagTargetDetection)
        {
            // Remove o colisor da lista de �rea de ataque
            areaAttack.Remove(collision);
        }
    }
}
