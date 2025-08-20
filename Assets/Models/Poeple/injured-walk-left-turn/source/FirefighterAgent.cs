using UnityEngine;
using UnityEngine.AI;

public class FirefighterAgent : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 4f;
    public float helpDistance = 2f;
    public string victimTag = "Victim";
    public string ambulanceTag = "Ambulance";
    public string playerTag = "Player";
    public Animator animator; // يجب ربطه بالInspector لتشغيل تريجر "help"

    private NavMeshAgent agent;
    private Transform currentVictim;
    private Transform ambulance;
    private Transform player;

    public bool isHelping = false;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;

        // العثور على الإسعاف
        GameObject ambObj = GameObject.FindGameObjectWithTag(ambulanceTag);
        if (ambObj != null) ambulance = ambObj.transform;

        // العثور على اللاعب
        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null) player = playerObj.transform;

        FindNextVictim();
    }

    void Update()
    {
        // البحث المستمر عن Victim جديدة إذا لم يكن يساعد أحدهم
        if (!isHelping)
        {
            FindNextVictim();
        }

        if (currentVictim != null)
        {
            float distance = Vector3.Distance(transform.position, currentVictim.position);

            if (!isHelping)
            {
                // إذا وصل للVictim → يبدأ المساعدة
                if (distance < helpDistance)
                {
                    StartHelping();
                }
                else
                {
                    agent.SetDestination(currentVictim.position);
                }
            }
            else
            {
                // بعد المساعدة → يتجه للإسعاف
                agent.SetDestination(ambulance.position);

                float distToAmbulance = Vector3.Distance(transform.position, ambulance.position);
                if (distToAmbulance <= 1f)
                {
                    Debug.Log($"{name}: وصلت مع الضحية للإسعاف ✅");
                    isHelping = false;
                    currentVictim = null; // تحرير الضحية الحالية
                    FindNextVictim();      // البحث عن ضحية جديدة أو العودة للاعب
                }
            }
        }
       else
        {
            // لا توجد ضحية → تحرك للاعب فقط إذا لا توجد ضحية جديدة
            if (player != null && currentVictim == null){
                agent.SetDestination(player.position);
            }
                
        }

         
    }

    void StartHelping()
    {
        isHelping = true;

        VictimAgent victimScript = currentVictim.GetComponent<VictimAgent>();
        if (victimScript != null)
        {
            victimScript.rescued = true; // Victim تتبع Firefighter
        }

        Debug.Log($"{name}: أبدأ المساعدة على {currentVictim.name} 👨‍🚒");
    }

    public void FindNextVictim()
    {
        
     

        GameObject[] victims = GameObject.FindGameObjectsWithTag(victimTag);
        float minDist = Mathf.Infinity;
        Transform nearestVictim = null;

        foreach (GameObject v in victims)
        {
            VictimAgent va = v.GetComponent<VictimAgent>();
            if (va != null && !va.rescued)
            {
                float dist = Vector3.Distance(transform.position, v.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    nearestVictim = v.transform;
                }
            }
        }

        if (nearestVictim != null)
        {
            currentVictim = nearestVictim;
            agent.SetDestination(currentVictim.position);
            Debug.Log($"{name}: الضحية التالية هي {currentVictim.name}");
        }
        // else
        // {
        //     currentVictim = null;
        //     Debug.Log($"{name}: لا يوجد ضحية أخرى، الرجوع للاعب 👤");
        //     if (player != null)
        //         agent.SetDestination(player.position);
        // }
    }
}

