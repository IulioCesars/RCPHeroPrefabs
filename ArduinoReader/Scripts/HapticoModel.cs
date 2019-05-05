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

            if (values.Length == 3)
            {
                hapticoModel = new HapticoModelInput()
                {
                    InclinacionCabeza = int.Parse(values[0]),
                    RespiracionPresionAire = int.Parse(values[1]),
                    FuerzaContraccion = int.Parse(values[2]),
                };
            }

            return hapticoModel != null;
        }
    }

    public class HapticoModelOutput
    {
        public bool DispararBombearSangre { get; set; } = false;
        public bool DispararVomito { get; set; } = false;

        public override string ToString() => $"{DispararBombearSangre}|{DispararVomito}";
    }
}