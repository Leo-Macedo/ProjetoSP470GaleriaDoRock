using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movimento : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    public float correrSpeed = 10f;  // A velocidade adicional quando o guarda corre
    public float veloSpeed = 2f;  // Uma velocidade adicional

    public float jumpForce = 5f; // A força do pulo

    private Rigidbody rb; // Referência ao Rigidbody

    public float energia = 100f;  // A energia inicial do guarda
    public Slider sliderEnergia;  // O slider que mostra a energia do guarda
    private bool estaCorrendo = false;

    void Start()
    {
        // Obtém a referência ao Rigidbody
        rb = GetComponent<Rigidbody>();

        sliderEnergia.maxValue = energia;
        sliderEnergia.value = energia;
        // Começa a regenerar energia após 5 segundos
        InvokeRepeating("RegenerarEnergia", 1f, 1f);
    }

    void Update()
    {
        // Rotação com o mouse
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up, mouseX * rotationSpeed * Time.deltaTime);
        Camera.main.transform.Rotate(Vector3.right, mouseY * rotationSpeed * Time.deltaTime);

        // Movimentação com WASD
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        movement = Camera.main.transform.rotation * movement;

        if (Input.GetKey(KeyCode.LeftShift) && energia > 0)
        {
            estaCorrendo = true;
            Correr();
        }
        else
        {
            estaCorrendo = false;
        }

        if (estaCorrendo)
        {
            // Se o guarda está correndo, adiciona a velocidade de corrida e a velocidade adicional
            transform.Translate(movement * (speed + correrSpeed + veloSpeed) * Time.deltaTime, Space.World);
        }
        else
        {
            // Se o guarda não está correndo, usa a velocidade normal
            transform.Translate(movement * speed * Time.deltaTime, Space.World);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 1.5f))
            {
                // Aplica uma força de pulo
                rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            }
        }
    }

    void Correr()
    {
        if (energia > 0)  // O guarda só pode correr se a energia for maior que 0
        {
            energia -= 0.7f;  // A energia diminui em 10 unidades quando o guarda corre
            sliderEnergia.value = energia;  // Atualiza o valor do slider
        }
    }

    void RegenerarEnergia()
    {
        if (!estaCorrendo && energia < 200)  // Se o guarda não está correndo e a energia é menor que 100
        {
            energia += 50;  // A energia aumenta em 10 unidades
            sliderEnergia.value = energia;  // Atualiza o valor do slider
        }
    }
}