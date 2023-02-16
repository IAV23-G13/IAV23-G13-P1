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
        private int ratNumber = 0;
        // El radio en el que se empieza a ralentizarse

        //Tiene que acceder a la lista de entidades para que el objetivo a seguir sea la ultima rata en vez del avatar
        public float rRalentizado;

        public float fRalentizado;

        public int distancia = 7;

        // El tiempo en el que conseguir la aceleracion objetivo
        float timeToTarget = 0.1f;
        public override Direccion GetDireccion()
        {
            var resultado = new Direccion();

            float targetSpeed = maxSpeed;

            var dir = Vector3.zero;
            if (ratNumber <= 2) dir = objetivo.transform.position - this.transform.position;
            else dir = this.transform.position - lastRatPos;
            var dist = dir.magnitude;

            if (ratNumber <= 2 && dist < distancia)
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
            return resultado;

        }   

        private Vector3 lastRatPos;

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Rat")){
                ratNumber++;
                lastRatPos = other.gameObject.transform.position;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Rat")){
                ratNumber--;
            }
        }


    }
}
