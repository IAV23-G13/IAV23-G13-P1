/*    
   Copyright (C) 2020-2023 Federico Peinado
   http://www.federicopeinado.com

   Este fichero forma parte del material de la asignatura Inteligencia Artificial para Videojuegos.
   Esta asignatura se imparte en la Facultad de Informática de la Universidad Complutense de Madrid (España).

   Autor: Federico Peinado 
   Contacto: email@federicopeinado.com
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

        float speed=0.5f;
        float maxAcceleration=1.0f;

        public bool cambioestado = false;


        Direccion lastDir = new Direccion();

        public override Direccion GetDireccion(){
            //System.Random rn = new System.Random();
            if (cambioestado)
            {
               
                cambioestado = false;
            }

            float rndtime = UnityEngine.Random.Range(tiempoMinimo, tiempoMaximo); ; //random float entre el tiempo maximo y el minimo

            if (t > rndtime)
            {

                float randomdirX = UnityEngine.Random.Range(-5, 5);
                float randomdirY = UnityEngine.Random.Range(-5, 5);
                float randomdirZ = UnityEngine.Random.Range(-5, 5);

                UnityEngine.Debug.Log(randomdirX);
                Direccion newdir = lastDir;

                newdir.lineal.x = this.transform.position.x + randomdirX;
                newdir.lineal.y = this.transform.position.y + randomdirY;
                newdir.lineal.z = this.transform.position.z + randomdirZ;
                t = 0;
                var dir = newdir.lineal - this.transform.position;

                //newdir.lineal = this.GetComponent<Rigidbody>().velocity;



                newdir.lineal = speed*dir.normalized;

                if (newdir.lineal.magnitude > maxAcceleration)
                {
                    newdir.lineal.Normalize();
                    newdir.lineal *= maxAcceleration;
                   
                }
                
                newdir.angular = 0;


                return newdir;
            }
            // IMPLEMENTAR merodear
            //if(t>)
            //var dir = this.transform.position*randomdir;

            //si ha llegado el tiempo a superar el rndtime, reseteamos el tiempo y cambiamos la direccion


            else
            {
                t += 0.01f;
                return new Direccion();
            }
            
        }

    }
}
