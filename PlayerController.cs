using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    #region variable
    // Vari�veis para anima��es e movimentos
    private Rigidbody2D _playerRigidbody2D; // Rigidbody do jogador para movimento f�sico
    private Vector2 _playerDirection; // Dire��o do jogador
    private Animator _playerAnimator; // Componente de anima��o do jogador
    private bool _isAttack = false; // Flag para indicar se o jogador est� atacando

    // Vari�veis para combate
    public float _playerSpeed; // Velocidade de movimento do jogador
    public float _playerInitialSpeed; // Velocidade inicial do jogador
    public float _playerRunSpeed; // Velocidade de corrida do jogador
    public CircleCollider2D _areaAtk; // �rea de ataque do jogador
    public Transform healthBar; // Barra de vida do jogador
    public GameObject healtBarObject; // Objeto da barra de vida
    public int health; // Quantidade de vida do jogador
    private Vector3 healtBarScale; // Escala da barra de vida
    private float healtPercent; // Porcentagem da vida para calcular a escala da barra

    
    


    #endregion

    public VectorValue startingPosition;


    

    // M�todo chamado no in�cio do jogo
    void Start()
    {
        transform.position = startingPosition.InitialValue; 

        AttackEnable(); // Desabilita o ataque no in�cio

        _playerRigidbody2D = GetComponent<Rigidbody2D>(); // Obt�m o Rigidbody do jogador
        _playerAnimator = GetComponent<Animator>(); // Obt�m o Animator do jogador

        _playerSpeed = _playerInitialSpeed; // Define a velocidade inicial do jogador

        // Configura��o inicial da barra de vida
        healtBarScale = healthBar.localScale;
        healtPercent = healtBarScale.x / health;
    }

    // M�todo chamado a cada quadro
    void Update() {
        
        // Obt�m a dire��o do jogador a partir dos controles de entrada
        _playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Atualiza as anima��es do jogador de acordo com a dire��o
        _playerAnimator.SetFloat("x", _playerDirection.x);
        _playerAnimator.SetFloat("y", _playerDirection.y);

        // Define a anima��o de movimento com base na magnitude da dire��o
        if (_playerDirection.sqrMagnitude > 0)
        {
            _playerAnimator.SetInteger("Movimento", 1); // Movendo-se
        }
        else
        {
            _playerAnimator.SetInteger("Movimento", 0); // Parado
        }

        PlayerRun(); // Atualiza a velocidade de acordo com a a��o de correr

        // Se o jogador estiver atacando, habilita a �rea de ataque e define a anima��o
        if (_isAttack)
        {
            _areaAtk.enabled = true;
            _playerAnimator.SetInteger("Movimento", 2); // Ataque
        }

        // Verifica se o jogador iniciou um ataque
        if (Input.GetMouseButtonDown(0))
        {
            OnAttack();
        }

        // Verifica se a vida do jogador chegou a zero para morrer
        if (healtBarScale.x <= 0)
        {
            Morrer();
        }

        // Verifica se o jogador pressionou a tecla de regenera��o de vida
        if (Input.GetKey(KeyCode.Space))
        {
            if (health < 100)
            {
                health = 100;
            }
            UpdateHealthBar();
        }
    }

    // M�todo para a morte do jogador
    void Morrer()
    {
        _playerAnimator.SetBool("Die", true); // Ativa a anima��o de morte
        Destroy(healtBarObject); // Destroi a barra de vida
        //SceneManager.LoadScene("InitialScene"); // Reinicia a cena (comentado para desabilitar)
    }

    // M�todo para habilitar o ataque
    public void AttackEnable()
    {
        _areaAtk.enabled = false; // Desabilita a �rea de ataque
    }

    // M�todo chamado a cada quadro fixo
    void FixedUpdate()
    {
        // Move o jogador de acordo com a dire��o e velocidade
        _playerRigidbody2D.MovePosition(_playerRigidbody2D.position + _playerDirection.normalized * _playerSpeed * Time.fixedDeltaTime);

        // Se o jogador estiver atacando, atualiza o ataque
        if (_isAttack)
        {
            if (Input.GetMouseButton(0))
            {
                UpdateAttackOffset();
            }
            else
            {
                EndAttack();
            }
        }
    }

    // M�todo para atualizar a barra de vida
    void UpdateHealthBar()
    {
        if (healtBarObject != null)
        {
            healtBarScale.x = healtPercent * health;
            healthBar.localScale = healtBarScale;
        }
    }

    // M�todo para controlar a a��o de correr do jogador
    void PlayerRun()
    {
        // Se o jogador estiver atacando, a velocidade � 0
        if (_isAttack)
        {
            _playerSpeed = 0;
        }
        else
        {
            // Se a tecla de corrida for pressionada, aumenta a velocidade
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _playerSpeed = _playerRunSpeed;
            }

            // Se a tecla de corrida for solta, volta � velocidade normal
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                _playerSpeed = _playerInitialSpeed;
            }
        }
    }

    #region Player attack 
    // M�todo para atualizar o deslocamento do ataque
    void UpdateAttackOffset()
    {
        // Define o offset com base na dire��o do jogador
        if (_playerDirection.y > 0)
        {
            _areaAtk.offset = new Vector2(0f, 1f);
        }
        else if (_playerDirection.y < 0)
        {
            _areaAtk.offset = new Vector2(0f, 0f);
        }
        else
        {
            if (_playerDirection.x > 0)
            {
                _areaAtk.offset = new Vector2(0.5f, 0.5f);
            }
            else if (_playerDirection.x < 0)
            {
                _areaAtk.offset = new Vector2(-0.5f, 0.5f);
            }
        }
    }

    // M�todo para encerrar o ataque
    void EndAttack()
    {
        _isAttack = false; // Define que o jogador n�o est� mais atacando
        _playerSpeed = _playerInitialSpeed; // Restaura a velocidade do jogador
        // Redefine o offset para o centro quando o ataque termina
        _areaAtk.offset = Vector2.zero;
        AttackEnable(); // Habilita o ataque novamente
    }

    // M�todo para iniciar o ataque
    void OnAttack()
    {
        _isAttack = true; // Define que o jogador est� atacando
        _playerSpeed = 0; // Define a velocidade como 0 durante o ataque

        // Se houver uma dire��o definida, atualiza o deslocamento do ataque
        if (_playerDirection != Vector2.zero)
        {
            UpdateAttackOffset();
        }
    }

    // M�todo para receber dano
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount; // Reduz a vida do jogador
        UpdateHealthBar(); // Atualiza a barra de vida
    }

    // M�todo para verificar se o jogador est� atacando
    public bool IsAttacking()
    {
        return _isAttack;
    }
    #endregion



    
}
