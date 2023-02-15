/*    
   Copyright (C) 2020-2023 Federico Peinado
   http://www.federicopeinado.com

   Este fichero forma parte del material de la asignatura Inteligencia Artificial para Videojuegos.
   Esta asignatura se imparte en la Facultad de Informática de la Universidad Complutense de Madrid (España).

   Autor: Federico Peinado 
   Contacto: email@federicopeinado.com
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UCM.IAV.Movimiento
{
    /// <summary>
    /// Clase para modelar el comportamiento de WANDER a otro agente
    /// </summary>
    public class Merodear : ComportamientoAgente
    {
        [SerializeField]
        float tiempoMaximo = 2.0f;

        [SerializeField]
        float tiempoMinimo = 1.0f;

        float t = 3.0f;
        float actualT = 2.0f;

        Direccion lastDir = new Direccion();

        public override Direccion GetDireccion(){
            
            //float rndtime = random float entre el tiempo maximo y el minimo
            //var randomdir = transform entre 0 y 1
            //float 

            // IMPLEMENTAR merodear
            //if(t>)
            //var dir = this.transform.position*randomdir;

            //si ha llegado el tiempo a superar el rndtime, reseteamos el tiempo y cambiamos la direccion



            return new Direccion();
        }

    }
}
