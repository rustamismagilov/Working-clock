using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;
using TMPro;

public class ClockHandMovement : MonoBehaviour
{
    public Transform hourHandTransform;
    public Transform minuteHandTransform;
    public Transform secondHandTransform;
    public TMP_Text digitalTimeText;
    public List<string> timeServiceUrls = new List<string>
    {
        "http://time.google.com/",
        "http://time.windows.com/",
        "http://pool.ntp.org/",
        "http://time.apple.com/"
        // Add more NTP server URLs as needed
    };

    // Start is called before the first frame update
    void Start()
    {
        // Выполняем запрос времени при старте приложения
        StartCoroutine(RequestTimeFromAPI("https://time.google.com/"));
        // Запускаем повторяющийся запрос времени каждый час
        InvokeRepeating("RequestTimeFromAPI", 3600f, 3600f);

        // Request time from all the time services listed in the timeServiceUrls list
        foreach (string url in timeServiceUrls)
        {
            StartCoroutine(RequestTimeFromAPI(url));
        }
    }

    private void UpdateClockHands()
    {
        // Получаем текущее время
        DateTime currentTime = DateTime.Now;

        // Вычисляем углы для часовой, минутной и секундной стрелок
        float hoursAngle = (currentTime.Hour % 12 + currentTime.Minute / 60f) * 30f;
        float minutesAngle = currentTime.Minute * 6f;
        float secondsAngle = currentTime.Second * 6f;

        // Применяем повороты к стрелкам
        hourHandTransform.localRotation = Quaternion.Euler(0f, 0f, -hoursAngle);
        minuteHandTransform.localRotation = Quaternion.Euler(0f, 0f, -minutesAngle);
        secondHandTransform.localRotation = Quaternion.Euler(0f, 0f, -secondsAngle);

        string currentTimeString = DateTime.Now.ToString("HH:mm:ss"); // Формат "часы:минуты:секунды"
        digitalTimeText.text = currentTimeString;
    }

    private IEnumerator RequestTimeFromAPI(string url)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        using (Stream stream = response.GetResponseStream())
        using (StreamReader reader = new StreamReader(stream))
        {
            string jsonResponse = reader.ReadToEnd();
            // Здесь производите разбор ответа сервиса и получение времени из JSON-ответа
            // Возможно, вам понадобится использовать библиотеки для разбора JSON, например, Newtonsoft.Json
        }

        yield return null; // Возвращаем null для завершения корутины без ошибок
    }




    // Update is called once per frame
    void Update()
    {
        // Обновляем позиции стрелок
        UpdateClockHands();

        // Вызываем метод для обновления времени с интернет-сервиса каждый час
        if (DateTime.Now.Minute == 0 && DateTime.Now.Second == 0)
        {
            foreach (string url in timeServiceUrls)
            {
                StartCoroutine(RequestTimeFromAPI("https://time.google.com/"));
            }
        }
    }

}
