using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private RectTransform hBar;
    private Image barImage;
    // Start is called before the first frame update
    void Start()
    {
        hBar = GetComponent<RectTransform>();
        barImage = GetComponent<Image>();
        if (Health.totHealth < 0.3f)
        {
            barImage.color = Color.red;
        }
       
        UpdateSize(Health.totHealth);
    }

    public void Damage(float dmg){
        if ((Health.totHealth - dmg)  > 0 )
        {
            Health.totHealth -= dmg;
        }
        else {
            Health.totHealth = 0;
        }
        
        if (Health.totHealth < 0.3f)
        {
            barImage.color = Color.red;
        }
        
        UpdateSize(Health.totHealth);
    }

    public void UpdateSize(float size)
    {
        hBar.localScale = new Vector3(size, 1f);
    }


}
