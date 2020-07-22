using UnityEngine;
using Isekai.Interactions;

namespace Isekai.Characters
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField]
        private Character character = default;
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
                InteractionCommand c = new CharacterMoveCommand(hitInfo.point, positionMarkerPrefab);
                c.SetInteractionCharacter(character);
                processor.ProcessCommand(c, true);
            }
        }
    }
}