using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int playerDamage = 10; // Dano causado pelo jogador ao atacar
    private PlayerController playerController; // Refer�ncia para o PlayerController do jogador

    
    void Start()
    {
        // Obt�m o componente PlayerController do pai deste objeto
        playerController = GetComponentInParent<PlayerController>();
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o PlayerController existe, se o jogador est� atacando e se a colis�o � com um inimigo
        if (playerController != null && playerController.IsAttacking() && collision.gameObject.CompareTag("Enemy"))
        {
            // Obt�m o componente SlimeController do objeto colidido
            SlimeController slimeController = collision.gameObject.GetComponent<SlimeController>();

            // Se o SlimeController existir, aplica o dano ao inimigo
            if (slimeController != null)
            {
                slimeController.TakeDamage(playerDamage);
            }
        }
    }
}
