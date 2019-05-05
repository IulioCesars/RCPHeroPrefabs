using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Prefab.ArduinoReader.Scripts
{
    public static class HapticoEvents
    {
        static HapticoEvents()
        { eventDictionary = new Dictionary<string, Action<HapticoModelInput>>(); }

        private static Dictionary<string, Action<HapticoModelInput>> eventDictionary { get; set; }

        public static void Subscribe(Type t, Action<HapticoModelInput> @event) =>
            eventDictionary.Add(t.Name, @event);

        public static void Unsusbcribe(Type t) =>
            eventDictionary.Remove(t.Name);

        public static void ProcessEvents(HapticoModelInput hapticoModel)
        {
            foreach (var @event in eventDictionary.Values)
            { @event?.Invoke(hapticoModel); }
        }
    }
}
