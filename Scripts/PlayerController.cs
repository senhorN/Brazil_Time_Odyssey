using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _playerRigidBody2D;
    public float        _playerSpeed;
    private Vector2     _playerDirection; //o Vector2 é os eixos X e Y

    // Start is called before the first frame update
    void Start()
    {
        _playerRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void FixedUpdate()
    {
        _playerRigidBody2D.MovePosition(_playerRigidBody2D.position + _playerDirection * _playerSpeed * Time.fixedDeltaTime);
    }
}
