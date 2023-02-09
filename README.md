# Práctica 1 (IAV) - Grupo 13

## Autores
- Javier Vaillegas Montelongo ([Yavi123](https://github.com/Yavi123))
- Gonzalo Fernández Moreno ([GonzaloFdezMoreno](https://github.com/GonzaloFdezMoreno))
- Enrique Juan Gamboa ([ivo_hr](https://github.com/ivo-hr))

## Propuesta
Esta práctica consiste en realizar un prototipo de IA dentro de un pequeño entorno virtual con obstáculos y una entidad jugador, que será **el flautista**. En este entorno se encuentran, de manera *dinámica* **un perro** y un grupo de **ratas**.

La idea principal de la práctica es conseguir los siguientes comportamientos:

 1. **El perro** deberá de *perseguir al flautista* de manera que prediga el movimiento del flautista, y quedarse a una distancia de él.
 2. **El perro** ha de huir si *hay tres o más ratas cerca suya*.
 3. **Las ratas** han de moverse erráticamente cuando *el flautista no toca la flauta*
 4. **Las ratas**, al escuchar la flauta, tienen que *seguir al flautista*, y *ordenarse entre ellas en formación*. Tienen que tener separación entre ellas y quedarse a una distancia del flautista.


## Punto de partida
Se parte de un proyecto base de Unity proporcionado por el profesor aquí:
https://github.com/Narratech/IAV-P1

Se dispone de una escena en Unity que consta de un entorno por el cual el jugador podrá moverse; además de una serie de obstáculos, como arboles o casas. También contiene las entidades del perro, las ratas y el jugador; el perro no hace nada 
al igual que las ratas, pero estas últimas puedes aumentar o reducir su nº con las teclas O y P respectivamente (cuando se le añadan los scripts a estas entidades se moveran automaticamente siguiendo al jugador o merodeando por el entorno); el jugador, sin embargo, podrá moverse con las teclas WASD y tocar la flauta con el espacio. 

Se puede cambiar la posicion de la cámara con N, el ratio (FPS) con F y recargar la escena con R.
Hay scripts para darles los comportamientos necesarios a las entidades (los cuales hay que completar).

## Diseño de la solución

La manera en la que vamos a afrontar esta práctica es la siguiente:

 - Completaremos **el script de seguimiento** que se le aplicará tanto al perro como a las ratas, de manera que puedan *seguir o perseguir al objetivo*.

 
 - También completaremos **el script** que lé de a las ratas un **movimiento errático** para cuando no estén bajo el control del flautista.
 
 - Finalmente completaremos **el script de huida** del perro para que *se aleje de las ratas* que tenga en su proximidada.

 - También vamos a crear **dos scripts** que hagan de **máquina de estados**: 
	 - uno de ellos para las ratas, que decidirá si están *bajo la hipnosis del flautista o no* (y aplicará el código de seguimiento o de moviemiento errático). También cogerá el objetivo a seguir para cada rata, que puede ser otra rata o el flautista. El objetivo con esto es que acaben ordenándose en fila india.
	 - y el otro, para el perro, que aplicará el código de seguimiento o el de huida (dependiendo de *si tiene mas de dos ratas cerca o no*)

Pseudocódigo del algoritmo de seguimiento:
```
class Follow:
    character: Kinematic
    target: Kinematic

    maxAcc: float
    maxSp: float

    # Distancia mínima entre la entidad y el objetivo
    minDist: float

    # Distancia a la que la entidad va parando
    slowDist: float

    # Tiempo en llegar a la velocidad pedida
    timToVel: float

    function gatAngl() -> AnglOut:
        result = new AnglOut()

        # Dirección hacia el objetivo
        dir = target.position - character.position
        dist = dir.length()

        # Si ya llegamos, paramos
        if dist < minDist:
            return null

        # Si no estamos a distancia de frenar, vamos a tope
        if dist > slowDist:
            targetSp = maxSp
        # si no, a calcular
        else:
            targetSp = maxSp * dist / slowDist

         targetVel = dir
        targetVel.normalize()
        targetVel *= targetSp

        # Aceleración para llegar a la velocidad que queremos
        result.linear = (targetVel - character.velocity)/timeToVel

        # Si la aceleración es demasiada
        if result.linear.length() > maxAcc:
            result.linear.normalize()
            result.linear *= maxAcc

        result.angular = 0
        return result
```

Pseudocódigo del algoritmo de huida:

    function AreRatsNearby(int maxNumOfRats) -> bool 
	    nearbyObjects = Physics.SphereCast(...)
	    numOfRats = 0 
	    foreach object in nearbyObjects: 
		    if object is a rat: 
			    numOfRats++ 
			    if numOfRats >= maxNumOfRats: 
				    return true 
			    return false

Diagrama de la máquina de estado del perro

![Máquina de estados del perro](https://cdn.discordapp.com/attachments/1072955659827556384/1072964346335989841/image.png)
Diagrama de la máquina de estado de las ratas
![Máquina de estado de las ratas](https://cdn.discordapp.com/attachments/1072955659827556384/1072965194038390824/image.png)


## Pruebas y métricas

- [Vídeo con la batería de pruebas](https://www.youtube.com/watch?v=dQw4w9WgXcQ)

## Ampliaciones

TBD

## Producción

Las tareas se han realizado y el esfuerzo ha sido repartido entre los autores.

| Estado  |  Tarea  |  Fecha  |  
|:-:|:--|:-:|
| ✔ | Diseño: Primer borrador | 08/02/2023 |
|  | Característica A: Mundo virtual | --/--/---- |
|  | Característica B: Perro persigue al flautista| --/--/---- |
|  | Característica C: Perro huye de ratas| --/--/---- |
|  | Característica D: Ratas merodeadoras| --/--/---- |
|  | Característica E: Ratas hipnotizadas| --/--/---- |
|   | ... | |
|  | OPCIONAL |  |
|  | Generador pseudoaleatorio | --/--/---- |
|  | Varios flautistas/generadores de ratas| --/--/---- |
|  | Curarle la ceguera al perro| --/--/---- |
|  | Perro y ratas evitan obstáculos| --/--/---- |

## Referencias

Los recursos de terceros utilizados son de uso público.

- *AI for Games*, Ian Millington.
- [Kaykit Medieval Builder Pack](https://kaylousberg.itch.io/kaykit-medieval-builder-pack)
- [Kaykit Dungeon](https://kaylousberg.itch.io/kaykit-dungeon)
- [Kaykit Animations](https://kaylousberg.itch.io/kaykit-animations)
