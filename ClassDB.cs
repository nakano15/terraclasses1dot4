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
            Archer = 2,
            Mage = 3,
            Merchant = 4,
            Thief = 5,
            Cleric = 6,
            Cerberus = 9000;

        protected override void LoadClasses()
        {
            AddClass(Terrarian, new TerrarianClass());
            AddClass(Fighter, new FighterClass());
            AddClass(Archer, new ArcherClass());
            AddClass(Mage, new MageClass());
            AddClass(Merchant, new MerchantClass());
            AddClass(Thief, new ThiefClass());
            AddClass(Cleric, new ClericClass());
            AddClass(Cerberus, new CerberusClass());
        }
    }
}