using System.Collections.Generic;
using TMPro; // ��� ������ � ������� TextMeshProUGUI
using UnityEngine;

// �����, ������� ��������� ��������� ������
[System.Serializable]
public class TaskItem
{
    public string description; // ����� ������
    public bool isCompleted;   // ��������� �� ������
}

public class TaskManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI taskText; // ���� ��������� ����� �� Canvas

    [Header("Initial tasks (Inspector)")]
    public List<TaskItem> tasks = new List<TaskItem>(); // ������ ���� �����

    // ����� ����������� ���� ��� ��� ������ �����
    void Start()
    {
        UpdateUI(); // ���������� ��������� ������ �� ������
    }

    // ����� ��� ������ ������ �����, �������� ��� ����� ���
    public void SetTasks(List<string> newTasks)
    {
        tasks.Clear(); // ������� ������ ������
        foreach (var t in newTasks)
        {
            tasks.Add(new TaskItem { description = t, isCompleted = false });
        }
        UpdateUI(); // ��������� UI ����� ������
    }

    // �������� ������ ��� ����������� �� � ������
    public void CompleteTaskByDescription(string description)
    {
        for (int i = 0; i < tasks.Count; i++)
        {
            if (tasks[i].description == description)
            {
                tasks[i].isCompleted = true;
                UpdateUI(); // ��������� UI ����� ����� ����������
                return;      // �������, ����� �� ������ ������
            }
        }
        Debug.LogWarning("Task not found: " + description); // ���� �� ����� ������
    }

    // ��������� ����� �� ������
    void UpdateUI()
    {
        if (taskText == null) return; // ���� UI �� ��������, ������ �� ������

        string result = "";
        foreach (var task in tasks)
        {
            // ���� ������ ���������, ����������� �����
            result += task.isCompleted ? "<s>" + task.description + "</s>\n" : task.description + "\n";
        }

        taskText.text = result; // ����� ��� ������ � TextMeshProUGUI
    }
}
