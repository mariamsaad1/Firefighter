using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleInteraction : MonoBehaviour
{
    public Camera mainCamera;
    public Transform zoomTarget;
    public Canvas confirmationCanvas;
    private bool isZoomed = false;

    void Start()
    {
        confirmationCanvas.gameObject.SetActive(false);
    }

    void OnMouseDown()
    {
        if (!isZoomed)
        {
            confirmationCanvas.gameObject.SetActive(true);
        }
    }

    public void ConfirmZoom(bool confirm)
    {
        if (confirm)
        {
            mainCamera.transform.position = zoomTarget.position;
            mainCamera.transform.rotation = zoomTarget.rotation;
            isZoomed = true;
        }
        confirmationCanvas.gameObject.SetActive(false);
    }

    public void ResetView()
    {
        // إعدادات إعادة التعيين (مثل إعادة الكاميرا إلى الوضع الافتراضي)
    }
}