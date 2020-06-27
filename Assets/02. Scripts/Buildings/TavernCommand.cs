using UnityEngine;
using System.Collections;
using Isekai.Interactions;
using UnityEngine.Events;

namespace Isekai.Buildings
{
    public class TavernCommand : InteractionCommand
    {
        private Transform enterPoint;
        private Transform centerPoint;
        private Animator animator;
        private Coroutine coroutine;

        public TavernCommand(Transform enterPoint, Transform centerPoint)
        {
            this.enterPoint = enterPoint;
            this.centerPoint = centerPoint;
        }

        public override void Cancel()
        {
            if (coroutine != null)
            {
                interactionCharacter.StopCoroutine(coroutine);
            }
            animator?.SetFloat("movementSpeed", 0f);
        }

        public override void Execute(UnityAction onFinishAction)
        {
            Debug.Log("Executing tavern command");
            animator = interactionCharacter.GetComponentInChildren<Animator>();
            coroutine = interactionCharacter.StartCoroutine(DoTavernProcess(onFinishAction));
        }

        private IEnumerator DoTavernProcess(UnityAction onFinishAction)
        {
            animator?.SetFloat("movementSpeed", 0.5f);
            Debug.Log("Entering tavern");
            yield return EnterTavern();
            Debug.Log("Waiting");
            yield return Wait();
            Debug.Log("Exiting tavern");
            yield return ExitTavern();
            Debug.Log("Tavern command done!");
            animator?.SetFloat("movementSpeed", 0f);
            onFinishAction();
        }

        private IEnumerator EnterTavern()
        {
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

        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(2f);
        }

        private IEnumerator ExitTavern()
        {
            Vector3 speed = Vector3.zero;
            interactionCharacter.transform.LookAt(enterPoint, Vector3.up);
            float timer = 0f;
            while (timer < 3f)
            {
                interactionCharacter.transform.position = Vector3.SmoothDamp(interactionCharacter.transform.position, enterPoint.position, ref speed, 0.1f, 1f, Time.deltaTime);
                timer += Time.deltaTime;
                yield return null;
            }
        }


        public override void Undo(UnityAction onFinishAction)
        {
            throw new System.NotImplementedException();
        }
    }
}