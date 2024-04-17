using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health_Bar : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image _healthbarForeGroundImage;
    public void UpdateHealthBar(HealthController healthController)
    {
        _healthbarForeGroundImage.fillAmount = healthController.RemainingHealthPercentage;
    }
}
