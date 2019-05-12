using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Prefab.ArduinoReader.Scripts
{
    public class HapticoModelInput
    {
        public int InclinacionCabeza { get; set; } = 0;
        public int RespiracionPresionAire { get; set; } = 0;
        public int FuerzaContraccion { get; set; } = 0;

        public static bool TryParse(string source, out HapticoModelInput hapticoModel)
        {
            var values = source.Split('|');
            hapticoModel = null;

            if (values.Length == 4)
            {
                hapticoModel = new HapticoModelInput()
                {
                    InclinacionCabeza = int.Parse(values[0]),
                    RespiracionPresionAire = int.Parse(values[1]),
                    FuerzaContraccion = int.Parse(values[2]) + int.Parse(values[3]),
                };
            }

            return hapticoModel != null;
        }

        public override string ToString()
        {
            return string.Join("|", InclinacionCabeza, RespiracionPresionAire, FuerzaContraccion);
        }
    }

    public class HapticoModelOutput
    {
        /*
         0 No vomito presion normal (Final)
         1 No Vomito presion baja (Durante)
         2 Vomita y presion normal (Salvaste y boita)
         3 Vomita y presion baja (Vomita y se muere)
         4 Apaga todo
             */

        public bool DispararBombearSangre { get; set; } = false;
        public bool DispararVomito { get; set; } = false;

        public override string ToString() => $"{DispararBombearSangre}|{DispararVomito}";
    }
}