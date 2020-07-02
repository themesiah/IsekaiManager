using UnityEngine;
using Isekai.Buildings;

namespace Isekai.Interactions
{
    public abstract class BuildingResourceInteractionCommand : BuildingInteractionCommand
    {
        protected BuildingResourceInteractionData resourceInteractionData;

        public BuildingResourceInteractionCommand(InteractableBuilding interactableBuilding, Transform enterPoint, Transform centerPoint, Isekai.UI.BuildingInteractionProgressUI progressUI, BuildingInteractionDataTemplate buildingInteractionData)
            : base(interactableBuilding, enterPoint, centerPoint, progressUI, buildingInteractionData)
        {
            resourceInteractionData = (BuildingResourceInteractionData)buildingInteractionData;
        }

        protected override void OnStartBuildingProcess()
        {
            base.OnStartBuildingProcess();
            StartProgressUI();            
        }

        protected override void OnEndBuildingProcess()
        {
            base.OnEndBuildingProcess();
            FinishProgressUI();
        }

        protected override void OnCancel()
        {
            base.OnCancel();
            FinishProgressUI();
        }

        private void StartProgressUI()
        {
            BuildingResourceInteractionData interactionData = (BuildingResourceInteractionData)buildingInteractionData;
            progressUI.InitInteraction(interactionData.ResourceData);
        }

        private void FinishProgressUI()
        {
            progressUI.Finish();
        }
    }
}