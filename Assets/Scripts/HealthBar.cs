using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HealthBar : MonoBehaviour
{
    private Image HealthBarImage;
    private float _red = .35f;

    private void Start()
    {
        HealthBarImage = GetComponent<Image>();
        HealthBarImage.color = Color.green;
    }

    public void SetHealthBarValue(float value)
    {
        HealthBarImage.fillAmount = value;
        if (HealthBarImage.fillAmount < _red) {
            HealthBarImage.color = Color.red;
        }
        else if (HealthBarImage.fillAmount >= _red)
        {
            HealthBarImage.color = Color.green;
        }
    }
}
