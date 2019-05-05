using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using Assets.Prefab.ArduinoReader.Scripts;

namespace Assets.Prefab.ArduinoReader.Scripts
{
    public class ArduinoController : MonoBehaviour
    {
        [Tooltip("Puerto serial desde el cual se conectara")]
        public int COMPort = 2;
        [Tooltip("Baudratet")]
        public int Baudrate = 9600;

        private SerialPort SerialPort { get; set; }

        //<CR><LF> Char 13 y Char 10 es lo que se espera para cortar la linea
        void Start()
        {
            SerialPort = new SerialPort($"COM{COMPort}", Baudrate)
            { ReadTimeout = 50 };

            SerialPort.Open();
            
            Debug.Log($"Iniciando {name}, en el puerto {SerialPort.PortName}");
            StartCoroutine(ReadSerialPortAsync());
        }

        void Update()
        { }

        IEnumerator ReadSerialPortAsync()
        {
            string data;

            while (SerialPort.IsOpen)
            {
                try
                { data = SerialPort.ReadLine(); }
                catch (Exception)
                { data = null; }

                if (string.IsNullOrWhiteSpace(data))
                { yield return new WaitForSeconds(0.01f); }
                else
                {
                    if (HapticoModelInput.TryParse(data, out HapticoModelInput hapticoModel))
                    { HapticoEvents.ProcessEvents(hapticoModel); }
                    else
                    { Debug.Log($"No fue posible convertir el string '{data}' en HapticoModel"); }

                    yield return null;
                }
            }
        }

        public void WriteSerialPort(HapticoModelOutput hapticoModel)
        {
            if (SerialPort != null)
            { SerialPort.WriteLine(hapticoModel.ToString()); }
        }

        public void WriteSerialPort(string data)
        {
            if (SerialPort != null)
            { SerialPort.WriteLine(data); }
        }

        public void Close()
        {
            if (SerialPort.IsOpen)
            { SerialPort.Close(); }
        }
    }
}
