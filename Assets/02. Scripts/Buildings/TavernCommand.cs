using UnityEngine;
using System.Collections;
using Isekai.Interactions;
using UnityEngine.Events;

namespace Isekai.Buildings
{
    public class TavernCommand : BuildingInteractionCommand
    {
        public TavernCommand(InteractableTavern interactableTavern, Transform enterPoint, Transform centerPoint) : base(interactableTavern, enterPoint, centerPoint)
        {

        }

        protected override IEnumerator DoBuildingProcess()
        {
            yield return new WaitForSeconds(2f);
        }
    }
}