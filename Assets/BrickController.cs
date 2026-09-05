using UnityEngine;

public class BrickController : MonoBehaviour
{
    public int pontos = 10; // Quantos pontos o bloco vale

    void OnCollisionEnter2D(Collision2D coll)
    {
        // Verifica se quem bateu foi a bola
        if (coll.gameObject.CompareTag("Ball") || coll.gameObject.GetComponent<BallController>() != null)
        {
            // Acha o GameManager e soma os pontos
            GameManager gerenciador = FindFirstObjectByType<GameManager>();
            if (gerenciador != null)
            {
                gerenciador.AdicionarPontos(pontos);
            }

            // Destrói apenas este bloco
            Destroy(gameObject);
        }
    }
}