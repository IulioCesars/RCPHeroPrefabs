﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Prefab.Definitions
{
    public class Configuration
    {
        public static bool FuerzaContraccionValida(int value)
        {
            //Debug.Log($"Fuerza Contraccion: {value}");

            return value >= 30 && value <= 60;
        }

        public static bool FuerzaRespiracionValida(int value)
        {
            //Debug.Log($"Fuerza Respiracion: {value}");

            return value >= 30;
        }

        public static bool RCPSuccess()
        {
            return true;
        }


        public static int LimitContractions => 30;
        public static int LimitBreathing => 2;
    }
}
