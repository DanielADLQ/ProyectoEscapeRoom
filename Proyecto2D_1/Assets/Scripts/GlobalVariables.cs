using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    ArrayList variables = new ArrayList();
    ArrayList values = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        //0 false
        //1 true
        variables.Add("gotKey1");
        values.Add("0");
        variables.Add("gotKey2");
        values.Add("0");
        variables.Add("taponEncontrado");
        values.Add("0");
        //variables.Add("fridgeState");
        //values.Add("0");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeVariable(string nomVar, string valueVar)
    {
        this.values[variables.IndexOf(nomVar)] = valueVar;
    }

    public string getValue(string nomVar)
    {
        return this.values[variables.IndexOf(nomVar)].ToString();
    } 
}
