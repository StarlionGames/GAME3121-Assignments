using UnityEngine;

public class BounceFloor : MonoBehaviour
{
    AudioSource _audio => GetComponent<AudioSource>();

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out PlayerControllerX_3 player) 
            && collision.TryGetComponent(out Rigidbody rb) )
        {
            if (player.gameOver) { return; }

            _audio.Play();
            rb.AddForce(Vector3.up * 30, ForceMode.Impulse);
        }
    }
}
