using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class Persecucion : Llegada
    {
        Rigidbody objectiveRB;
        private void Start()
        {
            objectiveRB = objetivo.GetComponent<Rigidbody>();
        }

        protected override Vector3 GetObjective()
        {
            Debug.Log(objectiveRB.velocity);

            var prediccion = objectiveRB.velocity * timeToTarget;
            return objetivo.transform.position + prediccion;
        }
    }
}
