using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    [Header("References")]
    public Animator policeAnimator;     // أنيميشن الشرطة
    public Animator ambulanceAnimator;  // أنيميشن الإسعاف
    public GameObject arrowObject;      // السهم
    public Transform barrier;           // الحاجز
    public Vector3 barrierNewPosition;  // الموقع الجديد للحاجز

    private void Start()
    {
        // تأكد أن السهم مخفي بالبداية
        if (arrowObject != null)
            arrowObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // تحقق أن الجسم المار هو اللاعب (مثلاً Tag = "Player")
        if (other.CompareTag("Player"))
        {
            Debug.Log("enter");
            // إيقاف أنيميشن الشرطة
            if (policeAnimator != null){
                policeAnimator.SetTrigger("Pass");
                Debug.Log("Pass");}

            // تشغيل أنيميشن الإسعاف
            if (ambulanceAnimator != null){
                ambulanceAnimator.SetTrigger("Start");
                Debug.Log("Start");}

            // إظهار السهم
            if (arrowObject != null){
                arrowObject.SetActive(true);
                Debug.Log("true");}

            // تغيير موقع الحاجز
            if (barrier != null){
                Debug.Log("قبل: " + barrier.position);
                // barrier.position = barrierNewPosition;
                
                Debug.Log("barrierNewPosition");}

                Rigidbody rb = barrier.GetComponent<Rigidbody>();
                if (rb != null && !rb.isKinematic)
                {
                    rb.MovePosition(barrierNewPosition);
                }
                else
                {
                    barrier.position = barrierNewPosition; // أو barrier.localPosition لو تبيه بالنسبة للـ parent
                }
                Debug.Log("بعد: " + barrier.position);
        }
    }
}
