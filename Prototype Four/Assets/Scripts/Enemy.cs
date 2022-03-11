using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed = 3.0f;
    private Rigidbody enemyBody;
    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        
        enemyBody.AddForce(lookDirection * speed);

        if (transform.position.y < -30)
            Destroy(gameObject);
    }
}
