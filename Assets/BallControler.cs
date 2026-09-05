using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class BallController : MonoBehaviour
{
    public Transform paddle;
    public float velocidade = 5f;

    private bool emJogo = false;
    private Rigidbody2D rb;
    private Vector3 offset;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.linearVelocity = Vector2.zero;

        if (paddle == null)
        {
            GameObject pObj = GameObject.Find("Paddle");
            if (pObj == null) pObj = GameObject.Find("paddleRed");
            if (pObj != null) paddle = pObj.transform;
        }

        // Define uma altura padrão fixa segura logo acima do paddle
        if (paddle != null)
        {
            offset = new Vector3(0f, 0.6f, 0f);
        }
    }

    void Update()
    {
        if (!emJogo && paddle != null)
        {
            transform.position = paddle.position + offset;

            if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                LancarBola();
            }
        }
    }

    void LancarBola()
    {
        emJogo = true;
        rb.linearVelocity = new Vector2(2f, 5f).normalized * velocidade;
    }

    // Usado pelas bolas clonadas pelo bloco amarelo para já nascerem correndo instantaneamente
    public void ForcarMovimento(Vector2 direcaoInicial)
    {
        emJogo = true;
        rb.gravityScale = 0;
        rb.linearVelocity = direcaoInicial * velocidade;
    }

    void FixedUpdate()
    {
        if (emJogo)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * velocidade;
        }
    }

    public void ResetarBolaNoPaddle()
    {
        emJogo = false;
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0;

        if (paddle != null)
        {
            // Reseta exatamente na altura certa acima do paddle
            transform.position = paddle.position + new Vector3(0f, 0.6f, 0f);
        }
    }

    // Função de Turbo ativada pelo Bloco Amarelo (caso queira usar)
    public void AtivarTurbo()
    {
        StartCoroutine(RotinaTurbo());
    }

    IEnumerator RotinaTurbo()
    {
        float velocidadeNormal = velocidade;
        velocidade *= 1.5f; // Aumenta 50% da velocidade temporariamente

        yield return new WaitForSeconds(5f); // Dura 5 segundos

        velocidade = velocidadeNormal;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Brick")
        {
            // A destruição e os pontos são gerenciados pelo BrickController
        }
    }

    // Sensor de vida com Debug integrado para rastrear o erro no Console
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("A bola encostou em: " + collision.gameObject.name);

        if (collision.gameObject.name == "Sensor" || collision.gameObject.CompareTag("Sensor"))
        {
            Debug.Log("Sensor detectado com sucesso pelo código!");
            GameManager gerenciador = FindFirstObjectByType<GameManager>();
            if (gerenciador != null)
            {
                gerenciador.PerderVida();
            }
        }
    }
}