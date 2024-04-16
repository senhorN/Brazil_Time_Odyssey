using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int playerDamage = 10; // Dano causado pelo jogador ao atacar
    private PlayerController playerController; // Referência para o PlayerController do jogador

    // Método chamado no início
    void Start()
    {
        // Obtém o componente PlayerController do pai deste objeto
        playerController = GetComponentInParent<PlayerController>();
    }

    // Método chamado quando este objeto colide com outro Collider2D
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o PlayerController existe, se o jogador está atacando e se a colisão é com um inimigo
        if (playerController != null && playerController.IsAttacking() && collision.gameObject.CompareTag("Enemy"))
        {
            // Obtém o componente SlimeController do objeto colidido
            SlimeController slimeController = collision.gameObject.GetComponent<SlimeController>();

            // Se o SlimeController existir, aplica o dano ao inimigo
            if (slimeController != null)
            {
                slimeController.TakeDamage(playerDamage);
            }
        }
    }
}
