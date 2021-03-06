﻿using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Котроллер экрана и обучением игре
/// </summary>
public class InfoController : IController {

    public static InfoController controller;//статическая ссылка на этот контроллер
    public bool swapping, swapped;//происходит ли переход от одной страницы к другой
    private int currentPage;//номер текущей страницы обучения
    public const float fadeTime = 0.15f;//время которое тратится на свап страниц
    private float currentFadeTime;//переменная для расчёта времени перехода от страницы к странице
    public Image fadeImage;//картинка спомощью которой обеспечиваем переход фэйдами

    IController backPoint;//к какому экрану возвращаемся после конца страниц

    public GameObject[] pages;//страницы

    public override void GameLoadInitialization()
    {
        if (controller == null)
            controller = this;
    }

    /// <summary>
    /// Переход к следующей странице
    /// </summary>
    public void Next()
    {
        if (!swapping)
        {
            currentPage++;
            swapping = true;
            currentFadeTime = 0f;
        }
    }

    void Update()
    {
        if(swapping)
        {
            //вычисляем нужный цвет
            currentFadeTime += Time.deltaTime;
            float r, g, b, a;
            r = fadeImage.color.r;
            g = fadeImage.color.g;
            b = fadeImage.color.b;
            if (swapped)//фэйд/анфэйд
                a = 1 - currentFadeTime / fadeTime;
            else
                a = currentFadeTime / fadeTime;
            fadeImage.color = new Color(r, g, b, a);
            if (currentFadeTime > fadeTime)//свапаем экран если прошло нужное время и экран полностью закрылся нашей картинкой
            {
                if (swapped)
                {
                    swapping = false;
                    swapped = false;
                }
                else
                {
                    pages[currentPage].SetActive(true);
                    pages[currentPage - 1].SetActive(false);
                    swapped = true;
                    currentFadeTime = 0f;
                }
            }
        }
    }

    public override void Init()
    {
        //запоминает в какой экран нужно вернуться
        base.Init();
        backPoint = MainController.controller.currentScreen;
        //переходим на первую страницу
        currentPage = 0;
        foreach (GameObject g in pages)
        {
            g.SetActive(false);
        }
        pages[0].SetActive(true);
    }

    /// <summary>
    /// Включает экран из которого сам был включён
    /// </summary>
    public void GoBack()
    {
        MainController.controller.GoToScreen(backPoint, 0.3f);
    }
}
