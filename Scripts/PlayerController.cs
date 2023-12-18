using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region player variable
        private Rigidbody2D _playerRigidBody2D;
        public float        _playerSpeed;
        private Vector2     _playerDirection; //o Vector2 é os eixos X e Y
    #endregion

    #region Animator
        private Animator    _playerAnimator;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _playerRigidBody2D = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Magnitude -> calcula distancia ou proximidade
        if (_playerDirection.sqrMagnitude > 0)
        {
            _playerAnimator.SetInteger("Movimento", 1);
        }
        else {
            _playerAnimator.SetInteger("Movimento", 1);
        }
        Flip();
    }

    void FixedUpdate()
    {
        _playerRigidBody2D.MovePosition(_playerRigidBody2D.position + _playerDirection * _playerSpeed * Time.fixedDeltaTime);
    }

    void Flip ()
    {
        if (_playerDirection.x > 0)
        {
            transform.eulerAngles = new Vector2(0f, 0f);
        
        }else if(_playerDirection.x < 0)
        {
            transform.eulerAngles = new Vector2(0f, 180f);
        }

    }

}
