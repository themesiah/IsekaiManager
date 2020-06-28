using UnityEngine;
using System.Collections;
using Isekai.Interactions;
using UnityEngine.Events;

namespace Isekai.Buildings
{
    public class TavernCommand : BuildingInteractionCommand
    {
        private float timer = 0f;

        public TavernCommand(InteractableTavern interactableTavern, Transform enterPoint, Transform centerPoint, Isekai.UI.BuildingInteractionProgressUI progressUI, BuildingInteractionData buildingInteractionData) : base(interactableTavern, enterPoint, centerPoint, progressUI, buildingInteractionData)
        {

        }

        protected override IEnumerator DoBuildingProcess()
        {
            while (timer < buildingInteractionData.Duration)
            {
                timer += Time.deltaTime;
                progressUI.SetProgress(timer / buildingInteractionData.Duration);
                yield return ManagePause();
                yield return null;
            }
            buildingInteractionData.ResourceData.ResourceRef.SetValue(buildingInteractionData.ResourceData.ResourceRef.GetValue() + buildingInteractionData.Reward);
        }
    }
}