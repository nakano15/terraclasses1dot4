using Terraria;
using Terraria.GameContent;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace terraclasses
{
    public class SkillBase
    {
        public virtual string Name => "Unknown";
        public virtual string Description => "";
        public virtual SkillTypes SkillType => SkillTypes.Passive;
        SkillAttribute[] _Attributes = new SkillAttribute[0]; //Handles each benefit the skill will get.
        protected virtual SkillAttribute[] SetSkillAttributes => new SkillAttribute[0];
        public SkillAttribute[] GetSkillAttributes => _Attributes;
        public virtual SkillData GetSkillData => new SkillData();
        public virtual bool EffectiveAtLevel0 => false;
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

        #region Skill Functions
        public virtual void OnStartUse(SkillData Data)
        {

        }

        public virtual void Update(SkillData Data)
        {

        }

        public virtual void OnStepChange(SkillData Data)
        {

        }

        public virtual void OnEndUse(SkillData Data, bool Abrupt)
        {
            
        }

        public virtual void UpdateAnimation(SkillData Data)
        {

        }

        public virtual void UpdateStatus(SkillData Data)
        {

        }

        public virtual void UpdateItemUse(SkillData Data, bool JustUsed)
        {

        }

        public virtual bool BeforeShooting(SkillData Data, Item weapon, ref int type, ref int damage, ref float knockback, ref Microsoft.Xna.Framework.Vector2 Position, ref Microsoft.Xna.Framework.Vector2 Velocity)
        {
            return true;
        }

        public virtual void OnHitNPC(SkillData Data, NPC target, NPC.HitInfo hit, int damageDone)
        {

        }

        public virtual void OnHitNPCWithProj(SkillData data, Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {

        }

        public virtual void OnHitByNPC(SkillData data, NPC npc, Player.HurtInfo hurtInfo)
        {

        }

        public virtual void OnHitByProjectile(SkillData data, Projectile proj, Player.HurtInfo hurtInfo)
        {

        }

        public virtual void ModifyHitNPC(SkillData data, NPC target, ref NPC.HitModifiers modifiers)
        {

        }

        public virtual void ModifyHitNPCWithItem(SkillData data, Item item, NPC target, ref NPC.HitModifiers modifiers)
        {

        }

        public virtual void ModifyHitNPCWithProj(SkillData data, Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {

        }

        public virtual void ModifyHitByNPC(SkillData data, NPC npc, ref Player.HurtModifiers modifiers)
        {

        }

        public virtual void ModifyHitByProjectile(SkillData data, Projectile proj, ref Player.HurtModifiers modifiers)
        {

        }

        public virtual void DrawBehindPlayer(SkillData data, ref PlayerDrawSet drawInfo)
        {

        }

        public virtual void DrawInFrontOfPlayer(SkillData data, ref PlayerDrawSet drawInfo)
        {

        }

        public virtual void DrawInFrontOfEverything(SkillData data)
        {

        }
        #endregion

        #region Handy Methods
        public int GetTime(int Seconds, int Minutes = 0, int Hours = 0)
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
        #endregion
    }
}