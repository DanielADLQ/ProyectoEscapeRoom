using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretCodeGenerator : MonoBehaviour
{
    public string n1;
    public string n2;
    public string n3;
    public string n4;
    public string numCompleto;
    
    // Start is called before the first frame update
    void Start()
    {
        n1 = Random.Range(0,10).ToString();
        n2 = Random.Range(0,10).ToString();
        n3 = Random.Range(0,10).ToString();
        n4 = Random.Range(0,10).ToString();
        //Se genera un numero de 4 cifras con las obtenidas
        numCompleto = n1+n2+n3+n4;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
