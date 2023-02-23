using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatDetectRats : MonoBehaviour
{
    [SerializeField]
    int nRatsToScare = 1;

    UCM.IAV.Movimiento.Llegada lleg;
    UCM.IAV.Movimiento.Separacion sep;

    [SerializeField]
    float timeToChange = 5;
    float lastTimeChanged = 0;

    // Start is called before the first frame update
    void Start()
    {
        lleg = GetComponent<UCM.IAV.Movimiento.Llegada>();
        sep = GetComponent<UCM.IAV.Movimiento.Separacion>();
    }

    // Update is called once per frame
    void Update()
    {
        lastTimeChanged += Time.deltaTime;
        var a = Physics.SphereCastAll(transform.position, 3, transform.forward);

        int i = 0;
        foreach (var obj in a)
        {
            if (obj.collider.gameObject.tag == "Rat")
            {
                i++;
                if (i >= nRatsToScare && lastTimeChanged > timeToChange)
                {
                    //lleg.enabled = false;
                    sep.objetivo = obj.collider.gameObject;
                    sep.enabled = true;
                    lastTimeChanged = 0;
                    return;
                }
            }
        }
        if (lastTimeChanged > timeToChange)
        {
            //lleg.enabled = true;
            sep.enabled = false;
            lastTimeChanged = 0;
        }
    }
}
