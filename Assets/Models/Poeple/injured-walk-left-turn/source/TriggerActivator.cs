using UnityEngine;

public class TriggerActivator : MonoBehaviour
{

    public GameObject[] objectsToActivate;
    public GameObject[] objectsToDeactivate;
    private string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            // تفعيل الكائنات
            foreach (GameObject obj in objectsToActivate)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                }
            }

            // إخفاء الكائنات
            foreach (GameObject obj in objectsToDeactivate)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }
        }
    }
}
