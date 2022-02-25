using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float imageDuration = 1f;
    private bool isPlayerAtExit, isPlayerCaught;
    private float timer;

    [SerializeField] private GameObject player;
    [SerializeField] private CanvasGroup exitCanvasGroup;
    [SerializeField] private CanvasGroup caughtCanvasGroup;
    
    [SerializeField] private AudioSource caughtAudio, winAudio;
    private bool hasAudioPlayed;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerAtExit = true;
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        
        if (isPlayerAtExit)
        {
            EndLevel(exitCanvasGroup, false, winAudio);
        }
        else if (isPlayerCaught)
        {
            EndLevel(caughtCanvasGroup, true, caughtAudio);
        }
    }

    /// <summary>
    /// Lanza la imagen de fin de la partida
    /// </summary>
    /// <param name="imageCanvasGroup">Imagen de fin de partida correspondiente</param>
    void EndLevel(CanvasGroup imageCanvasGroup, Boolean doRestart, AudioSource audioSource)
    {
        if (!hasAudioPlayed)
        {
            audioSource.Play();
            hasAudioPlayed = true;
        }
        timer += Time.deltaTime;
        imageCanvasGroup.alpha = Mathf.Clamp(timer / fadeDuration, 0,1);
            
        if (timer > fadeDuration + imageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                Application.Quit(); // Para Salir de la aplicación
            }
        }
    }

    public void CatchPlayer()
    {
        isPlayerCaught = true;
    }
}
