#if UNITY_IOS || UNITY_ANDROID
    #define UNITY_MOBILE
#endif


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    private Vector3 movement;

    private Animator _animator;
    [SerializeField] private float turnSpeed = 20f;

    private Rigidbody _rigidbody;
    private AudioSource _audioSource;
    private Quaternion _rotation = Quaternion.identity;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }
    
    void FixedUpdate()
    {
        
    #if UNITY_MOBILE
        float horizontalInput = Input.GetAxis("Mouse X");
        float verticalInput = Input.GetAxis("Mouse Y");
        if (Input.touchCount > 0)
        {
            horizontalInput = Input.touches[0].deltaPosition.x;
            verticalInput = Input.touches[0].deltaPosition.y;
        }
    #else
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
    #endif
        
        movement.Set(horizontalInput, 0 , verticalInput);
        movement.Normalize();

        // Mathf.Approximately: Compara los parámetros y si uno se aproxima al otro, lo anula. Si horizontalInput se aproxima a 0, dará 0
        // Con esto conseguimos que si por sensibilidad de las teclas, hubiera un valor que no sea 0 puro, por pequeñas vibraciones, lo cambia a 0
        bool hasHorizontalInput = !Mathf.Approximately(horizontalInput, 0f);
        bool hasVerticalInput = !Mathf.Approximately(verticalInput, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        _animator.SetBool("isWalking", isWalking);

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movement, turnSpeed * Time.fixedDeltaTime, 0f);
        _rotation = Quaternion.LookRotation(desiredForward);

        if (isWalking)
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }
        else
        {
            _audioSource.Stop();//Si pausas el audio, se reproducirá desde donde lo pausó. 
                                //Si lo paras, empezará desde el principio.
        }
    }

    private void OnAnimatorMove()
    {
        _rigidbody.MovePosition(_rigidbody.position + movement * _animator.deltaPosition.magnitude);
        _rigidbody.MoveRotation(_rotation);
        
    }
}
