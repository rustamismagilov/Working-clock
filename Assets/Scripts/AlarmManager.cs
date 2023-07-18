using System;
using TMPro;
using UnityEngine;

public class AlarmManager : MonoBehaviour
{
    public TMP_InputField alarmTimeInput; // Ссылка на текстовое поле для ввода времени будильника
    private bool alarmSet = false; // Флаг, указывающий, установлен ли будильник
    private DateTime alarmTime; // Время, на которое установлен будильник



    // Метод для обработки события нажатия на кнопку "SetAlarmButton"
    public void SetAlarm()
    {
        //Debug.Log("SetAlarm() method called!");
        Debug.Log("Alarm activated!");

        // Проверяем, введено ли время в текстовое поле
        if (!string.IsNullOrEmpty(alarmTimeInput.text))
        {
            // Получаем время, введенное пользователем, и парсим его в формат DateTime
            DateTime.TryParse(alarmTimeInput.text, out alarmTime);
            // Устанавливаем флаг, что будильник установлен
            alarmSet = true;
        }
    }

    // Метод для проверки, активирован ли будильник
    private void CheckAlarm()
    {
        if (alarmSet && DateTime.Now.Hour == alarmTime.Hour && DateTime.Now.Minute == alarmTime.Minute)
        {
            // Активируем будильник, когда текущее время совпадает с временем будильника
            // Здесь можно добавить звуковой или визуальный эффект для оповещения пользователя
            
            // Сбрасываем флаг, чтобы будильник активировался только один раз
            alarmSet = false;
        }
    }

    void Update()
    {
        CheckAlarm();
    }
}
