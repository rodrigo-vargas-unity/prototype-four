using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerBody;
    private float speed = 5.0f;
    private GameObject focalCameraPoint;
    private bool hasPowerup;
    private float powerupStrengh = 15.0f;
    public GameObject powerupIndicator;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        focalCameraPoint = GameObject.Find("FocalCameraPoint");
    }

    // Update is called once per frame
    void Update()
    {
        var forwardInput = Input.GetAxis("Vertical");

        playerBody.AddForce(focalCameraPoint.transform.forward * forwardInput * speed);

        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            SetPowerup(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    private void SetPowerup(bool isActive)
    {
        powerupIndicator.SetActive(isActive);
        hasPowerup = isActive;
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);

        SetPowerup(false);

        Debug.Log("Powerup is over");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyBody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 pushBackDirection = collision.gameObject.transform.position - transform.position;

            enemyBody.AddForce(pushBackDirection * powerupStrengh, ForceMode.Impulse);


            Debug.Log($"Collided with: {collision.gameObject.name} with powerup set to {hasPowerup}");
        }
    }
}
