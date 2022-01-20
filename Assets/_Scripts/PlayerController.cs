using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Vector3 movement;

    private Animator _animator;
    [SerializeField] private float turnSpeed = 20f;

    private Rigidbody _rigidbody;
    private Quaternion _rotation = Quaternion.identity;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        movement.Set(horizontalInput, 0 , verticalInput);
        movement.Normalize();

        // Mathf.Approximately: Compara los parámetros y si uno se aproxima al otro, lo anula. Si horizontalInput se aproxima a 0, dará 0
        // Con esto conseguimos que si por sensibilidad de las teclas, hubiera un valor que no sea 0 puro, por pequeñas vibraciones, lo cambia a 0
        bool hasHorizontalInput = !Mathf.Approximately(horizontalInput, 0f);
        bool hasVerticalInput = !Mathf.Approximately(verticalInput, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        _animator.SetBool("isWalking", isWalking);

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movement, turnSpeed * Time.deltaTime, 0f);
        _rotation = Quaternion.LookRotation(desiredForward);
    }

    private void OnAnimatorMove()
    {
        _rigidbody.MovePosition(_rigidbody.position + movement * _animator.deltaPosition.magnitude);
        _rigidbody.MoveRotation(_rotation);
    }
}
