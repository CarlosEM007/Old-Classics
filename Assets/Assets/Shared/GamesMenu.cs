using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Assets.Shared
{
    public class GamesMenu : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private List<TextMeshProUGUI> Textos;
        [SerializeField] private List<string> TextosAba1;
        [SerializeField] private List<string> TextosAba2;
        [SerializeField] private Color Selecionado;
        [SerializeField] private Color Padrao;

        [SerializeField] protected int IDScene;

        protected int Indice;
        protected int AbaMenu;

        void Start()
        {
            AbaMenu = 0;
            Indice = 0;
            AlterarCorTextos();
            AlterarMenu();
        }

        void Update()
        {
            SelecionarModo();
            SelecionarCampo();
        }

        protected virtual void SelecionarModo()
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if(AbaMenu == 0)
                {
                    if (Indice == TextosAba1.Count - 1) return;
                }
                else
                {
                    if (Indice == TextosAba2.Count - 1) return;
                }

                Indice += 1;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (Indice == 0) return;

                Indice -= 1;
            }

            AlterarCorTextos();
        }

        protected virtual void AlterarMenu()
        {
            if (AbaMenu == 0)
            {
                AlterarTextosMenu(TextosAba1);
            }
            else
            {
                AlterarTextosMenu(TextosAba2);
            }

            Indice = 0;
            AlterarCorTextos();
        }

        protected virtual void SelecionarCampo()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                AcionarCampo();
            }
        }

        protected virtual void AcionarCampo()
        {
            if (AbaMenu == 0)
            {
                if (Indice == TextosAba1.Count - 1)
                {
                    AlterarCena("", 0);
                }
                else if (Indice == 0)
                {
                    AbaMenu = 1;

                    AlterarMenu();
                }
                else
                {
                    AlterarCena("-1", IDScene);
                }
            }
            else
            {
                if (Textos[Indice].text == "Voltar")
                {
                    AbaMenu = 0;

                    AlterarMenu();
                }
                else
                {
                    AlterarCena(Indice.ToString(), IDScene);
                }
            }
        }

        protected virtual void AlterarCorTextos()
        {
            for (int i = 0; i <= Textos.Count - 1; i++)
            {
                if (i == Indice)
                {
                    Textos[i].color = Selecionado;
                }
                else
                {
                    Textos[i].color = Padrao;
                }
            }
        }

        protected void AlterarTextosMenu(List<string> Valores)
        {
            for(int i = 0; i <= Textos.Count - 1; i++)
            {
                if(i > Valores.Count - 1)
                {
                    Textos[i].text = "";
                    continue;
                }

                string valor = Valores[i] ?? "";
                Textos[i].text = valor;
            }
        }

        protected void AlterarCena(string Pametro, int IdScene)
        {
            GameParameters.GameParameter = Pametro;
            SceneManager.LoadScene(IdScene);
        }
    }
}
