
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    bool alive = true;
    // speed
    public float defaultSpeed = 10f;
    private float currentSpeed;
    // boost
    public float boostAmount = 3.5f;
    public float boostDuration = 5f;
    // fuel
    public float maxFuel = 100f;
    public float currentFuel;
    public float fuelConsumptionRate = 5f;

    [SerializeField] Rigidbody rb;

    float horizontalInput;
    [SerializeField] float horizontalMultiplier = 2;
    float time;

    [SerializeField] Game_Manager gameManager;

    void Start()
    {
        currentSpeed = defaultSpeed;
        currentFuel = maxFuel;
    }

    void FixedUpdate()
    {
        if (!alive)
            return;

        // movimento para frente
        float forwardSpeed = currentSpeed;

        // movimento lateral
        float horizontalSpeed = horizontalInput * horizontalMultiplier;

        // aplica no rigidbody
        rb.linearVelocity = new Vector3(horizontalSpeed, rb.linearVelocity.y, forwardSpeed);

        transform.rotation = Quaternion.identity; // trava a rota��o
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (transform.position.y < -5)
        {
            Die();
        }

        if (alive)
        {
            ConsumeFuel();
        }
    }


    public void Die()
    {
        alive = false;
        // Restart the game
        Invoke("Restart", 2);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ActivateBoost()
    {
        StopAllCoroutines(); // para evitar boosts empilhados   
        StartCoroutine(BoostRoutine());
    }

    private IEnumerator BoostRoutine()
    {
        Debug.Log("Boost iniciado");
        currentSpeed = defaultSpeed + boostAmount;
        Debug.Log("Velocidade aumentada para: " + currentSpeed);
        yield return new WaitForSeconds(boostDuration);
        currentSpeed = defaultSpeed;
        Debug.Log("Boost terminou, velocidade normal: " + currentSpeed);
    }

    public void AddFuel(float amount)
    {
        currentFuel += amount;
        if (currentFuel > maxFuel)
            currentFuel = maxFuel;
    }

    void ConsumeFuel()
    {
        currentFuel -= fuelConsumptionRate * Time.deltaTime;
        currentFuel = Mathf.Max(currentFuel, 0);

        if (gameManager != null)
            gameManager.UpdateFuelBar(currentFuel, maxFuel);

        if (currentFuel == 0)
        {
            Die();  // Ou outra a��o que preferir quando acabar combust�vel
        }
    }
}
