using Terraria.ModLoader;
using System.Collections.Generic;

namespace terraclasses
{
    public class ClassContainer
    {
        static Dictionary<string, ClassContainer> ClassContainers = new Dictionary<string, ClassContainer>();
        static ClassBase DefaultClass = new ClassBase().SetInvalidClass();
        Dictionary<uint, ClassBase> ClassDictionary = new Dictionary<uint, ClassBase>();

        public static bool AddClassContainer(Mod mod, ClassContainer container)
        {
            if (ClassContainers.ContainsKey(mod.Name)) return false;
            container.LoadClasses();
            ClassContainers.Add(mod.Name, container);
            return true;
        }

        public static ClassBase GetClass(uint ID, string ModID = "")
        {
            if (ModID == "") ModID = terraclasses.GetModName;
            if (ClassContainers.ContainsKey(ModID))
            {
                if (ClassContainers[ModID].ClassDictionary.ContainsKey(ID))
                {
                    return ClassContainers[ModID].ClassDictionary[ID];
                }
            }
            return DefaultClass;
        }

        protected virtual void LoadClasses()
        {

        }

        protected bool AddClass(uint ID, ClassBase Class)
        {
            if (ClassDictionary.ContainsKey(ID)) return false;
            ClassDictionary.Add(ID, Class);
            Class.Initialize();
            return true;
        }
        
        internal void OnUnload()
        {
            ClassDictionary.Clear();
            ClassDictionary = null;
        }

        internal static void Unload()
        {
            foreach (string s in ClassContainers.Keys)
            {
                ClassContainers[s].OnUnload();
            }
            ClassContainers.Clear();
            ClassContainers = null;
            DefaultClass = null;
        }
    }
}