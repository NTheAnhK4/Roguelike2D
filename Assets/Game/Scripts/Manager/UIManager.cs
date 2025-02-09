using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Transform m_LoseUI;

    private void LoadComponent()
    {
        if (m_LoseUI == null) m_LoseUI = transform.Find("LoseUI");
    }

    private void Reset()
    {
        LoadComponent();
    }

    protected override void Awake()
    {
        base.Awake();
        LoadComponent();
        ObserverManager.Attach(EventId.Lose, (param) =>
        {
            DisplayLoseUI(true);
        });
    }

    public void DisplayLoseUI(bool isDisplay)
    {
        m_LoseUI.gameObject.SetActive(isDisplay);
    }
}
