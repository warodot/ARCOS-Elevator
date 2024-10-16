using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashlicht : MonoBehaviour
{
   public Light flashlight;
   public Light PLight; // Asigna aquí tu fuente de luz

   public float mCarga;
    public float maxBatteryLife = 100f; // Duración máxima de la batería en segundos
    public float batteryDrainRate = 1f; // Tasa de agotamiento por segundo
    public float intensitiFL;
    public float FLCharge;
    public float currentBatteryLife;
    public AudioSource aS;
    public AudioClip sonido,flSFX,full;

    public float currentPitch;
    private float limit;

    bool FLActive,recargando;

    void Start()
    {
        flashlight.enabled = false;

        
        
        PLight.enabled = false;
        FLActive = false;
        // Inicializamos la vida de la batería al máximo
        currentBatteryLife = maxBatteryLife;

        intensitiFL =  flashlight.intensity;
    }

    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.F) )
        {
            FLActive = !FLActive;
           

            

            flashlight.enabled = FLActive;
            PLight.enabled = FLActive;
            aS.PlayOneShot(flSFX);
        }

        if(Input.GetKey(KeyCode.R))
        {
         recargando = true;
        }else
        {
            recargando = false;
        }

        if(recargando)
        {
           // aS.loop = true;
            //aS.clip = sonido;
           // aS.pitch += mCarga * Time.deltaTime;
          //  aS.Play();

            FLCharge += mCarga * Time.deltaTime;

            if(FLCharge > 25)
            {
                FLCharge = 25;
            }
           
        }
        if(Input.GetKeyUp(KeyCode.R))
        {
           // aS.loop = false;

           // aS.PlayOneShot(full);
           // aS.pitch = currentPitch;
            
            currentBatteryLife = currentBatteryLife+FLCharge;
            if(currentBatteryLife > maxBatteryLife)
             {
             currentBatteryLife = maxBatteryLife;
             }
            FLCharge = 0;
        }
        // Si la linterna está encendida y la batería tiene carga
        if (flashlight.enabled && currentBatteryLife > 0)
        {
            // Reducimos la vida de la batería según el tiempo que ha pasado
            currentBatteryLife -= batteryDrainRate * Time.deltaTime;

            // Ajustamos la intensidad de la linterna según la cantidad de batería restante
            flashlight.intensity = Mathf.Lerp(0, intensitiFL, currentBatteryLife / maxBatteryLife);

            // Si la batería se agota, apagamos la linterna
            if (currentBatteryLife <= 0)
            {
                flashlight.enabled = false;
            }
        }
    }
}
