
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : Singleton<EnergyManager>
{
    [SerializeField] private Image energyImage;
    [SerializeField] private TextMeshProUGUI energyText;
    [SerializeField] private float curEnergyValue = 100f;
    [SerializeField] private float maxEnergyValue = 100;
   
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

    public void TakeEnegy(float energy = 1)
    {
        curEnergyValue -= energy;
        energyImage.fillAmount = curEnergyValue / maxEnergyValue;
        energyText.text = curEnergyValue + "/" + maxEnergyValue;
    }
}
