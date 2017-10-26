/* Enric Llagostera <http://enric.llagostera.com.br> */

using UnityEngine;
using UnityEngine.UI;
using ArcadePUCCampinas;

public class TestesInput : MonoBehaviour
{
    public int indiceJogador;
    private Text txt;

    void Start()
    {
        txt = GetComponent<Text>();
    }

    void Update()
    {
        txt.text = 
            "Jogador " + indiceJogador + "\n\n" +
            InputArcade.Apertado(indiceJogador, EControle.CIMA) + " - " +
            InputArcade.Apertado(indiceJogador, EControle.DIREITA) + " - " +
            InputArcade.Apertado(indiceJogador, EControle.BAIXO) + " - " +
            InputArcade.Apertado(indiceJogador, EControle.ESQUERDA) + " - " +
            InputArcade.Apertado(indiceJogador, EControle.VERDE) + " - " +
            InputArcade.Apertado(indiceJogador, EControle.VERMELHO) + " - " +
            InputArcade.Apertado(indiceJogador, EControle.PRETO) + " - " +
            InputArcade.Apertado(indiceJogador, EControle.AZUL) + " - " +
            InputArcade.Apertado(indiceJogador, EControle.AMARELO) + " - " +
            InputArcade.Apertado(indiceJogador, EControle.BRANCO) + ";";
    }

}
