using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatDetector : MonoBehaviour
{
    [SerializeField]
    int nRatsToScare = 2;
    [SerializeField]
    float radius = 2;

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
        var a = Physics.SphereCastAll(transform.position, radius, transform.forward);

        int i = 0;
        foreach (var obj in a)
        {
            if (obj.collider.gameObject.tag == "Rat")
            {
                i++;
                if (i >= nRatsToScare && lastTimeChanged > timeToChange)
                {
                    pers.peso = 0;
                    huir.objetivo = obj.collider.gameObject;
                    huir.peso = 1;
                    lastTimeChanged = 0;
                    return;
                }
            }
        }
        if (lastTimeChanged > timeToChange)
        {
            pers.peso = 1;
            huir.peso = 0;
            lastTimeChanged = 0;
        }
    }
}
