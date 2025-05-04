using Unity.Cinemachine;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] 
    private CinemachineCamera virtualCam;

    [SerializeField]
    private int ppuForThisRoom;

    private PixelPerfectZoomController ppZoom;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            virtualCam.Follow = player.transform;
        }

        ppZoom = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PixelPerfectZoomController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            virtualCam.Priority += 1;

            if (ppZoom)
            {
                ppZoom.SetZoomLevelPPU(ppuForThisRoom);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            virtualCam.Priority -= 1;

            if (ppZoom)
            {
                ppZoom.SetBaseZoomLevelPPU();
            }
        }
    }
}
