using System.Collections;
using System.Collections.Generic;
using Isekai.Interactions;
using Isekai.Characters;
using UnityEngine;

namespace Isekai.Buildings
{
    public class InteractableTavern : InteractableBuilding
    {
        public override InteractionCommand[] GetUniqueCommandSet(int i)
        {
            return new InteractionCommand[] {
                new CharacterMoveCommand(enterPoint.position, positionMarkerPrefab),
                new TavernCommand(this, enterPoint, buildingCenterPoint, progressUI, buildingInteractionData[i])
            };
        }
    }
}