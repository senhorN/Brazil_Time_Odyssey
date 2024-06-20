using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaw_enemy : MonoBehaviour
{
    public List<GameObject> slimesToSpawn = new List<GameObject>(); // Lista de GameObjects dos slimes que serão spawnados
    public CircleCollider2D respawnArea; // Referência ao CircleCollider2D da área de respawn
    public float enemySpawnInterval = 10.0f; // Intervalo de tempo entre os spawns de inimigos
    public int maxRespawns;

    private float enemyTimer; // Timer para controlar os spawns de inimigos
    private int enemyCount = 0; // Contador de inimigos spawnados

    // Start is called before the first frame update
    void Start()
    {
        enemyTimer = enemySpawnInterval; // Inicializa o timer de inimigos
    }

    // Update is called once per frame
    void Update()
    {
        // Reduz o timer de inimigos
        enemyTimer -= Time.deltaTime;

        // Verifica se é hora de spawnar um inimigo
        if (enemyTimer <= 0 && enemyCount < maxRespawns)
        {
            SpawnEnemy();
            enemyCount++; // Incrementa o contador de inimigos
            enemyTimer = enemySpawnInterval; // Reseta o timer de inimigos
        }
    }

    // Função para spawnar o inimigo
    void SpawnEnemy()
    {
        // Escolhe aleatoriamente um slime da lista de slimes
        GameObject slimeToSpawn = slimesToSpawn[Random.Range(0, slimesToSpawn.Count)];

        // Calcula uma posição aleatória dentro da área de respawn
        Vector2 randomPosition = GetRandomPositionInRespawnArea();

        // Spawn o slime na posição calculada
        Instantiate(slimeToSpawn, randomPosition, Quaternion.identity);
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

    // Método para decrementar o contador de inimigos quando um inimigo for destruído
    public void DecreaseEnemyCount()
    {
        enemyCount--;
    }
}
