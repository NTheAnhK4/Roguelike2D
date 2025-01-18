
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : Singleton<EnergyManager>
{
    [SerializeField] private Image energyImage;
    [SerializeField] private TextMeshProUGUI energyText;
    [SerializeField] private int curEnergyValue = 10;
    [SerializeField] private int maxEnergyValue = 10;
   
    private void Init()
    {
        if (energyImage == null)  energyImage = transform.Find("Mask").Find("Energy").GetComponent<Image>();
        if (energyText == null) energyText = transform.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Reset()
    {
        Init();
    }

    private void Start()
    {
        Init();
    }

    private void SetEnergyUI()
    {
        energyImage.fillAmount = 1.0f * curEnergyValue / maxEnergyValue;
        energyText.text = curEnergyValue + "/" + maxEnergyValue;
    }
    public void TakeEnegy(int energy = 1)
    {
        curEnergyValue = Math.Min(curEnergyValue - 1, 0);
        if(curEnergyValue == 0) ObserverManager.Notify(EventId.Lose);
        SetEnergyUI();
    }

    public void AddEnergy(int energy)
    {
        curEnergyValue = Math.Min(curEnergyValue + energy, maxEnergyValue);
        SetEnergyUI();
    }
    
}
