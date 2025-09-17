using UnityEngine;

public class BedInteract : MonoBehaviour
{
    public DayManager dayManager;
    public SleepUIManager uiManager;

    private bool playerInRange = false;
    private bool isSleeping = false;
    private bool hintShown = false;

    void Start()
    {
        if (dayManager == null)
            dayManager = FindFirstObjectByType<DayManager>();

        if (uiManager == null)
            uiManager = FindFirstObjectByType<SleepUIManager>();
    }

    void Update()
    {
        if (playerInRange && !isSleeping && Input.GetKeyDown(KeyCode.E))
        {
            OnPlayerSleep();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isSleeping && !hintShown)
        {
            playerInRange = true;
            hintShown = true; // показываем подсказку один раз
            uiManager.ShowHint("Нажмите E, чтобы лечь спать");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            hintShown = false; // сброс флага при выходе
            uiManager.HideHint();
        }
    }

    public void OnPlayerSleep()
    {
        if (dayManager != null && uiManager != null)
        {
            isSleeping = true;
            playerInRange = false;
            hintShown = false; // чтобы подсказка больше не включалась

            uiManager.HideHint(); // сразу скрываем подсказку

            int nextDay = dayManager.currentDay + 1;
            dayManager.GoToNextDay();
            StartCoroutine(uiManager.PlaySleepSequence(nextDay));

            StartCoroutine(ResetSleepFlagAfterDelay(3f));
        }
    }

    private System.Collections.IEnumerator ResetSleepFlagAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isSleeping = false;
    }
}
