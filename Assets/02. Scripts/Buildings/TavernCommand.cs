using UnityEngine;
using System.Collections;
using Isekai.Interactions;
using Isekai.Buildings;

namespace Isekai.Buildings
{
    public class TavernCommand : BuildingResourceInteractionCommand
    {
        private float timer = 0f;

        public TavernCommand(InteractableTavern interactableTavern, Transform enterPoint, Transform centerPoint, Isekai.UI.BuildingInteractionProgressUI progressUI, BuildingInteractionDataTemplate buildingInteractionData) : base(interactableTavern, enterPoint, centerPoint, progressUI, buildingInteractionData)
        {

        }

        protected override IEnumerator DoBuildingProcess()
        {
            while (timer < resourceInteractionData.Duration)
            {
                timer += Time.deltaTime;
                progressUI.SetProgress(timer / resourceInteractionData.Duration);
                yield return ManagePause();
                yield return null;
            }
            resourceInteractionData.ResourceData.ResourceRef.SetValue(resourceInteractionData.ResourceData.ResourceRef.GetValue() + resourceInteractionData.Reward);
        }
    }
}