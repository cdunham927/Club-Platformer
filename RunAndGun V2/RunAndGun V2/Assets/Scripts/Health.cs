using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Image healthBar;

    public void UpdateHealth(float newHealth)
    {
        healthBar.fillAmount = newHealth;
    }
}
