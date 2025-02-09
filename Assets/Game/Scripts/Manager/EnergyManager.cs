
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

    public void Init(int energy)
    {
        curEnergyValue = energy;
        maxEnergyValue = energy;
        SetEnergyUI();
    }

    private void LoadComponent()
    {
        if (energyImage == null)  energyImage = transform.Find("Mask").Find("Energy").GetComponent<Image>();
        if (energyText == null) energyText = transform.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Reset()
    {
        LoadComponent();
    }

    private void Start()
    {
        LoadComponent();
    }

    private void SetEnergyUI()
    {
        energyImage.fillAmount = 1.0f * curEnergyValue / maxEnergyValue;
        energyText.text = curEnergyValue + "/" + maxEnergyValue;
    }
    public void TakeEnegy(int energy = 1)
    {
        curEnergyValue = Math.Max(curEnergyValue - energy, 0);
        if(curEnergyValue == 0) ObserverManager.Notify(EventId.Lose);
        SetEnergyUI();
    }

    public void AddEnergy(int energy)
    {
        curEnergyValue = Math.Min(curEnergyValue + energy, maxEnergyValue);
        SetEnergyUI();
    }
    
}
