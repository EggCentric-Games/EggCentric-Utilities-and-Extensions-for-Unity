using EggCentric.Commands;
using UnityEngine;

namespace EggCentric.Abilities
{
    public abstract class AbilityConfig<TData> : ScriptableObject where TData : AbilityData
    {
        protected TData data;
    }

    public abstract class AbilityConfig<TData, TAbility> : AbilityConfig<TData> where TData : AbilityData where TAbility : AbilityInstance<TData>
    {
        public TAbility CreateInstance() => AddModifiers(Create(GetCommand()));

        protected TAbility AddModifiers(TAbility ability)
        {
            foreach (var abilityModifier in data.Modifiers)
                ability.AddModifier(abilityModifier.Create());

            return ability;
        }

        protected abstract TAbility Create(ICommand performableCommand);
        protected abstract ICommand GetCommand();
    }

    public abstract class AbilityConfig<TData, TContext, TAbility> : AbilityConfig<TData> where TData : AbilityData where TAbility : AbilityInstance<TData, TContext>
    {
        public TAbility CreateInstance(TContext context) => AddModifiers(Create(context, GetCommand(context)));

        protected TAbility AddModifiers(TAbility ability)
        {
            foreach (var abilityModifier in data.Modifiers)
                ability.AddModifier(abilityModifier.Create());

            return ability;
        }

        protected abstract TAbility Create(TContext context, ICommand performableCommand);
        protected abstract ICommand GetCommand(TContext context);
    }
}