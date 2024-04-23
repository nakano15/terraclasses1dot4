using terraclasses.Classes.Starter;
using terraclasses.Classes.Aspect;
using terraclasses.Classes.Primary;
using terraclasses.Classes.Special;

namespace terraclasses
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