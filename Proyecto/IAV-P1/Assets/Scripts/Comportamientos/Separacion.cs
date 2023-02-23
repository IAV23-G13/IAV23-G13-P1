/*    
   Copyright (C) 2020-2023 Federico Peinado
   http://www.federicopeinado.com

   Este fichero forma parte del material de la asignatura Inteligencia Artificial para Videojuegos.
   Esta asignatura se imparte en la Facultad de Informática de la Universidad Complutense de Madrid (España).

   Autor: Federico Peinado 
   Contacto: email@federicopeinado.com
*/
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace UCM.IAV.Movimiento
{
    public class Separacion : ComportamientoAgente
    {
        /// <summary>
        /// Separa al agente
        /// </summary>
        /// <returns></returns>

        // Entidades potenciales de las que huir
        public GameObject targEmpty;

        // Umbral en el que se activa
        [SerializeField]
        float umbral;

        // Coeficiente de reducción de la fuerza de repulsión
        [SerializeField]
        float decayCoefficient;

        [SerializeField]
        private int nRatsDetected = 0;

        private Vector3 lastRatPos;

        private GameObject[] targets;


        public override Direccion GetDireccion()
        {
            // IMPLEMENTAR separación

            var dirLLegada = base.GetDireccion();
            dirLLegada.lineal = -dirLLegada.lineal * 5;
            dirLLegada.angular = -dirLLegada.angular;
            return dirLLegada;
           
        }

        //void OnTriggerEnter(Collider other)
        //{
        //    if (other.gameObject.CompareTag("Rat"))
        //    {
        //        nRatsDetected++;
        //        lastRatPos = other.gameObject.transform.position;
        //        Debug.Log("pillaRata");
        //    }
        //}

        //void OnTriggerExit(Collider other)
        //{
        //    if (other.gameObject.CompareTag("Rat"))
        //    {
        //        nRatsDetected--;
        //    }
        //}
    }
}