using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DayTaskSet
{
    public int dayNumber = 1;
    public List<string> tasks = new List<string>();
}

public class DayManager : MonoBehaviour
{
    [Header("Task Manager")]
    public TaskManager taskManager;

    [Header("Day Settings")]
    public int currentDay = 1;
    public List<DayTaskSet> dayTaskSets = new List<DayTaskSet>();

    void Start()
    {
        if (taskManager == null)
            taskManager = FindFirstObjectByType<TaskManager>();

        StartDay(currentDay);
    }

    // Запуск конкретного дня
    public void StartDay(int day)
    {
        currentDay = day;

        var set = dayTaskSets.Find(s => s.dayNumber == day);

        if (taskManager != null)
        {
            taskManager.SetTasks(set != null ? new List<string>(set.tasks) : new List<string>());
        }
        else
        {
            Debug.LogWarning("TaskManager not found!");
        }

        Debug.Log("Day " + currentDay + " started.");
    }

    // Переход к следующему дню
    public void GoToNextDay()
    {
        StartCoroutine(_GoToNextDay());
    }

    private IEnumerator _GoToNextDay()
    {
        // Можно добавить анимацию сна или затемнение экрана
        Debug.Log("Player is going to sleep...");

        yield return new WaitForSeconds(1f); // Задержка перед новым днем

        currentDay++;
        StartDay(currentDay);

        Debug.Log("New day started: " + currentDay);
    }
}

