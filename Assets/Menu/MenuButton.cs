﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Кнопка в меню игры
/// </summary>
public class MenuButton : InteractableObject {

    public Vector3 posUp, posDown;
    public string function;
    Material mat;
    public Texture textureUp, textureDown;
    public bool onIt;
    public bool pressed;
    
    void Start()
    {
        if (mat == null)
        {
            //клонируем материал, чттобы мы могли его спокойно изменять во время игры
            mat = new Material(transform.GetChild(0).GetComponent<MeshRenderer>().material);
            transform.GetChild(0).GetComponent<MeshRenderer>().material = mat;
        }
    }

    public void SetTextureUp(Texture t)
    {
        if(mat == null)
        {
            Start();
        }
        textureUp = t;
        if(transform.position == posUp)
        {
            mat.SetTexture("_MainTex", textureUp);
        }
    }

    public override void Interact()
    {
        base.Interact();
        if (MenuController.controller.gameObject.activeSelf)
        {
            MenuController.controller.CallFunction(function);
        }
        else
        {
            PostMenuController.controller.CallFunction(function);
        }
    }

    public override void PreInteract()
    {
        base.PreInteract();
        onIt = true;
    }

    /// <summary>
    /// "Вдавливание" кнопки
    /// </summary>
    public void Press()
    {
        transform.position = posDown;
        mat.SetTexture("_MainTex", textureDown);
        Debug.Log("Pressed");
    }

    void Update()
    {
        if(!onIt && !pressed)
        {
            Unpress();
        }
        else
        {
            Press();
        }
        onIt = false;
    }

    /// <summary>
    /// "Выдавливание" кнопки
    /// </summary>
    public void Unpress()
    {
        if (mat != null)
        {
            transform.position = posUp;
            mat.SetTexture("_MainTex", textureUp);
        }
    }
}
