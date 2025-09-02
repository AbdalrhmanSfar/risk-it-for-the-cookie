using UnityEngine;

public class CameraAspect : MonoBehaviour
{
    // Set your desired aspect ratio here (e.g., 16:9)
    public float targetAspect = 16.0f / 9.0f;

    void Start()
    {
        AdjustCameraViewport();
    }

    void Update()
    {
        // Optional: Re-adjust on resolution change
        AdjustCameraViewport();
    }

    void AdjustCameraViewport()
    {
        // Determine the current screen aspect ratio
        float windowAspect = (float)Screen.width / (float)Screen.height;
        // Calculate the scale height based on the target aspect
        float scaleHeight = windowAspect / targetAspect;

        // Get the camera component
        Camera camera = GetComponent<Camera>();

        // If the window is wider than the target aspect
        if (scaleHeight < 1.0f)
        {
            Rect rect = camera.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
            camera.rect = rect;
        }
        // If the window is taller than the target aspect
        else
        {
            Rect rect = camera.rect;
            float scaleWidth = 1.0f / scaleHeight;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
            camera.rect = rect;
        }
    }
}