using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyX : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    Transform playerGoal;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Set enemy direction towards player goal and move there
        Vector3 lookDirection = (playerGoal.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed * Time.deltaTime);

    }
    public void SetGoal(Transform goal)
    {
        playerGoal = goal;
    }
    public void SetSpeed(float speedMult)
    {
        float s = 0.5f * speedMult;
        speed *= s;
    }
    private void OnCollisionEnter(Collision other)
    {
        // If enemy collides with either goal, destroy it
        if (other.gameObject.name == "Enemy Goal")
        {
            Destroy(gameObject);
        } 
        else if (other.gameObject.name == "Player Goal")
        {
            Destroy(gameObject);
        }

    }

}
