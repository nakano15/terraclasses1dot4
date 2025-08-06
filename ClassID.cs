using System.Diagnostics.CodeAnalysis;
using Terraria.ModLoader;

namespace terraclasses1dot4
{
    public struct ClassID
    {
        public uint ID;
        public string ModID;

        public ClassID()
        {
            ID = 0;
            ModID = terraclasses.GetModName;
        }

        public ClassID(uint NewID, Mod NewModID)
        {
            ID = NewID;
            ModID = NewModID.Name;
        }

        internal ClassID(uint NewID, string NewModID)
        {
            ID = NewID;
            ModID = NewModID;
        }

        public bool IsTheSame(uint OtherID, Mod OtherMod)
        {
            return ID == OtherID && ModID == OtherMod.Name;
        }

        public bool IsTheSame(uint OtherID, string OtherModID)
        {
            return ID == OtherID && ModID == OtherModID;
        }

        public bool IsTheSame(ClassID OtherID)
        {
            return ID == OtherID.ID && ModID == OtherID.ModID;
        }

        public override bool Equals([NotNullWhen(true)] object obj)
        {
            if (obj != null && obj is ClassID) return IsTheSame((ClassID)obj);
            return false;
        }
    }
}