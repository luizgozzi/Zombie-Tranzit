using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum PickupType { Coin, Gasoline, Nitro }
    public PickupType type;

    public int coinValue = 1;         // Valor da moeda
    public float nitroBoost = 2f;     // Boost de velocidade (ajuste conforme quiser)
    public float gasolineAmount = 10f; // Exemplo para gasolina (energia/combustível)

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (type)
            {
                case PickupType.Coin:
                    CoinManager.Instance.AddCoins(coinValue);
                    break;

                case PickupType.Gasoline:
                    // Aqui você chama seu sistema de gasolina/combustível
                    Debug.Log("Gasolina coletada: +" + gasolineAmount);
                    // Exemplo -> other.GetComponent<PlayerFuel>().AddFuel(gasolineAmount);
                    break;

                case PickupType.Nitro:
                    // Aqui você chama seu sistema de nitro/boost
                    Debug.Log("Nitro coletado: +" + nitroBoost);
                    // Exemplo -> other.GetComponent<PlayerBoost>().ActivateBoost(nitroBoost);
                    break;
            }

            Destroy(gameObject);
        }
    }
}