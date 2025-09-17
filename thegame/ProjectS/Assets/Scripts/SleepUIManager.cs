using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SleepUIManager : MonoBehaviour
{
    public Image fadePanel;
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI hintText;
    private CanvasGroup hintCanvasGroup;

    public float fadeDuration = 1f;
    public float hintFadeDuration = 0.3f;

    private Coroutine hintCoroutine;

    void Start()
    {
        if (fadePanel != null) fadePanel.color = new Color(0, 0, 0, 0);
        if (dayText != null) dayText.gameObject.SetActive(false);

        if (hintText != null)
        {
            hintCanvasGroup = hintText.GetComponent<CanvasGroup>();
            hintCanvasGroup.alpha = 0;
        }
    }

    public void ShowHint(string text)
    {
        if (hintText != null && hintCanvasGroup != null)
        {
            hintText.text = text;

            if (hintCoroutine != null) StopCoroutine(hintCoroutine);
            hintCoroutine = StartCoroutine(FadeHint(1f));
        }
    }

    public void HideHint()
    {
        if (hintText != null && hintCanvasGroup != null)
        {
            if (hintCoroutine != null) StopCoroutine(hintCoroutine);
            hintCoroutine = StartCoroutine(FadeHint(0f));
        }
    }

    private IEnumerator FadeHint(float targetAlpha)
    {
        float startAlpha = hintCanvasGroup.alpha;
        float elapsed = 0f;

        while (elapsed < hintFadeDuration)
        {
            elapsed += Time.deltaTime;
            hintCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / hintFadeDuration);
            yield return null;
        }

        hintCanvasGroup.alpha = targetAlpha;
        hintCoroutine = null;
    }

    public IEnumerator PlaySleepSequence(int newDay)
    {
        HideHint();  // сразу скрываем подсказку перед затемнением

        yield return StartCoroutine(Fade(0, 1));

        if (dayText != null)
        {
            dayText.text = "Day " + newDay;
            dayText.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(2f);

        if (dayText != null) dayText.gameObject.SetActive(false);

        yield return StartCoroutine(Fade(1, 0));
    }

    private IEnumerator Fade(float from, float to)
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, elapsed / fadeDuration);
            if (fadePanel != null) fadePanel.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }
}

