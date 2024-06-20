using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public float _moveSpeedSlime = 3.5f; // Velocidade de movimento do slime
    public DetectionController _detectionArea; // Refer�ncia para o controle de detec��o do slime
    public SlimeAttack _slimeAttack; // Refer�ncia para o ataque do slime
    private Rigidbody2D _slimeRB2D; // Rigidbody do slime
    private SpriteRenderer _spriteRenderer; // Renderer do sprite do slime
    private Animator _slimeAnimator; // Animator do slime

    public Transform healthBar; // Barra de vida do slime
    public GameObject healtBarObject; // Objeto da barra de vida
    private Vector3 healtBarScale; // Escala da barra de vida
    private float healtPercent; // Porcentagem da vida para calcular a escala da barra
    public int health; // Quantidade de vida do slime

    public float destroyDelay = 0.5f; // Tempo de espera antes de destruir o slime ap�s a morte
    public int slimeDmg; // Dano causado pelo slime

    void Start()
    {
        _slimeRB2D = GetComponent<Rigidbody2D>(); // Obt�m o Rigidbody do slime
        _spriteRenderer = GetComponent<SpriteRenderer>(); // Obt�m o SpriteRenderer do slime
        _slimeAnimator = GetComponent<Animator>(); // Obt�m o Animator do slime

        // Configura��o inicial da barra de vida
        healtBarScale = healthBar.localScale;
        healtPercent = healtBarScale.x / health;
    }

    void Update()
    {
        // Se houver objetos na �rea de detec��o do slime
        if (_detectionArea.detectedObjs.Count > 0)
        {
            Collider2D detectedObject = _detectionArea.detectedObjs[0]; // Obt�m o primeiro objeto detectado

            Vector2 targetDirection = (detectedObject.transform.position - transform.position).normalized; // Dire��o para o objeto detectado

            // Se houver objetos na �rea de ataque do slime
            if (_slimeAttack.areaAttack.Count > 0)
            {
                // Se o slime estiver atualmente atacando
                if (_slimeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    // Se o objeto detectado ainda estiver na �rea de ataque do slime
                    if (_slimeAttack.areaAttack.Contains(detectedObject))
                    {
                        // Continue atacando
                        _slimeAnimator.SetInteger("MoveSlime", 2); // Ataque
                    }
                    else
                    {
                        // Se o objeto detectado n�o estiver mais na �rea de ataque do slime
                        // Pare de atacar
                        _slimeAnimator.SetInteger("MoveSlime", 0); // Parado
                    }
                }
                else
                {
                    // Se o slime n�o estiver atualmente atacando
                    // e o objeto detectado estiver na �rea de ataque do slime
                    // Inicie o ataque
                    if (_slimeAttack.areaAttack.Contains(detectedObject))
                    {
                        _slimeAnimator.SetInteger("MoveSlime", 2); // Ataque
                    }
                    else
                    {
                        // Se o objeto detectado n�o estiver na �rea de ataque do slime
                        // Mova-se em dire��o ao objeto detectado
                        _slimeAnimator.SetInteger("MoveSlime", 1); // Movendo
                        _slimeRB2D.velocity = targetDirection * _moveSpeedSlime;

                        // Vire o sprite do slime na dire��o do movimento
                        if (targetDirection.x > 0)
                        {
                            _spriteRenderer.flipX = false;
                        }
                        else if (targetDirection.x < 0)
                        {
                            _spriteRenderer.flipX = true;
                        }
                    }
                }
            }
            else
            {
                // Se n�o houver objetos na �rea de ataque do slime
                // Mova-se em dire��o ao objeto detectado
                _slimeAnimator.SetInteger("MoveSlime", 1); // Movendo
                _slimeRB2D.velocity = targetDirection * _moveSpeedSlime;

                // Vire o sprite do slime na dire��o do movimento
                if (targetDirection.x > 0)
                {
                    _spriteRenderer.flipX = false;
                }
                else if (targetDirection.x < 0)
                {
                    _spriteRenderer.flipX = true;
                }
            }
        }
        else
        {
            // Se n�o houver objetos na �rea de detec��o do slime
            // Pare de se mover e entre na anima��o de idle
            _slimeAnimator.SetInteger("MoveSlime", 0); // Parado
            _slimeRB2D.velocity = Vector2.zero;
        }

        // Se a vida do slime for menor ou igual a zero, destrua o slime e sua barra de vida
        if (healtBarScale.x <= 0)
        {
            Destroy(healtBarObject); // Destroi a barra de vida
            _slimeAnimator.SetBool("die", true); // Ativa a anima��o de morte
            Destroy(gameObject, destroyDelay); // Destroi o slime com um atraso
        }
    }

    // M�todo para atualizar a barra de vida do slime
    void UpdateHealthBar()
    {
        if (healthBar == null) return; // Verifica se o objeto healthBar foi destru�do

        healtBarScale.x = healtPercent * health; // Calcula a nova escala da barra de vida
        healthBar.localScale = healtBarScale; // Atualiza a escala da barra de vida
    }

    // M�todo para receber dano
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount; // Reduz a vida do slime
        UpdateHealthBar(); // Atualiza a barra de vida
    }
}
