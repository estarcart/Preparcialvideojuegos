using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New : MonoBehaviour
{
    private Animator animator; // Referencia al componente Animator
    private Rigidbody2D rb;     // Referencia al componente Rigidbody2D

    void Start()
    {
        animator = GetComponent<Animator>(); // Obtén el Animator del GameObject
        rb = GetComponent<Rigidbody2D>();    // Obtén el Rigidbody2D del GameObject
    }

    void Update()
    {
        float speed = 0.006f; // Ajusta la velocidad según sea necesario
        float jumpForce = 5f; // Ajusta la fuerza de salto según sea necesario
        bool isMoving = false;

        if (Input.GetKey("d")) // Mover a la derecha
        {
            transform.position += new Vector3(speed, 0, 0);
            isMoving = true;
        }
        if (Input.GetKey("a")) // Mover a la izquierda
        {
            transform.position += new Vector3(-speed, 0, 0);
            isMoving = true;
        }

        // Control de animación de movimiento
        if (isMoving)
        {
            animator.Play("walk");
        }
        else
        {
            animator.Play("idle");
        }

        // Control de animación de salto
        if (Input.GetKeyDown("space") && Mathf.Abs(rb.velocity.y) < 0.01f) // Saltar
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            animator.Play("jump");
        }

        // Control de animación de caída
        if (rb.velocity.y < 0) // El personaje está cayendo
        {
            animator.Play("fall");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Box") // Si el jugador colisiona con una caja
        {
            Destroy(collision.gameObject); // Destruye el objeto de la caja cuando el jugador colisiona con él
        }

        // Al tocar el suelo, resetea las animaciones de salto y caída
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.Play("idle");
        }
    }
}
