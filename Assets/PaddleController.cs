using UnityEngine;
using UnityEngine.InputSystem; // Importa o novo sistema de input do Unity

public class PaddleController : MonoBehaviour
{
    public float velocidade = 10f;
    public float limiteX = 7.5f;

    void Update()
    {
        float direcao = 0f;

        // Verifica o teclado usando o Novo Input System (Keyboard atual)
        if (Keyboard.current != null)
        {
            if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
            {
                direcao = -1f;
            }
            if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
            {
                direcao = 1f;
            }
        }

        // Movimenta a nave
        transform.Translate(Vector2.right * direcao * velocidade * Time.deltaTime);

        // Trava para a nave não sair da tela
        float posicaoX = Mathf.Clamp(transform.position.x, -limiteX, limiteX);
        transform.position = new Vector3(posicaoX, transform.position.y, transform.position.z);
    }
}