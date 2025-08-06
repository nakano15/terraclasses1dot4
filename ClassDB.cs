using terraclasses1dot4.Classes.Starter;
using terraclasses1dot4.Classes.Aspect;
using terraclasses1dot4.Classes.Primary;
using terraclasses1dot4.Classes.Special;

namespace terraclasses1dot4
{
    public class ClassDB : ClassContainer
    {
        public const uint Terrarian = 0,
            Fighter = 1,
            Cerberus = 9;

        protected override void LoadClasses()
        {
            AddClass(Terrarian, new TerrarianClass());
            AddClass(Fighter, new FighterClass());
            AddClass(Cerberus, new CerberusClass());
        }
    }
}