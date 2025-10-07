using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerControllerX2 : MonoBehaviour
{
    public GameObject dogPrefab;
    float dogCooldown = 2.0f;

    // Update is called once per frame
    void Update()
    {
        dogCooldown -= Time.deltaTime;
        math.clamp(dogCooldown, 0, 2);
        
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space) && CanSpawnDog())
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
            dogCooldown = 2f;
        }
    }

    bool CanSpawnDog()
    {
        if (dogCooldown > 0) { return false; }
        else { return true; }
    }
}
