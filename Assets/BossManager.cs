using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    [Header("Boss Settings")]
    [SerializeField] private float bossHealth=35;
    [SerializeField] private float currenthealth;
    [SerializeField] private Image sliderimage;
    [SerializeField] private float fillamount;
    public static BossManager bossManager;

    
    void Start()
    {
        currenthealth=bossHealth;
        sliderimage = transform.parent.GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>();
        bossManager=this;
    }

    
    void Update()
    {
        fillamount=(currenthealth / bossHealth);
        sliderimage.fillAmount = fillamount;

        if (fillamount < 0.01f)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    public void BossGetDamage()
    {
        currenthealth = currenthealth -1;
    }

   

    
}
