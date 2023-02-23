using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatDetector : MonoBehaviour
{
    [SerializeField]
    int nRatsToScare = 2;

    UCM.IAV.Movimiento.Persecucion pers;
    UCM.IAV.Movimiento.Huir huir;

    [SerializeField]
    float timeToChange = 5;
    float lastTimeChanged = 0;

    // Start is called before the first frame update
    void Start()
    {
        pers = GetComponent<UCM.IAV.Movimiento.Persecucion>();
        huir = GetComponent<UCM.IAV.Movimiento.Huir>();
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
                    pers.enabled = false;
                    huir.objetivo = obj.collider.gameObject;
                    huir.enabled = true;
                    lastTimeChanged = 0;
                    return;
                }
            }
        }
        if (lastTimeChanged > timeToChange)
        {
            pers.enabled = true;
            huir.enabled = false;
            lastTimeChanged = 0;
        }
    }
}
