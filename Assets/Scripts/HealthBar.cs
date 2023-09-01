using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;

    private Transform barTransform;

    private void Awake() {
        barTransform = transform.Find("bar");
    }

    private void Start() {
        healthSystem.OnDamaged += HealthBar_OnDamageReceived;
        healthSystem.OnHeal += HealthBar_OnHeal;
        UpdateHealthBar();
    }

    private void HealthBar_OnHeal(object sender, EventArgs e)
    {
                UpdateHealthBar();


    }

    private void HealthBar_OnDamageReceived(object sender, EventArgs e)
    {
        UpdateHealthBar();
    }


    private void UpdateHealthBar()
    {
        barTransform.localScale = new Vector3(healthSystem.GetHealthAmountNormalized(),1f,1f);
        HealthBarVisible();
    }

    private void HealthBarVisible()
    {
        if(healthSystem.IsFullHealth())
        {
            gameObject.SetActive(false);
        }
        else{
            gameObject.SetActive(true);
        }
    }
}
