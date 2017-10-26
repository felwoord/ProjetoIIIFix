using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArcadePUCCampinas;

public class Nave : MonoBehaviour
{
    public int jogador = 0;
    Vector2 input;
    Rigidbody2D rb;
    public float forcaFogueteX;
    public float forcaFogueteY;
    public Vector2 velocidade;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();		
    }

    void Update()
    {
        input = new Vector2();
        input.x = InputArcade.Eixo(jogador, EEixo.HORIZONTAL);
        input.y = InputArcade.Eixo(jogador, EEixo.VERTICAL);
        Debug.Log(input);
    }

    /// <summary>
    /// Versão da função sem aplicar curvas de ataque e release, apenas usando o sistema de InputManager da Unity pra lidar com a resposta dos eixos.
    /// </summary>
    void FixedUpdate()
    {
        // seta pra esquerda
        if (input.x < 0f)
        {
            // ligar o foguete esquerdo
            rb.AddRelativeForce(Vector2.right * forcaFogueteX);
        }
        else if (input.x > 0f)
        {
            // ligar o foguete direito
            rb.AddRelativeForce(Vector2.left * forcaFogueteX);
        }
        // se apertar para baixo
        if (input.y < 0f)
        {
            // ligar o foguete de baixo
            rb.AddRelativeForce(Vector2.up * forcaFogueteY);
        }
        velocidade = rb.velocity;
    }
}
