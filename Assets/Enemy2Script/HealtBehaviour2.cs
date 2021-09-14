using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealtBehaviour2 : MonoBehaviour
{
    public Text healthText2;
    public Image healthBar2;
    float health = 100;

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthText2.text = string.Format("%{0}", health);
        healthBar2.fillAmount = health / 100;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
