using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class JumpScare : MonoBehaviour
{
    public GameObject telaperdeu; // Objeto para ativar
    public GameObject jumpsscaretela; // Objeto para desativar
    public GameObject lobisomen; // Objeto para desativar
    public GameObject telajogo;



    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "guarda")
        {
            // Ativa o objeto quando ocorre uma colisão
           
            telaperdeu.SetActive(true);
            lobisomen.SetActive(false);
             telajogo.SetActive(false);
            // Invoca o método DeactivateObject após 2 segundos
            Invoke("DeactivateObject", 2f);
             Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void DeactivateObject()
    {
        // Desativa o objeto após 2 segundos
        jumpsscaretela.SetActive(false);
         lobisomen.SetActive(true);

    }

}
