using UnityEngine;

public class GamePauseManager : MonoBehaviour
{
    [Header("Pause Settings")]
    [SerializeField] private bool isPaused = false;  // يمكن التحكم فيها من Inspector

    [Header("Objects to Ignore When Paused")]
    [SerializeField] private GameObject[] ignoreObjects; // الأشياء المستثناة من الإيقاف

    public bool IsPaused => isPaused;

    private void Start()
    {
        ApplyPauseState();
    }

    // دالة لإيقاف اللعبة
    public void PauseGame()
    {
        if (isPaused) return;
        isPaused = true;
        ApplyPauseState();
        Debug.Log("Game Paused");
    }

    // دالة لإستئناف اللعبة
    public void ResumeGame()
    {
        if (!isPaused) return;
        isPaused = false;
        ApplyPauseState();
        Debug.Log("Game Resumed");
    }

    // دالة لتبديل حالة الـ pause
    public void TogglePause()
    {
        isPaused = !isPaused;
        ApplyPauseState();
    }

    // الدالة الرئيسية لتطبيق حالة pause
    private void ApplyPauseState()
    {
        // جمع كل الانميترز
        Animator[] allAnimators = FindObjectsOfType<Animator>();

        foreach (var anim in allAnimators)
        {
            // إذا العنصر موجود في الاستثناء لا نوقفه
            // if (IsIgnored(anim.gameObject)) continue;

            // إيقاف أو تفعيل الأنميشن حسب حالة الـ pause
            anim.enabled = !isPaused;
        }
    }

    // دالة للتحقق إذا العنصر موجود في الاستثناء
    // private bool IsIgnored(GameObject obj)
    // {
    //     if (obj == null) return false;

    //     foreach (var go in ignoreObjects)
    //     {
    //         if (go == obj) return true;
    //     }

    //     return false;
    // }
}
