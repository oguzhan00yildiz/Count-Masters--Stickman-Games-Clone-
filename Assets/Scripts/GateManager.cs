using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateManager : MonoBehaviour
{
    [SerializeField] private TextMesh gateno;
    public int randomnumber;

    public bool multiply;


    void Start()
    {
        if (multiply)
        {
            randomnumber = Random.Range(2,4);
            gateno.text = randomnumber.ToString() + "x";
        }
        else
        {
            randomnumber = Random.Range(10,100);
            gateno.text = "+" + randomnumber.ToString();
        }

        if (randomnumber % 2 !=0)  //random sayı çift mi kontrol etmek için
        {
            randomnumber+=1;
        }
    }

    
    void Update()
    {
        
    }


}
