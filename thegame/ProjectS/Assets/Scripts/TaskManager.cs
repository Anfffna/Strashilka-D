using System.Collections.Generic;
using TMPro; // Для работы с текстом TextMeshProUGUI
using UnityEngine;

// Класс, который описывает отдельную задачу
[System.Serializable]
public class TaskItem
{
    public string description; // Текст задачи
    public bool isCompleted;   // Выполнена ли задача
}

public class TaskManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI taskText; // Сюда перетащим текст на Canvas

    [Header("Initial tasks (Inspector)")]
    public List<TaskItem> tasks = new List<TaskItem>(); // Список всех задач

    // Метод запускается один раз при старте сцены
    void Start()
    {
        UpdateUI(); // Показываем начальные задачи на экране
    }

    // Метод для замены списка задач, например при новом дне
    public void SetTasks(List<string> newTasks)
    {
        tasks.Clear(); // Очищаем старые задачи
        foreach (var t in newTasks)
        {
            tasks.Add(new TaskItem { description = t, isCompleted = false });
        }
        UpdateUI(); // Обновляем UI после замены
    }

    // Отмечает задачу как выполненную по её тексту
    public void CompleteTaskByDescription(string description)
    {
        for (int i = 0; i < tasks.Count; i++)
        {
            if (tasks[i].description == description)
            {
                tasks[i].isCompleted = true;
                UpdateUI(); // Обновляем UI сразу после выполнения
                return;      // Выходим, чтобы не искать дальше
            }
        }
        Debug.LogWarning("Task not found: " + description); // Если не нашли задачу
    }

    // Обновляет текст на экране
    void UpdateUI()
    {
        if (taskText == null) return; // Если UI не назначен, ничего не делаем

        string result = "";
        foreach (var task in tasks)
        {
            // Если задача выполнена, зачеркиваем текст
            result += task.isCompleted ? "<s>" + task.description + "</s>\n" : task.description + "\n";
        }

        taskText.text = result; // Пишем все задачи в TextMeshProUGUI
    }
}
