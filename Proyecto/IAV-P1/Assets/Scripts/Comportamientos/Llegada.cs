/*    
   Copyright (C) 2020-2023 Federico Peinado
   http://www.federicopeinado.com

   Este fichero forma parte del material de la asignatura Inteligencia Artificial para Videojuegos.
   Esta asignatura se imparte en la Facultad de Inform�tica de la Universidad Complutense de Madrid (Espa�a).

   Autor: Federico Peinado 
   Contacto: email@federicopeinado.com
*/
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    /// <summary>
    /// Clase para modelar el comportamiento de SEGUIR a otro agente
    /// </summary>
    public class Llegada : ComportamientoAgente
    {
        /// <summary>
        /// Obtiene la direcci�n
        /// </summary>
        /// <returns></returns>

        public float maxSpeed;
        public float maxAcceleration;

        // El radio para llegar al objetivo
        public float rObjetivo;

        // El radio en el que se empieza a ralentizarse

        //Tiene que acceder a la lista de entidades para que el objetivo a seguir sea la ultima rata en vez del avatar
        public float rRalentizado;

        public float fRalentizado;

        public int distancia = 7;

        // El tiempo en el que conseguir la aceleracion objetivo
        protected float timeToTarget = 0.1f;
        public override Direccion GetDireccion()
        {
            var resultado = new Direccion();
            float targetSpeed = maxSpeed;

            Vector3 dir = GetObjective() - this.transform.position;
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

            if (resultado.lineal.magnitude > maxAcceleration)
            {
                resultado.lineal.Normalize();
                resultado.lineal *= maxAcceleration;
            }

            resultado.angular = 0;
            Debug.Log(this.gameObject.name + " "+ objetivo.name) ;
            
            return resultado;

        }

        protected virtual Vector3 GetObjective()
        {
            return objetivo.transform.position;
        }
    }
}
