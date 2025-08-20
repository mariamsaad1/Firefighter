using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportInteraction : MonoBehaviour
{
    public GameObject body; // الكائن الذي سيتم التحكم فيه (المكعب)
    public GameObject face;
    public Color changeColor = Color.red; // اللون الذي سيتم تغييره عند خروج اللاعب
    private Renderer bodyRenderer;
    private Renderer faceRenderer;


    void Start()
    {
        bodyRenderer = body.GetComponent<Renderer>();
        faceRenderer = face.GetComponent<Renderer>();
        body.SetActive(true); // تأكد من أن المكعب مرئي في البداية
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            body.SetActive(false); // إخفاء المكعب عند دخول اللاعب منطقة النقل
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            body.SetActive(true); // إظهار المكعب عند خروج اللاعب من منطقة النقل
            // if (changeMaterial != null)
            // {
            //     cubeRenderer.material = changeMaterial; // تغيير المادة إذا كانت محددة
            // }
            // else
            // {
                bodyRenderer.material.color = changeColor; // تغيير اللون إذا لم تكن المادة محددة
                faceRenderer.material.color = changeColor;
            // }
        }
    }
}
