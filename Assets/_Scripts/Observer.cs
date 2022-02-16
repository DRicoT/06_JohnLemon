using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

[RequireComponent(typeof(CapsuleCollider))]

public class Observer : MonoBehaviour
{
    [SerializeField] private Transform player;
    private bool isPlayerInRange = false;
    public GameEnding gameEnding;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            isPlayerInRange = false;
        }
    }

    private void Update()
    {
        if (isPlayerInRange)
        {
            /*La dirección se establece restando la posición del player,
              menos la posición de los ojos de la gárgola.
              El vector AB se calcula restando el vector B menos el A.
              Como nuestro player tiene el centro en el 000, se le suma el vector
              Vector3.up para que suba un metro su posición y se coloque en el pecho*/
            Vector3 direction = player.position - transform.position + Vector3.up;
            
            /*Se define el rayo. Necesita posición desde la que se inicia el rayo,
             y la dirección, que la hemos definido anteriormente*/
            Ray ray = new Ray(transform.position, direction);
            
            /*El comando para pintar el rayo como gizmo, con parámentros (inicio, dirección, color, duración, boolena
             para que si hay un objeto entre la cámara y el rayo, éste se difumina*/ 
            Debug.DrawRay(transform.position, direction, Color.magenta, Time.deltaTime, true);
            
            /*Ahora definimos una variable que va a recoger el choque del rayo contra algo, y
             podremos preguntarle contra qué ha chocado*/

            RaycastHit raycastHit; //Declarar variable RaycastHit
            
            if (Physics.Raycast(ray, out raycastHit)) //Physics.Raycast() es una función que devuelve una booleana,
                                      //true si algo está por en medio, false si no hay nada que lo obstaculice.
                                      //Y el out devuelve el choque a una variable previamente declarada.
            {
                if (raycastHit.collider.transform == player)
                {
                    gameEnding.CatchPlayer();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.1f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, player.position);
    }
}
