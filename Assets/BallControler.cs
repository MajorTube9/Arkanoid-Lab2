using UnityEngine;

public class BallController : MonoBehaviour
{
    public float velocidade = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        // Joga a bola direto para cima/direita ao dar o Play
        rb.linearVelocity = new Vector2(2f, 5f).normalized * velocidade;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Brick")
        {
            Destroy(coll.gameObject);
        }
    }
}