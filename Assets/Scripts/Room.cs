using Unity.Cinemachine;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] 
    private CinemachineCamera virtualCam;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            virtualCam.Follow = player.transform;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            virtualCam.Priority += 1;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            virtualCam.Priority -= 1;

        }
    }
}
