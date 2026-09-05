using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int vidas = 3;
    public Image[] coracoes;
    public int pontuacao = 0;

    [Header("UI do Placar")]
    public TextMeshProUGUI textoPlacar;

    private float tempoInicio = 0f;

    void Start()
    {
        // Recupera a pontuação acumulada caso venha da fase anterior
        if (PlayerPrefs.HasKey("PontuacaoAtual"))
        {
            pontuacao = PlayerPrefs.GetInt("PontuacaoAtual");
        }

        AtualizarPlacar();
        AtualizarCoracoes();
        tempoInicio = Time.time;
    }

    void Update()
    {
        // Só começa a procurar os blocos após 1.5 segundos de jogo rolando (evita falso positivo no Start)
        if (Time.time - tempoInicio > 1.5f)
        {
            VerificarFaseConcluida();
        }
    }

    public void PerderVida()
    {
        vidas--;
        AtualizarCoracoes();

        if (vidas <= 0)
        {
            // Salva o fim de jogo por derrota e limpa o temporário
            PlayerPrefs.SetInt("VenceuJogo", 0);
            PlayerPrefs.SetInt("PontuacaoFinal", pontuacao);
            PlayerPrefs.DeleteKey("PontuacaoAtual");
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            BallController bola = FindFirstObjectByType<BallController>();
            if (bola != null)
            {
                bola.ResetarBolaNoPaddle();
            }
        }
    }

    public void GanharVida()
    {
        if (vidas < 3)
        {
            vidas++;
            AtualizarCoracoes();
        }
    }

    public void AdicionarPontos(int quantidade)
    {
        pontuacao += quantidade;
        AtualizarPlacar();

        // Salva instantaneamente na memória para garantir a persistência
        PlayerPrefs.SetInt("PontuacaoAtual", pontuacao);
        PlayerPrefs.Save();
    }

    void VerificarFaseConcluida()
    {
        // Procura por todos os objetos na cena que possuem a tag "Brick"
        GameObject[] blocosRestantes = GameObject.FindGameObjectsWithTag("Brick");

        // Se não sobrou nenhum bloco na fase
        if (blocosRestantes.Length == 0)
        {
            PlayerPrefs.SetInt("PontuacaoAtual", pontuacao);
            PlayerPrefs.SetInt("PontuacaoFinal", pontuacao);
            PlayerPrefs.Save();

            string cenaAtual = SceneManager.GetActiveScene().name;

            if (cenaAtual == "Cena 2")
            {
                // Se acabou a Cena 2, o jogador venceu o jogo inteiro!
                PlayerPrefs.SetInt("VenceuJogo", 1);
                PlayerPrefs.DeleteKey("PontuacaoAtual");
                SceneManager.LoadScene("GameOver");
            }
            else
            {
                // Se for a Cena 1, avança para a Cena 2 mantendo a pontuação
                SceneManager.LoadScene("Cena 2");
            }
        }
    }

    void AtualizarCoracoes()
    {
        if (coracoes == null) return;

        for (int i = 0; i < coracoes.Length; i++)
        {
            if (coracoes[i] != null)
            {
                if (i < vidas)
                {
                    coracoes[i].gameObject.SetActive(true);
                }
                else
                {
                    coracoes[i].gameObject.SetActive(false);
                }
            }
        }
    }

    void AtualizarPlacar()
    {
        if (textoPlacar != null)
        {
            textoPlacar.text = "Pontos: " + pontuacao;
        }
    }
}