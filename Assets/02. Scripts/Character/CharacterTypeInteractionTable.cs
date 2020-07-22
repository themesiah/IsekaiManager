using System.Collections.Generic;
using Isekai.Interactions;

namespace Isekai.Characters
{
    public static class CharacterTypeInteractionTable
    {
        private static Dictionary<CharacterTypeEnum, Dictionary<InteractionType, bool>> interactionTable = new Dictionary<CharacterTypeEnum, Dictionary<InteractionType, bool>> {
            { CharacterTypeEnum.Hero, new Dictionary<InteractionType, bool> { { InteractionType.Building, true }, { InteractionType.Enemy, true }, { InteractionType.TalkToNPC, true }} },
            { CharacterTypeEnum.Goddess, new Dictionary<InteractionType, bool> { { InteractionType.Building, true }, { InteractionType.TalkToNPC, true }} },
            { CharacterTypeEnum.Enemy, new Dictionary<InteractionType, bool> { { InteractionType.AttackBuilding, true }, { InteractionType.AttackHero, true }, { InteractionType.GenerateChaos, true }} },
            { CharacterTypeEnum.PassiveNPC, new Dictionary<InteractionType, bool> { { InteractionType.Hide, true } } },
            { CharacterTypeEnum.AttackerNPC, new Dictionary<InteractionType, bool> { { InteractionType.Enemy, true }} },
        };

        public static bool GetInteractionPermission(CharacterTypeEnum charType, InteractionType interactionType)
        {
            if (interactionTable.ContainsKey(charType))
            {
                if (interactionTable[charType].ContainsKey(interactionType))
                {
                    return interactionTable[charType][interactionType];
                }
            }
            return false;
        }
    }
}