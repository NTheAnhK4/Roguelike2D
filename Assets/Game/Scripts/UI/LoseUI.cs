using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoseUI : MonoBehaviour
{
    [SerializeField] private Transform loseHolder;
    [SerializeField] private TextMeshProUGUI loseInfor;
    
    private void LoadComponent()
    {
        if (loseHolder == null) loseHolder = transform.Find("LoseImg");
        if (loseInfor == null) loseInfor = loseHolder.Find("Infor").GetComponent<TextMeshProUGUI>();
    }

    private void Awake()
    {
        LoadComponent();
        ObserverManager.Attach(EventId.NewGame, param =>
        {
            transform.gameObject.SetActive(false);
        });
    }

    private void Reset()
    {
        LoadComponent();
    }

    private void OnEnable()
    {
        loseInfor.text = "Survived " + GameManager.Instance.CurrentLevel + " day";
    }
}
