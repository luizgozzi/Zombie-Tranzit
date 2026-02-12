using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DistanceManager : MonoBehaviour
{
    public Transform player;  // referência ao player
    public TextMeshProUGUI distanceText; // texto UI para mostrar a distância (opcional)

    private float startZ; // posição inicial
    private float distance;

    void Start()
    {
        startZ = player.position.z; // salva a posição inicial do player
    }

    void Update()
    {
        // calcula distância percorrida no eixo Z
        distance = player.position.z - startZ;

        // exibe na UI (se quiser)
        if (distanceText != null)
        {
            distanceText.text = Mathf.Floor(distance).ToString() + " m";
        }
    }

    public float GetDistance()
    {
        return distance;
    }

    public float baseSpeed = 10f;       // velocidade inicial dos zumbis
    public float speedIncreaseRate = 0.1f; // quanto a velocidade aumenta a cada 100 metros

    public float GetZombieSpeed()
    {
        float km = distance / 100f; // supondo que "distance" seja em metros
        return baseSpeed + (km * speedIncreaseRate);
    }
}