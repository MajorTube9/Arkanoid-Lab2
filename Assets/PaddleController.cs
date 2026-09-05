using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PaddleController : MonoBehaviour
{
    public float velocidade = 10f;
    public float limiteX = 7.5f;

    private Vector3 escalaOriginal;

    void Start()
    {
        escalaOriginal = transform.localScale;
    }

    void Update()
    {
        float direcao = 0f;

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

        transform.Translate(Vector2.right * direcao * velocidade * Time.deltaTime);

        float posicaoX = Mathf.Clamp(transform.position.x, -limiteX, limiteX);
        transform.position = new Vector3(posicaoX, transform.position.y, transform.position.z);
    }

    public void AumentarPaddle()
    {
        StopAllCoroutines();
        StartCoroutine(EfeitoAumentarPaddle());
    }

    IEnumerator EfeitoAumentarPaddle()
    {
        // Altere o multiplicador (ex: 1.8f) se quiser que fique maior ou menor
        transform.localScale = new Vector3(escalaOriginal.x * 1.8f, escalaOriginal.y, escalaOriginal.z);

        // Altere o tempo (ex: 8f para 8 segundos) para o poder durar mais ou menos
        yield return new WaitForSeconds(8f);

        transform.localScale = escalaOriginal;
    }
}