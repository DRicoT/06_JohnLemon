using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float imageDuration = 1f;
    private bool isPlayerAtExit;
    private float timer;

    [SerializeField] private GameObject player;
    [SerializeField] private CanvasGroup exitCanvasGroup;

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerAtExit = true;
        }
    }

    private void Update()
    {
        if (isPlayerAtExit)
        {
            timer += Time.deltaTime;
            exitCanvasGroup.alpha = Mathf.Clamp(timer / fadeDuration, 0,1);
            
            if (timer > fadeDuration + imageDuration)
            {
                EndLevel();
            }
        }
    }

    void EndLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // Application.Quit(); // Para Salir de la aplicación
    }
}
