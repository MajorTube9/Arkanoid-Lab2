using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverController : MonoBehaviour
{
    public TextMeshProUGUI textoTitulo;
    public TextMeshProUGUI textoPontuacao; // Arraste o objeto "Pontos: 0" para cá no Inspector

    void Start()
    {
        // Pega a pontuação final salva
        int pontuacaoFinal = PlayerPrefs.GetInt("PontuacaoFinal", 0);

        if (textoPontuacao != null)
        {
            textoPontuacao.text = "Pontos: " + pontuacaoFinal;
        }

        // Verifica se venceu ou perdeu
        bool venceu = PlayerPrefs.GetInt("VenceuJogo", 0) == 1;

        if (textoTitulo != null)
        {
            if (venceu)
            {
                textoTitulo.text = "VOCÊ VENCEU!";
            }
            else
            {
                textoTitulo.text = "GAME OVER";
            }
        }
    }

    // Função que o botão vai chamar
    public void VoltarAoMenu()
    {
        PlayerPrefs.SetInt("VenceuJogo", 0);
        PlayerPrefs.DeleteKey("PontuacaoAtual");
        SceneManager.LoadScene("Menu"); // Certifique-se de que a cena do menu se chama "Menu" (ou mude para "Cena 1")
    }
}