using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaw_enemy : MonoBehaviour
{
    public List<GameObject> slimesToSpawn = new List<GameObject>(); // Lista de GameObjects dos slimes que ser�o spawnados
    public CircleCollider2D respawnArea; // Refer�ncia ao CircleCollider2D da �rea de respawn
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

        // Verifica se � hora de spawnar um inimigo
        if (enemyTimer <= 0 && enemyCount < maxRespawns)
        {
            SpawnEnemy();
            enemyCount++; // Incrementa o contador de inimigos
            enemyTimer = enemySpawnInterval; // Reseta o timer de inimigos
        }
    }

    // Fun��o para spawnar o inimigo
    void SpawnEnemy()
    {
        // Escolhe aleatoriamente um slime da lista de slimes
        GameObject slimeToSpawn = slimesToSpawn[Random.Range(0, slimesToSpawn.Count)];

        // Calcula uma posi��o aleat�ria dentro da �rea de respawn
        Vector2 randomPosition = GetRandomPositionInRespawnArea();

        // Spawn o slime na posi��o calculada
        Instantiate(slimeToSpawn, randomPosition, Quaternion.identity);
    }

    // Fun��o para obter uma posi��o aleat�ria dentro da �rea de respawn
    Vector2 GetRandomPositionInRespawnArea()
    {
        // Calcula um ponto aleat�rio dentro do c�rculo da �rea de respawn
        float angle = Random.Range(0, 2 * Mathf.PI);
        float radius = Random.Range(0, respawnArea.radius);
        Vector2 randomPosition = new Vector2(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);

        // Converte a posi��o local para posi��o global
        randomPosition += (Vector2)respawnArea.transform.position;

        return randomPosition;
    }

    // M�todo para decrementar o contador de inimigos quando um inimigo for destru�do
    public void DecreaseEnemyCount()
    {
        enemyCount--;
    }
}
