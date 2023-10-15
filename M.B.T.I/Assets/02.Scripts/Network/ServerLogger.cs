using System;
using System.IO;
using UnityEngine;

public class ServerLogger
{
    private string logFilePath = "server_log.txt"; // 로그 파일 경로

    public void CleanLog(string filePath = null)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            filePath = logFilePath; // filePath가 제공되지 않으면 기본값으로 설정
        }
        try
        {
            // 파일 내용 읽기
            string fileContent = File.ReadAllText(filePath);

            // 파일 내용이 비어 있는지 확인
            if (!string.IsNullOrEmpty(fileContent))
            {
                // 파일 열기 또는 생성
                // 'false'는 기존 파일 내용을 덮어쓰도록 설정
                using (StreamWriter writer = new StreamWriter(filePath, false))
                {
                    writer.Write(""); // 파일 내용 비우기
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
            // 파일 처리 중 오류 발생 시 예외 처리
            Console.WriteLine($"Error cleaning log file: {ex.Message}");
        }
    }

    // 로그 작성 메서드
    public void Log(string message)
    {
        try
        {
            string[] words = message.Split(' '); // 띄어쓰기로 문자열 분할
            // 로그 파일 열기 또는 생성 (FileMode.Append는 파일이 존재하면 이어쓰기 모드)
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                string logEntry = $"{DateTime.Now} destination : {message}";
                writer.WriteLine(logEntry);
            }
        }
        catch (Exception ex)
        {
            // 로그 작성 중 오류가 발생하면 예외 처리
            Console.WriteLine($"Error writing to log file: {ex.Message}");
        }
    }

    public string ExtractWord(string filePath = null)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            filePath = logFilePath; // filePath가 제공되지 않으면 기본값으로 설정
        }
        string fileContent = File.ReadAllText(filePath);        // 전체
        string[] sentences = fileContent.Split('\n');   // 문장

        for (int i = 0; i < sentences.Length; i++)
        {
            // Debug.Log(sentences[i]);
            // return sentences[i];
        }

        // Debug.Log($"sentences.Length : {sentences.Length}");    // 처음 main에 가면 1이다. 
        string data = null;                                 // 직전 문장
        if (sentences.Length >= 3)
        {
            // 직전 룸의 정보를 담아야 하므로 2가 아닌 3을 뺀다. 
            data = sentences[sentences.Length - 3];
        }
        else data = sentences[sentences.Length - 1];        // 처음엔 빈칸


        string[] words = data.Split(' ');           // 단어
        string lastWord = null;                            // 마지막 단어

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
        // 마지막 줄 
        return lastWord;
    }
}