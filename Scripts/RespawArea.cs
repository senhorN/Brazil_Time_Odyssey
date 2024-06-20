using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawArea : MonoBehaviour
{
    public GameObject itemToSpawn; // Referência ao GameObject que será spawnado
    public CircleCollider2D respawnArea; // Referência ao CircleCollider2D da área de respawn
    public float spawnInterval = 5.0f; // Intervalo de tempo entre os spawns
    public float lowHealth;

    private float timer; // Timer para controlar os spawns
    private PlayerController playerController; // Referência ao PlayerController
    private int itemCount = 0; // Contador de itens spawnados

    // Start is called before the first frame update
    void Start()
    {
        timer = spawnInterval; // Inicializa o timer para permitir o primeiro spawn
        playerController = FindObjectOfType<PlayerController>(); // Encontra o PlayerController na cena
        lowHealth = (playerController.health / 100) * 30;
    }

    // Update is called once per frame
    void Update()
    {
        // Obtém a vida do jogador do PlayerController
        float playerHealth = playerController.health;

        // Reduz o timer
        timer -= Time.deltaTime;

        // Verifica se é hora de spawnar um item
        if ((timer <= 0 && Random.value > 0.5f) || playerHealth < lowHealth)
        {
            // Verifica se o número de itens no mapa é menor que 10
            if (itemCount < 10)
            {
                SpawnItem();
                itemCount++; // Incrementa o contador de itens
                timer = spawnInterval; // Reseta o timer
            }
        }
    }

    // Função para spawnar o item
    void SpawnItem()
    {
        // Calcula uma posição aleatória dentro da área de respawn
        Vector2 randomPosition = GetRandomPositionInRespawnArea();

        // Spawn o item na posição calculada
        Instantiate(itemToSpawn, randomPosition, Quaternion.identity);
    }

    // Função para obter uma posição aleatória dentro da área de respawn
    Vector2 GetRandomPositionInRespawnArea()
    {
        // Calcula um ponto aleatório dentro do círculo da área de respawn
        float angle = Random.Range(0, 2 * Mathf.PI);
        float radius = Random.Range(0, respawnArea.radius);
        Vector2 randomPosition = new Vector2(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);

        // Converte a posição local para posição global
        randomPosition += (Vector2)respawnArea.transform.position;

        return randomPosition;
    }

    // Método para decrementar o contador de itens quando um item for destruído
    public void DecreaseItemCount()
    {
        itemCount--;
    }
}
