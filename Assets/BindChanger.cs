using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
public class BindChanger : MonoBehaviour
{
    // Start is called before the first frame update
    public CinemachineConfiner cineMachineConfider;
    public Collider2D bindMap;

    public void ActionGetter(Action function = null)
    {
        if (function != null)
            function.Invoke();
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //ChangeBindMap();
            ActionGetter(ChangeBindMap);
        }
    }

    public void ChangeBindMap()
    {
        cineMachineConfider.m_BoundingShape2D = bindMap;
    }
}
