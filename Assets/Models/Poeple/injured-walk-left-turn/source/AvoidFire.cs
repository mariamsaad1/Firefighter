using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class InjuredAgent : MonoBehaviour
{
    public Transform fire;              
    public float safeDistance = 5f;     
    public float moveSpeed = 3.5f;      
    public string firefighterTag = "Firefighter"; 
    public string ambulanceTag = "Ambulance";     

    private NavMeshAgent agent;
    private bool beingHelped = false;
    private GameObject firefighter;     
    private GameObject ambulance;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.enabled = true;
        gameObject.tag = "Injured";

        // العثور على نقطة الإسعاف
        ambulance = GameObject.FindGameObjectWithTag(ambulanceTag);
    }

    void Update()
    {
        if (fire == null) return;

        // البحث عن أقرب مساعد
        if (!beingHelped)
        {
            firefighter = GameObject.FindGameObjectWithTag(firefighterTag);
            if (firefighter != null)
            {
                float distanceToFirefighter = Vector3.Distance(transform.position, firefighter.transform.position);
                if (distanceToFirefighter <= safeDistance) // يمكن ضبط المسافة المطلوبة للالتقاء
                {
                    beingHelped = true;
                }
            }
        }

        if (beingHelped && firefighter != null && ambulance != null)
        {
            // يتحرك المصاب مع المساعد نحو الإسعاف
            agent.SetDestination(ambulance.transform.position);
        }
        else
        {
            // الابتعاد عن النار بشكل مستقل
            float distanceToFire = Vector3.Distance(transform.position, fire.position);
            if (distanceToFire < safeDistance)
            {
                Vector3 directionAway = (transform.position - fire.position).normalized;
                Vector3 targetPosition = transform.position + directionAway * safeDistance;

                NavMeshHit hit;
                if (NavMesh.SamplePosition(targetPosition, out hit, safeDistance, NavMesh.AllAreas))
                {
                    agent.SetDestination(hit.position);
                }
            }
        }
    }
}
