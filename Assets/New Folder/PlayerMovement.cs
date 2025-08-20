using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float baseSpeed = 2f;       // السرعة الأساسية
    public float speedMultiplier = 0.2f; // كل ما اللاعب يتقدم للأمام تزيد السرعة بهذا المعدل
    public float forwardProgress = 0f;   // يتغير حسب تقدم اللاعب

    [Header("References")]
    public Transform playerTransform;  // اللاعب أو الكاميرا XR
    public Rigidbody rb;               // Rigidbody للأوبجيكت

    void Update()
    {
        // قراءة المحور الأفقي من الجويستك (يمين/يسار)
        float horizontalInput = Input.GetAxis("Horizontal"); 
        // (في XR تقدر تستبدلها بـ Unity XR Input System)

        // حساب السرعة الحالية حسب التقدم للأمام
        float currentSpeed = baseSpeed + (forwardProgress * speedMultiplier);

        // إنشاء متجه الحركة (يمين/يسار فقط)
        Vector3 movement = new Vector3(horizontalInput, 0, 0) * currentSpeed * Time.deltaTime;

        // تحريك الجسم
        rb.MovePosition(rb.position + movement);

        // مثال بسيط: كل فريم اللاعب يمشي قدام → يزيد التقدم
        forwardProgress += Time.deltaTime; 
    }
}
