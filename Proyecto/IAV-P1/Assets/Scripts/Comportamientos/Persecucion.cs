using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class Persecucion : Llegada
    {
        protected override Vector3 GetObjective()
        {
            var prediccion = objetivo.GetComponent<Rigidbody>().velocity * timeToTarget;
            return objetivo.transform.position + prediccion;
        }
    }
}
