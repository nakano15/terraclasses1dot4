using Terraria;
using Terraria.GameContent;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace terraclasses1dot4
{
    public class SkillBase
    {
        protected static SkillData _Data;
        public static SkillData Data => _Data;

        public virtual string Name => "Unknown";
        public virtual string Description => "";
        public virtual SkillTypes SkillType => SkillTypes.Passive;
        SkillAttribute[] _Attributes = new SkillAttribute[0]; //Handles each benefit the skill will get.
        protected virtual SkillAttribute[] SetSkillAttributes => new SkillAttribute[0];
        public SkillAttribute[] GetSkillAttributes => _Attributes;
        public virtual SkillData GetSkillData => new SkillData();
        public virtual bool EffectiveAtLevel0 => false;
        public virtual int Cooldown => 0;
        bool _Invalid;
        public bool IsInvalid => _Invalid;

        public virtual void GetSkillIcon(out Texture2D Texture, out Rectangle Rect)
        {
            Texture = terraclasses.SkillIconsTexture.Value;
            Rect = new Rectangle(0, 0, 48, 48);
        }

        internal SkillBase SetInvalid()
        {
            _Invalid = true;
            return this;
        }

        internal void Initialize()
        {
            _Attributes = SetSkillAttributes;
        }

        #region Skill Data related
        internal static void ChangeSkillData(SkillData New)
        {
            _Data = New;
        }
        #endregion

        #region Skill Functions
        public virtual void OnStartUse()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void OnStepChange()
        {

        }

        public virtual void OnEndUse(bool Abrupt)
        {

        }

        public virtual void UpdateAnimation()
        {

        }

        public virtual void UpdateStatus()
        {

        }

        public virtual void UpdateItemUse(bool JustUsed)
        {

        }

        public virtual bool BeforeShooting(Item weapon, ref int type, ref int damage, ref float knockback, ref Microsoft.Xna.Framework.Vector2 Position, ref Microsoft.Xna.Framework.Vector2 Velocity)
        {
            return true;
        }

        public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {

        }

        public virtual void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {

        }

        public virtual void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {

        }

        public virtual void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
        {

        }

        public virtual void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {

        }

        public virtual void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
        {

        }

        public virtual void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {

        }

        public virtual void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {

        }

        public virtual void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
        {

        }

        public virtual void DrawBehindPlayer(ref PlayerDrawSet drawInfo)
        {

        }

        public virtual void DrawInFrontOfPlayer(ref PlayerDrawSet drawInfo)
        {

        }

        public virtual void DrawInFrontOfEverything()
        {

        }
        #endregion

        #region Handy Methods
        public int GetTime => _Data.GetTime;

        public int GetTotalTime => _Data.GetTotalTime;

        public int GetFrameFromTime(int Seconds, int Minutes = 0, int Hours = 0)
        {
            return Seconds * 60 + Minutes * 3600 + Hours * 216000;
        }

        public Texture2D GetProjectileTexture(int ID)
        {
            return TextureAssets.Projectile[ID].Value;
        }

        public Texture2D GetNPCTexture(int ID)
        {
            return TextureAssets.Npc[ID].Value;
        }

        public Texture2D GetItemTexture(int ID)
        {
            return TextureAssets.Item[ID].Value;
        }

        public Texture2D GetGoreTexture(int ID)
        {
            return TextureAssets.Gore[ID].Value;
        }

        public int GetBestDamage(Player player, DamageClass damageClass)
        {
            int HighestDamage = 0;
            for (int i = 0; i < 10; i++)
            {
                if (player.inventory[i].type > 0 && player.inventory[i].CountsAsClass(damageClass))
                {
                    int Damage = (int)(player.inventory[i].damage * (60f / player.inventory[i].useTime));
                    if (Damage > HighestDamage)
                        HighestDamage = Damage;
                }
            }
            return HighestDamage;
        }

        protected float GetAttributeValue(byte AttributeIndex)
        {
            return _Data.GetSkillAttributeValue(AttributeIndex);
        }

        protected float GetAttributeLevel(byte AttributeIndex)
        {
            return _Data.GetAttributeLevel(AttributeIndex);
        }

        protected void EndUse()
        {
            _Data.EndUse();
        }

        protected Player GetCaster => _Data.GetCaster;
        #endregion
    }
}