using UnityEngine;
using UnityEngine.Rendering.Universal;
// ��� PixelPerfectCamera

public class PixelPerfectZoomController : MonoBehaviour
{
    public PixelPerfectCamera pixelPerfectCamera;

    public int baseAssetsPPU = 60;

    public int zoomedInPPU = 40; // ������� / 2

    public int standardPPU = 80; // �������


    void Awake()
    {
        if (pixelPerfectCamera == null)
        {
            //Camera mainCam = Camera.current;
            pixelPerfectCamera = GetComponent<PixelPerfectCamera>();
        }

        if (pixelPerfectCamera == null)
        {
            Debug.LogError("Pixel Perfect Camera �� ������� ��� �� ���������!", this);
            enabled = false;
            return;
        }

        pixelPerfectCamera.assetsPPU = standardPPU;
        Debug.Log($"��������� PPU ���������� �: {pixelPerfectCamera.assetsPPU}");
    }

    // ��������� �����, ������� ����� �������� �� ��������� ������
    public void SetZoomLevelPPU(int targetPPU)
    {
        if (pixelPerfectCamera != null)
        {
            if (pixelPerfectCamera.assetsPPU != targetPPU)
            {
                pixelPerfectCamera.assetsPPU = targetPPU;
                Debug.Log($"Pixel Perfect Camera PPU ������� ��: {targetPPU}");
            }
        }
        else
        {
            Debug.LogWarning("������� �������� PPU, �� Pixel Perfect Camera �� �������.", this);
        }
    }

    public void SetBaseZoomLevelPPU()
    {
        SetZoomLevelPPU(baseAssetsPPU);
    }

    // ----- ������ ������ (�� ������� �������, ��������, �������� �������) -----
    /*
    public class RoomTrigger : MonoBehaviour
    {
        public int ppuForThisRoom = 150; // PPU ��� ���� �������
        public PixelPerfectZoomController zoomController; // ������ �� ���������� ����

        void Start() {
            // ������� ����������, ���� �� ��������
            if (zoomController == null) zoomController = FindObjectOfType<PixelPerfectZoomController>();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player")) // ���������, ��� ����� �����
            {
                if(zoomController != null)
                {
                    zoomController.SetZoomLevelPPU(ppuForThisRoom);
                }
            }
        }

         // �����������: ������� ��� ��� ������
         // void OnTriggerExit2D(Collider2D other) {
         //     if (other.CompareTag("Player")) {
         //          if(zoomController != null) {
         //               zoomController.SetZoomLevelPPU(zoomController.standardPPU); // ������� �����������
         //          }
         //     }
         // }
    }
    */

}