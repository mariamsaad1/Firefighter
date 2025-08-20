using UnityEngine;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
    }

    // Update is called once per frame
    // public void FadeOut()
    // {
    //     animator.Play("FadeOut");
    // }


        public void FadeOut()
    {
        StartCoroutine(FadeOutAndSwitch());
    }

    IEnumerator FadeOutAndSwitch()
    {
        animator.Play("FadeOut");

        // احصل على طول الأنيميشن
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float animationLength = stateInfo.length;

        // انتظر إلى أن ينتهي
        yield return new WaitForSeconds(animationLength);

        // بعد ما ينتهي
        SceneController.GoToGameScene();
    }

    public void FadeIn()
    {
        animator.Play("FadeIn");
    }
}
