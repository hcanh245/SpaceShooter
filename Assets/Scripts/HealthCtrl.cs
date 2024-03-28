using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthCtrl : MonoBehaviour
{
    public Image _fillBar;
    public TextMeshProUGUI _textHealth;

    public void UpdateBar(int currentHealth, int maxHealth)
    {
        _fillBar.fillAmount = (float)currentHealth / (float)maxHealth;
        if (_textHealth != null)
            _textHealth.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }
}
