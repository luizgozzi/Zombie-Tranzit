using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nitro : MonoBehaviour
{
    [SerializeField] float turnSpeed = 90f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Obstacle>() != null)
        {
            Destroy(gameObject);
            return;
        }

        // Verifica se colidiu com o player
        if (other.gameObject.name == "Player")
        {
            // Ativa o boost de velocidade no player
            other.gameObject.GetComponent<PlayerMovement>().ActivateBoost();

            // Destrói o nitro
            Destroy(gameObject);
            return;
        }

        // Check that the object we collided with is the pleyer
        if (other.gameObject.name != "Player")
        {
            return;
        }

        // Add to player's speed

        // Destroy this Nitro object
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
    }


}
