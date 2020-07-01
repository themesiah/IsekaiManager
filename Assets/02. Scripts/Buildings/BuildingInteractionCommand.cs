using Isekai.Interactions;
using UnityEngine.Events;
using UnityEngine;
using System.Collections;
using Isekai.Characters;

namespace Isekai.Buildings
{
    public abstract class BuildingInteractionCommand : InteractionCommand
    {
        protected Transform enterPoint;
        protected Transform centerPoint;
        protected InteractableBuilding interactableBuilding;
        protected Animator animator;
        protected Coroutine coroutine;
        private BuildingProcessState processState = BuildingProcessState.None;
        private UnityAction onFinishActionCache = null;
        protected BuildingInteractionData buildingInteractionData;
        protected Isekai.UI.BuildingInteractionProgressUI progressUI;

        private static float ENTER_EXIT_TIME = 1f;

        private enum BuildingProcessState
        {
            None,
            Entering,
            Inside,
            Exiting
        }

        public BuildingInteractionCommand(InteractableBuilding interactableBuilding, Transform enterPoint, Transform centerPoint, Isekai.UI.BuildingInteractionProgressUI progressUI, BuildingInteractionData buildingInteractionData)
        {
            this.enterPoint = enterPoint;
            this.centerPoint = centerPoint;
            this.interactableBuilding = interactableBuilding;
            this.buildingInteractionData = buildingInteractionData;
            this.progressUI = progressUI;
        }

        public override void Cancel()
        {
            OnCancel();
            FinishProgressUI();
            if (coroutine != null)
            {
                
                switch (processState)
                {
                    case BuildingProcessState.Entering:
                    case BuildingProcessState.None:
                    case BuildingProcessState.Inside:
                        // Actually exit
                        interactionCharacter.StopCoroutine(coroutine);
                        interactionCharacter.StartCoroutine(ExitBuilding(()=> {
                            //onFinishActionCache();
                            interactionCharacter.commandProcessor.IsBusy = false;
                            animator?.SetFloat("movementSpeed", 0f);
                        }));
                        break;
                    case BuildingProcessState.Exiting:
                        break; // Do nothing. Wait to exit.
                }
            }
            else
            {
                animator?.SetFloat("movementSpeed", 0f);
                interactionCharacter.commandProcessor.IsBusy = false;
            }
        }

        protected virtual void OnCancel() { }
        protected virtual void OnEnter() { }
        protected virtual void OnExit() { }
        protected virtual void OnFinish() { }

        public override void SetInteractionCharacter(PlayableCharacter pc)
        {
            base.SetInteractionCharacter(pc);
            animator = interactionCharacter.GetComponentInChildren<Animator>();
        }

        public override void Execute(UnityAction onFinishAction)
        {
            Debug.Log("Executing building interaction command");
            onFinishActionCache = onFinishAction;
            coroutine = interactionCharacter.StartCoroutine(DoBuildingFullProcess());
        }

        private IEnumerator DoBuildingFullProcess()
        {
            interactableBuilding.IsBusy = true;
            StartProgressUI();
            interactionCharacter.commandProcessor.IsBusy = true;
            animator?.SetFloat("movementSpeed", 0.5f);
            Debug.Log("Entering building");
            yield return ManagePause();
            yield return EnterBuilding();
            yield return ManagePause();
            Debug.Log("Waiting");
            processState = BuildingProcessState.Inside;
            yield return DoBuildingProcess();
            yield return ManagePause();
            Debug.Log("Exiting building");
            yield return ExitBuilding();
            yield return ManagePause();
            Debug.Log("Building command done!");
            OnFinish();
            animator?.SetFloat("movementSpeed", 0f);
            interactableBuilding.IsBusy = false;
            FinishProgressUI();
            onFinishActionCache();
        }

        private void StartProgressUI()
        {
            progressUI.InitInteraction(buildingInteractionData.ResourceData);
        }

        private void FinishProgressUI()
        {
            progressUI.Finish();
        }

        private IEnumerator EnterBuilding()
        {
            processState = BuildingProcessState.Entering;
            OnEnter();
            Vector3 speed = Vector3.zero;
            interactionCharacter.transform.LookAt(centerPoint, Vector3.up);
            float timer = 0f;
            while (timer < ENTER_EXIT_TIME)
            {
                yield return ManagePause();
                //interactionCharacter.transform.position = Vector3.SmoothDamp(interactionCharacter.transform.position, centerPoint.position, ref speed, 0.1f, 1f, Time.deltaTime);
                interactionCharacter.transform.position = Vector3.Lerp(enterPoint.position, centerPoint.position, timer / ENTER_EXIT_TIME);
                timer += Time.deltaTime;
                yield return null;
            }
        }

        protected abstract IEnumerator DoBuildingProcess();

        private IEnumerator ExitBuilding(UnityAction action = null)
        {
            OnExit();
            processState = BuildingProcessState.Exiting;
            Vector3 speed = Vector3.zero;
            interactionCharacter.transform.LookAt(enterPoint, Vector3.up);
            float timer = 0f;
            while (timer < ENTER_EXIT_TIME)
            {
                yield return ManagePause();
                //interactionCharacter.transform.position = Vector3.SmoothDamp(interactionCharacter.transform.position, enterPoint.position, ref speed, 0.1f, 1f, Time.deltaTime);
                interactionCharacter.transform.position = Vector3.Lerp(centerPoint.position, enterPoint.position, timer / ENTER_EXIT_TIME);
                timer += Time.deltaTime;
                yield return null;
            }
            action?.Invoke();
        }

        public override void Undo(UnityAction onFinishAction)
        {
            throw new System.NotImplementedException();
        }
    }
}