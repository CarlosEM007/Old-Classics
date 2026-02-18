using Assets.Assets.Shared;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OldClassics.Menu
{
    public class MenuScript : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private GameObject SetaCima;
        [SerializeField] private GameObject SetaBaixo;
        [SerializeField] private TextMeshProUGUI Text;
        [SerializeField] private ParticleSystem Faiscas;

        [Header("Jogos")]
        [SerializeField] private List<Game> Games;

        private int Indice = 0;

        private void Start()
        {
            SetaCima.SetActive(false);
            TrocarNome(Games[Indice].Name);
        }

        void Update()
        {
            TrocarJogo();
            AbrirJogo();
            ControladorDeSetas();
        }

        private void TrocarJogo()
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (Indice < Games.Count - 1)
                {
                    Indice++;
                    TrocarNome(Games[Indice].Name);
                }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (Indice > 0)
                {
                    Indice--;
                    TrocarNome(Games[Indice].Name);
                }
            }
        }

        private void ControladorDeSetas()
        {
            if (Indice == Games.Count - 1)
            {
                SetaBaixo.SetActive(false);
                SetaCima.SetActive(true);
            }
            else if (Indice == 0)
            {
                SetaBaixo.SetActive(true);
                SetaCima.SetActive(false);
            }
            else
            {
                SetaBaixo.SetActive(true);
                SetaCima.SetActive(true);
            }
        }

        private void TrocarNome(string NomeJogo)
        {
            Text.text = NomeJogo;
        }

        private void AbrirJogo()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                StartCoroutine(TrocarCena());
            }
        }

        private IEnumerator TrocarCena()
        {
            Faiscas.Play();

            yield return new WaitForSeconds(2);

            SceneManager.LoadScene(Games[Indice].SceneID);
        }
    }
}