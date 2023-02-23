/*    
   Copyright (C) 2020-2023 Federico Peinado
   http://www.federicopeinado.com

   Este fichero forma parte del material de la asignatura Inteligencia Artificial para Videojuegos.
   Esta asignatura se imparte en la Facultad de Informática de la Universidad Complutense de Madrid (España).

   Autor: Federico Peinado 
   Contacto: email@federicopeinado.com
*/

using UnityEngine;
namespace UCM.IAV.Movimiento
{

    /// <summary>
    /// Clase para modelar el comportamiento de HUIR a otro agente
    /// </summary>
    public class Huir : Llegada
    {
        /// <summary>
        /// Obtiene la dirección
        /// </summary>
        /// <returns></returns>
        public override Direccion GetDireccion()
        {
            if (nRatsDetected < nRatsToScare) this.peso = 0;
            else this.peso = 1;


            Debug.Log("HUIR active");
            // IMPLEMENTAR HUIR
            var resultado = new Direccion();

            float targetSpeed = maxSpeed;
            Vector3 dir = this.transform.position - lastRatPos;
            var dist = dir.magnitude;


            if (dist < rRalentizado)
            {
                targetSpeed = maxSpeed * dist / rRalentizado;
            }

            var targetVelocity = dir.normalized;

            targetVelocity *= targetSpeed;

            var thisSpeed = this.GetComponent<Rigidbody>().velocity;

            resultado.lineal = targetVelocity - thisSpeed;

            timeToTarget = dist / maxSpeed;

            //resultado.lineal /= timeToTarget;

            if (resultado.lineal.magnitude > maxAcceleration)
            {
                resultado.lineal.Normalize();
                resultado.lineal *= maxAcceleration;
            }

            resultado.angular = 0;

            

            return resultado;
        }
    }
}
