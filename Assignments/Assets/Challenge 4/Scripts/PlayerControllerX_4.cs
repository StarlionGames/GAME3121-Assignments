using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerX_4 : MonoBehaviour
{
    [SerializeField] InputActionAsset _actionAsset;
    InputAction _move => _actionAsset.FindAction("Move");

    private Rigidbody playerRb;
    private float speed = 500;
    private GameObject focalPoint;

    public bool hasPowerup;
    public GameObject powerupIndicator;
    public int powerUpDuration = 5;

    private float normalStrength = 10; // how hard to hit enemy without powerup
    private float powerupStrength = 25; // how hard to hit enemy with powerup

    float verticalInput;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
        // Set powerup indicator position to beneath player
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);

        // Add force to player in direction of the focal point (and camera)
        if (_move.ReadValue<float>() == 0) { return; }

        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime); 
    }

    // If Player collides with powerup, activate powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            StartCoroutine("PowerupCooldown");
        }
    }

    // Coroutine to count down powerup duration
    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    // If Player collides with enemy
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer =  transform.position - other.gameObject.transform.position; 
           
            if (hasPowerup) // if have powerup hit enemy with powerup force
            {
                enemyRigidbody.AddForce(-awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
            else // if no powerup, hit enemy with normal strength 
            {
                enemyRigidbody.AddForce(-awayFromPlayer * normalStrength, ForceMode.Impulse);
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        verticalInput = context.ReadValue<float>();
    }

    public void OnBoost(InputAction.CallbackContext context)
    {
        if (context.canceled) { speed = 500; return; }

        speed *= 2;
    }
}
