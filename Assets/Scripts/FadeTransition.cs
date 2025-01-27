using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeTransition : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1f;

    public void StartTransition()
    {
        StartCoroutine(Transition());
    }

    private IEnumerator Transition()
    {
        yield return StartCoroutine(Fade(0f, 1f)); 
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1; 
        SceneManager.LoadScene(nextSceneIndex); 
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        canvasGroup.alpha = startAlpha;

        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
    }
}