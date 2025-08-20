using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class VictimAgent : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 3.5f;           
    public float safeDistanceFromFire = 10f; 
    public string fireTag = "Fire";

    [Header("Respawn Settings")]
    public GameObject victimPrefab;          
    public Transform[] spawnPoints;          
    public float minRespawnTime = 2f;       
    public float maxRespawnTime = 5f;       

    // [HideInInspector] 
    public bool rescued = false; 
    private NavMeshAgent agent;
    private Transform firefighter;
    private Transform ambulance;
    private Transform nearestFire;
    // public FirefighterAgent firefighterAgent;
    public FirefighterAgent firefighterAgent ;

    void Start()
    {


        gameObject.tag = "Victim";
    firefighterAgent = FindObjectOfType<FirefighterAgent>();

        rescued = false; 
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;

        GameObject ambObj = GameObject.FindGameObjectWithTag("Ambulance");
        if (ambObj != null) ambulance = ambObj.transform;

        Debug.Log($"{name}: VictimAgent بدأ التشغيل ✅");
                // البحث عن كل الكائنات التي عليها التاج "Spawn"
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("Spawn");

        // إنشاء مصفوفة Transform بنفس الطول
        spawnPoints = new Transform[spawns.Length];

        // تعبئة المصفوفة بالـ Transform لكل كائن
        for (int i = 0; i < spawns.Length; i++)
        {
            spawnPoints[i] = spawns[i].transform;
        }
    }

    void Update()
    {
        if (rescued && firefighter != null && ambulance != null)
        {
            agent.SetDestination(ambulance.position);
        }
        else
        {
            FindNearestFire();
            if (nearestFire != null)
            {
                Vector3 dirAway = (transform.position - nearestFire.position).normalized;
                Vector3 targetPos = transform.position + dirAway * safeDistanceFromFire;
                agent.SetDestination(targetPos);
                Debug.Log($"{name}: أبعد عن النار 🔥 باتجاه {targetPos}");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Firefighter"))
        {
            firefighter = other.transform;
            rescued = true;
            agent.SetDestination(ambulance.position);
            Debug.Log($"{name}: Firefighter وصل إلي! 👨‍🚒");
              // تشغيل تريجر "help" على Animator إذا موجود
            if (firefighterAgent.animator != null)
                firefighterAgent.animator.SetTrigger("help");
        }

        if (other.CompareTag("Ambulance"))
        {
            if (gameObject.CompareTag("Victim"))
                gameObject.tag = "Untagged";
                firefighterAgent.FindNextVictim();

            Debug.Log($"{name}: تم إزالة التاق قبل الانتظار");
            StartCoroutine(RespawnCoroutine());

        }
    }
        void OnTriggerStay(Collider other)
    {
        firefighterAgent.FindNextVictim();
        if (other.CompareTag("Ambulance")){
            if (firefighterAgent.animator != null)
            firefighterAgent.animator.SetTrigger("NotHelp");
        }
    }


    

    void FindNearestFire()
    {
        GameObject[] fires = GameObject.FindGameObjectsWithTag(fireTag);
        float minDist = Mathf.Infinity;
        nearestFire = null;

        foreach (GameObject fire in fires)
        {
            float dist = Vector3.Distance(transform.position, fire.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearestFire = fire.transform;
            }
        }

        if (nearestFire != null)
            Debug.Log($"{name}: أقرب نار تبعد {minDist:F1} متر");
    }

    // IEnumerator RespawnCoroutine()
    // {
    //     // الانتظار قبل إعادة التوليد
    //     float waitTime = Random.Range(minRespawnTime, maxRespawnTime);
    //     yield return new WaitForSeconds(waitTime);

    //     // توليد نسخة جديدة عشوائية
    //     if (spawnPoints.Length > 0 && victimPrefab != null)
    //     {
    //         Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
    //         GameObject newVictim = Instantiate(victimPrefab, spawn.position, spawn.rotation);

    //         VictimAgent va = newVictim.GetComponent<VictimAgent>();
    //         if (va != null)
    //             va.rescued = false;

    //         Debug.Log($"نسخة جديدة من Victim تم إنشاؤها في {spawn.position}");

    //         // **تحديث كل Firefighter لملاحقة الضحية الجديدة فورًا**
    //         FirefighterAgent[] firefighters = FindObjectsOfType<FirefighterAgent>();
    //         foreach (FirefighterAgent ff in firefighters)
    //         {
    //             ff.FindNextVictim();
    //         }
    //     }

    //     // إزالة النسخة القديمة
    //     Destroy(gameObject);
    // }



    IEnumerator RespawnCoroutine()
{
    // الانتظار قبل إعادة التوليد
    float waitTime = Random.Range(minRespawnTime, maxRespawnTime);
    yield return new WaitForSeconds(waitTime);

    // توليد نسخة جديدة عشوائية
    if (spawnPoints.Length > 0 && victimPrefab != null)
    {
        Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject newVictim = Instantiate(victimPrefab, spawn.position, spawn.rotation);

        VictimAgent va = newVictim.GetComponent<VictimAgent>();
        if (va != null)
            va.rescued = false;
        if (firefighterAgent != null)
        {
            firefighterAgent.isHelping = false; 
        }

        Debug.Log($"نسخة جديدة من Victim تم إنشاؤها في {spawn.position}");
            
    
    }

    // إزالة النسخة القديمة
    Destroy(gameObject);
}

}
