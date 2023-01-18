using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateManager : MonoBehaviour
{
    [SerializeField] private TextMesh gateno;

    [SerializeField] private int lowerLimit = 10;
    [SerializeField] private int higherLimit = 40;
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
            randomnumber = Random.Range(lowerLimit,higherLimit);
            gateno.text = "+" + randomnumber.ToString();

           
          randomnumber+=1;
         
        }

        
    }

    
    void Update()
    {
        
    }


}
