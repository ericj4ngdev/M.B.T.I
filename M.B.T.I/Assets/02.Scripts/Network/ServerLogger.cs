using System;
using System.IO;
using UnityEngine;

public class ServerLogger
{
    private string logFilePath = "server_log.txt"; // �α� ���� ���

    public void CleanLog(string filePath = null)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            filePath = logFilePath; // filePath�� �������� ������ �⺻������ ����
        }
        try
        {
            // ���� ���� �б�
            string fileContent = File.ReadAllText(filePath);

            // ���� ������ ��� �ִ��� Ȯ��
            if (!string.IsNullOrEmpty(fileContent))
            {
                // ���� ���� �Ǵ� ����
                // 'false'�� ���� ���� ������ ������� ����
                using (StreamWriter writer = new StreamWriter(filePath, false))
                {
                    writer.Write(""); // ���� ���� ����
                    Console.WriteLine("Log file content cleared.");
                }
            }
            else
            {
                Console.WriteLine("Log file is empty. No action required.");
            }
        }
        catch (Exception ex)
        {
            // ���� ó�� �� ���� �߻� �� ���� ó��
            Console.WriteLine($"Error cleaning log file: {ex.Message}");
        }
    }

    // �α� �ۼ� �޼���
    public void Log(string message)
    {
        try
        {
            string[] words = message.Split(' '); // ����� ���ڿ� ����
            // �α� ���� ���� �Ǵ� ���� (FileMode.Append�� ������ �����ϸ� �̾�� ���)
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                string logEntry = $"{DateTime.Now} destination : {message}";
                writer.WriteLine(logEntry);
            }
        }
        catch (Exception ex)
        {
            // �α� �ۼ� �� ������ �߻��ϸ� ���� ó��
            Console.WriteLine($"Error writing to log file: {ex.Message}");
        }
    }

    public string ExtractWord(string filePath = null)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            filePath = logFilePath; // filePath�� �������� ������ �⺻������ ����
        }
        string fileContent = File.ReadAllText(filePath);        // ��ü
        string[] sentences = fileContent.Split('\n');   // ����

        for (int i = 0; i < sentences.Length; i++)
        {
            // Debug.Log(sentences[i]);
            // return sentences[i];
        }

        // Debug.Log($"sentences.Length : {sentences.Length}");    // ó�� main�� ���� 1�̴�. 
        string data = null;                                 // ���� ����
        if (sentences.Length >= 3)
        {
            // ���� ���� ������ ��ƾ� �ϹǷ� 2�� �ƴ� 3�� ����. 
            data = sentences[sentences.Length - 3];
        }
        else data = sentences[sentences.Length - 1];        // ó���� ��ĭ


        string[] words = data.Split(' ');           // �ܾ�
        string lastWord = null;                            // ������ �ܾ�

        foreach (var VARIABLE in words)
        {
            Debug.Log(VARIABLE);
        }
        if (words.Length > 0)
        {
            string temp = words[words.Length - 1];
            lastWord = temp.Replace("\r", "");
        }

        // Debug.Log($"data : {data}");
        Debug.Log($"lastWord : {lastWord}");
        // ������ �� 
        return lastWord;
    }
}