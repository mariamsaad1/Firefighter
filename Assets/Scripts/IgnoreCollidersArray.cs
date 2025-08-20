using UnityEngine;

public class IgnoreCollidersArray : MonoBehaviour
{
    [Header("Rigidbody to ignore collisions with")]
    public Rigidbody rb;                // الجسم اللي تبي يتجاهل التصادم
    public Collider[] collidersToIgnore; // المصفوفة اللي فيها الكولايدرز

    void Start()
    {
        if (rb == null) return;

        foreach (Collider col in collidersToIgnore)
        {
            if (col != null)
            {
                Physics.IgnoreCollision(rb.GetComponent<Collider>(), col, true);
            }
        }
    }
}
