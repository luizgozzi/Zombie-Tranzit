using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalManager : MonoBehaviour
{
    [SerializeField] private string nomeDoLevelDeJogo;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelLoja;

    public void Jogar() 
    {
        SceneManager.LoadScene(nomeDoLevelDeJogo);
    }

    public void AbrirLoja() 
    {
        painelMenuInicial.SetActive(false);
        painelLoja.SetActive(true);
    }

    public void FecharLoja()
    {
        painelLoja.SetActive(false);
        painelMenuInicial.SetActive(true);


    }

    public void SairJogo() 
    {
        Debug.Log("Sair do Jogo");
        Application.Quit();
    }
}
