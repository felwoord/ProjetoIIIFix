using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ArcadePUCCampinas
{
    public class ArcadeJogo : Singleton<ArcadeJogo>
    {
        private bool _noMenu;
        private bool _trocaControles = false;
        private Transform _menu;

        void Awake()
        {
            InputArcade.Inicializar();
            Screen.fullScreen = true;
            Cursor.visible = false;
            Application.runInBackground = true;
            _noMenu = false;
            _menu = transform.Find("Menu");
           // _menu.gameObject.SetActive(false);
        }

        void Update()
        {
            if (!_noMenu)
            {
                if (InputArcade.Apertou(0, EControle.MENU))
                {
                    MostrarMenu();
                }    
            }
            else
            {
                // se pressionar de novo, sai do jogo
                if (InputArcade.Apertou(0, EControle.VERDE) || InputArcade.Apertou(1, EControle.VERDE))
                {
                    print("saiu");
                    Application.Quit();
                    return;
                }

                if (InputArcade.Apertou(0, EControle.VERMELHO) || InputArcade.Apertou(1, EControle.VERMELHO))
                {
                    print("voltou ao jogo");
                    SairMenu();
                }

                if (InputArcade.Apertou(0, EControle.AMARELO) || InputArcade.Apertou(1, EControle.AMARELO))
                {
                    if(!_trocaControles) {
                        print("trocar controles");
                        GetComponent<AudioSource>().Play();
                        StartCoroutine(AlterarControles());
                    }
                }
            }

        }

        void LateUpdate()
        {
            InputArcade.Atualizar();
        }

        void MostrarMenu()
        {
            // mostra menu e pausa o tempo do jogo
            _noMenu = true;
            _menu.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        void SairMenu() {
            // esconde o menu e religa o tempo
            Time.timeScale = 1;
            _noMenu = false;
            _menu.gameObject.SetActive(false);
        }

        IEnumerator AlterarControles() {
            _trocaControles = true;
            InputArcade.TrocarControles();
            yield return new WaitForSeconds(1f);
            _trocaControles = false;
        }
    }
}