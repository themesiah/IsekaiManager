using UnityEngine;
using UnityEngine.AI;

namespace Isekai.Characters
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField]
        private PlayableCharacter playableCharacter = default;
        [SerializeField]
        private GamedevsToolbox.ScriptableArchitecture.Sets.RuntimeSingleCamera cameraRef = default;
        [SerializeField]
        private GamedevsToolbox.CommandPattern.CommandProcessor processor = default;
        [SerializeField]
        protected GameObject positionMarkerPrefab = default;

        private void Update()
        {
        }

        public void MoveToScreenPoint(Vector2 screenPoint)
        {
            Ray ray = cameraRef.Get().ScreenPointToRay(screenPoint);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                Interactions.InteractionCommand c = new CharacterMoveCommand(hitInfo.point, positionMarkerPrefab);
                c.SetInteractionCharacter(playableCharacter);
                processor.ProcessCommand(c, true);
            }
        }
    }
}