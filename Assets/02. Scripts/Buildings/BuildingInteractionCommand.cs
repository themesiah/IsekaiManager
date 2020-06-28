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

        private enum BuildingProcessState
        {
            None,
            Entering,
            Inside,
            Exiting
        }

        public BuildingInteractionCommand(InteractableBuilding interactableBuilding, Transform enterPoint, Transform centerPoint)
        {
            this.enterPoint = enterPoint;
            this.centerPoint = centerPoint;
            this.interactableBuilding = interactableBuilding;
        }

        public override void Cancel()
        {
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
            OnCancel();
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
            interactionCharacter.commandProcessor.IsBusy = true;
            animator?.SetFloat("movementSpeed", 0.5f);
            Debug.Log("Entering building");
            yield return EnterBuilding();
            Debug.Log("Waiting");
            processState = BuildingProcessState.Inside;
            yield return DoBuildingProcess();
            Debug.Log("Exiting building");
            yield return ExitBuilding();
            Debug.Log("Building command done!");
            OnFinish();
            animator?.SetFloat("movementSpeed", 0f);
            interactableBuilding.IsBusy = false;
            onFinishActionCache();
        }

        private IEnumerator EnterBuilding()
        {
            processState = BuildingProcessState.Entering;
            OnEnter();
            Vector3 speed = Vector3.zero;
            interactionCharacter.transform.LookAt(centerPoint, Vector3.up);
            float timer = 0f;
            while (timer < 3f)
            {
                interactionCharacter.transform.position = Vector3.SmoothDamp(interactionCharacter.transform.position, centerPoint.position, ref speed, 0.1f, 1f, Time.deltaTime);
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
            while (timer < 3f)
            {
                interactionCharacter.transform.position = Vector3.SmoothDamp(interactionCharacter.transform.position, enterPoint.position, ref speed, 0.1f, 1f, Time.deltaTime);
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