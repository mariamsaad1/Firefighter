using UnityEngine;
using UnityEngine.AI;

public class HelperAgent : MonoBehaviour
{
    public string injuredTag = "Injured";        // التاق الخاص بالشخص المصاب
    public string ambulanceTag = "Ambulance";    // التاق الخاص بالنقطة الآمنة
    public Animator animator;                    
    public string helpTriggerName = "help";
    public float reachDistance = 1.5f;           
    public float followSpeed = 4f;              
    public Transform player;                     // اللاعب الذي سيرجع لمتابعته بعد الإسعاف

    private NavMeshAgent agent;
    private GameObject currentTarget;            
    private bool helping = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = followSpeed;

        // البحث عن أول شخصية مصابة
        currentTarget = GameObject.FindGameObjectWithTag(injuredTag);
    }

    void Update()
    {
        if (currentTarget != null)
        {
            float distance = Vector3.Distance(transform.position, currentTarget.transform.position);

            if (!helping)
            {
                // تتبع المصاب
                agent.SetDestination(currentTarget.transform.position);

                if (distance <= reachDistance)
                {
                    // تفعيل الانميشن للمساعدة
                    if (animator != null)
                        animator.SetTrigger(helpTriggerName);

                    helping = true;
                }
            }
            else
            {
                // الذهاب مع المصاب لنقطة الاسعاف
                GameObject ambulance = GameObject.FindGameObjectWithTag(ambulanceTag);
                if (ambulance != null)
                {
                    agent.SetDestination(ambulance.transform.position);

                    // إذا وصل المصاب للاسعاف
                    if (Vector3.Distance(currentTarget.transform.position, ambulance.transform.position) <= reachDistance)
                    {
                        helping = false;  // انتهت مهمة المساعدة
                        currentTarget = null;

                        // الآن يتبع اللاعب
                        if (player != null)
                            agent.SetDestination(player.position);
                    }
                }
            }
        }
        else
        {
            // إذا لا يوجد مصاب محدد، يتبع اللاعب مباشرة
            if (player != null)
                agent.SetDestination(player.position);
        }
    }
}
