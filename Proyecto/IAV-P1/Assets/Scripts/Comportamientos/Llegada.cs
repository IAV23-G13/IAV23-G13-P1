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

        //Rat number
        [SerializeField]
        private int nRatsDetected = 0;
        [SerializeField]
        private int nRatsToScare = 3;

        //Last rat position to escape
        private Vector3 lastRatPos;
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

            if (nRatsDetected < nRatsToScare && dist < distancia)
            {
                return resultado;
            }

            if (dist < rRalentizado)
            {
                targetSpeed = maxSpeed * dist / rRalentizado;
            }

            var targetVelocity = dir.normalized;

            targetVelocity *= targetSpeed;

            var thisSpeed = this.GetComponent<Rigidbody>().velocity;

            resultado.lineal = targetVelocity - thisSpeed;

            timeToTarget = dist / thisSpeed.magnitude;

            if (timeToTarget > 10)
            {
                timeToTarget = 10;
            }

            //resultado.lineal /= timeToTarget;

            if (resultado.lineal.magnitude > maxAcceleration)
            {
                resultado.lineal.Normalize();
                resultado.lineal *= maxAcceleration;
            }

            resultado.angular = 0;
            return resultado;

        }   



        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Rat")){
                nRatsDetected++;
                lastRatPos = other.gameObject.transform.position;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Rat")){
                nRatsDetected--;
            }
        }

        protected virtual Vector3 GetObjective()
        {
            if (nRatsDetected < nRatsToScare) return objetivo.transform.position;;
            else return lastRatPos;
        }
    }
}
