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
    /// Clase para modelar el comportamiento de SEGUIR a otro agente
    /// </summary>
    public class LlegadaRatas : ComportamientoAgente
    {
        /// <summary>
        /// Obtiene la dirección
        /// </summary>
        /// <returns></returns>
        /// 

        [SerializeField]
        private int nRatsDetected = 0;

        private Vector3 lastRatPos;


        public float maxSpeed;
        public float maxAcceleration;

        // El radio para llegar al objetivo
        public float rObjetivo;

        // El radio en el que se empieza a ralentizarse

        //Tiene que acceder a la lista de entidades para que el objetivo a seguir sea la ultima rata en vez del avatar
        public float rRalentizado;

        public float fRalentizado;

        public int distancia = 7;

        TocarFlauta flau;
        Merodear mero;
        

        // El tiempo en el que conseguir la aceleracion objetivo
        float timeToTarget = 0.1f;
        public override Direccion GetDireccion()
        {
            var resultado = new Direccion();
            float targetSpeed = maxSpeed;
            flau = objetivo.GetComponent<TocarFlauta>();
            mero = this.GetComponent<Merodear>();

            //Hacer que el objetivo al que siguen sea la ultima rata de la lista a excepcion de la primera que sigue al jugador
            //usando la lista de ratas del TocarFlauta

            var dir = objetivo.transform.position - this.transform.position;
            //var dir = GetObjective() - this.transform.position;
            var dist = dir.magnitude;


            //Si esta mas lejos de la distancia y si la flauta esta tocandose seguirá al juagor
            //if (flau.isActive)
            if (!mero.enabled)
            {
                if (dist < distancia)
                {
                    return resultado;
                }

                if (dist < rRalentizado)
                {
                    targetSpeed = maxSpeed * dist / rRalentizado;
                }

                var targetVelocity = dir.normalized;

                targetVelocity *= targetSpeed;

                resultado.lineal = targetVelocity - this.GetComponent<Rigidbody>().velocity;

                resultado.lineal /= timeToTarget;

                if (resultado.lineal.magnitude > maxAcceleration)
                {
                    resultado.lineal.Normalize();
                    resultado.lineal *= maxAcceleration;
                }

                resultado.angular = 0;
            }
            return resultado;

        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Rat"))
            {
                nRatsDetected++;
                lastRatPos = other.gameObject.transform.position;
                Debug.Log("pillaRata");
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Rat"))
            {
                nRatsDetected--;
            }
        }

        protected virtual Vector3 GetObjective()
        {
            if (true)
                return objetivo.transform.position;
            else
                return lastRatPos;
        }


    }
}
