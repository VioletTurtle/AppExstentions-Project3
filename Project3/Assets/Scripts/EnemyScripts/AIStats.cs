using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStats : MonoBehaviour
{
    public float health = 100f;
    public string ScriptName;
    LinchAI LAI;
    GolemAI GAI;
    // Start is called before the first frame update
    void Start()
    {
        if(ScriptName == "LinchAI")
        {
            LAI = this.GetComponent<LinchAI>();
        }
        else
        {
            GAI = this.GetComponent<GolemAI>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(ScriptName == "LinchAI")
        {
            LAI.health = health;
        }
        else
        {
            GAI.health = health;
        }
    }
}
