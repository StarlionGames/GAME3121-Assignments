using UnityEngine;

public class Propeller : MonoBehaviour
{
    
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1) * 500 * Time.deltaTime);
    }
}
