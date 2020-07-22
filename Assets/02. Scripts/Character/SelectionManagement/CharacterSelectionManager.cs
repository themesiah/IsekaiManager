using UnityEngine;
using GamedevsToolbox.ScriptableArchitecture.Events;

namespace Isekai.Characters
{
    public class CharacterSelectionManager : MonoBehaviour
    {
        [SerializeField]
        private RuntimeCharacterSet playableCharacterSet = default;
        [SerializeField]
        private RuntimeCharacterSet allPlayableCharactersSet = default;
        [SerializeField]
        private GamedevsToolbox.ScriptableArchitecture.Sets.RuntimeSingleCamera cameraRef = default;
        [SerializeField]
        private GamedevsToolbox.ScriptableArchitecture.Values.ScriptableFloatReference singleSelectionDistanceThreshold = default;
        [SerializeField]
        private GenericGameEvent selectionBoxEvent = default;
        [SerializeField]
        private GameEvent onCharacterUnselectedEvent = default;
        [SerializeField]
        private GenericGameEvent onCharacterSelectedEvent = default;
        [SerializeField]
        private bool activateDebug = false;

        private Vector3 clickDownPosition = Vector3.zero;
        private bool doingSelectionBox = false;

        private void Start()
        {
            playableCharacterSet?.Clear();
        }

        private void Update()
        {
            MouseButtonPress();
            MouseButtonKeep();
            MouseButtonUp();
            ManageKeySelection();
        }

        private void ManageKeySelection()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                SelectPlayer(allPlayableCharactersSet.Items[0]);
            }
        }

        private void MouseButtonUp()
        {
            if (Input.GetMouseButtonUp(0)) // Release
            {
                if (doingSelectionBox)
                {
                    StopSelectionBox();
                }
                else
                {
                    UnselectAllPlayers();
                    Ray ray = cameraRef.Get().ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit hitInfo))
                    {
                        if (hitInfo.collider.tag == "Character")
                        {
                            Character c = hitInfo.collider.GetComponent<Character>();
                            if (c != null)
                            {
                                SelectPlayer(c);
                            }
                        }
                    }
                }
            }
        }

        private void MouseButtonKeep()
        {
            if (Input.GetMouseButton(0)) // Keep
            {
                if (Vector3.Distance(Input.mousePosition, clickDownPosition) > singleSelectionDistanceThreshold.GetValue() && !doingSelectionBox)
                {
                    StartSelectionBox();
                }
            }

            if (doingSelectionBox)
            {
                WhileSelectionBox();
            }
        }

        private void MouseButtonPress()
        {
            if (Input.GetMouseButtonDown(0)) // Press
            {
                clickDownPosition = Input.mousePosition;
            }
        }

        private void StartSelectionBox()
        {
            Log("Starting selection box");
            doingSelectionBox = true;
            BoxActiveEvent();
        }

        private void StopSelectionBox()
        {
            Log("Stopping selection box");
            doingSelectionBox = false;
            SelectionBox.SelectionBoxData sbd = new SelectionBox.SelectionBoxData();
            sbd.active = false;
            selectionBoxEvent.Raise(sbd);
        }

        private void BoxActiveEvent()
        {
            SelectionBox.SelectionBoxData sbd = new SelectionBox.SelectionBoxData();
            sbd.active = true;
            sbd.startPosition = (clickDownPosition + Input.mousePosition) / 2f;
            sbd.size = Input.mousePosition - clickDownPosition;
            sbd.size.x = Mathf.Abs(sbd.size.x);
            sbd.size.y = Mathf.Abs(sbd.size.y);
            selectionBoxEvent.Raise(sbd);
        }

        private void WhileSelectionBox()
        {
            BoxActiveEvent();
            Vector2 min = Vector2.Min(clickDownPosition, Input.mousePosition);
            Vector2 max = Vector2.Max(clickDownPosition, Input.mousePosition);
            allPlayableCharactersSet.ForEach(pc => {
                CharacterSelection cs = pc.gameObject.GetComponent<CharacterSelection>();
                if (cs != null)
                {
                    Vector2 screenPoint = cameraRef.Get().WorldToScreenPoint(pc.transform.position);
                    if (!cs.IsSelected)
                    {
                        if (screenPoint.x > min.x && screenPoint.y > min.y && screenPoint.x < max.x && screenPoint.y < max.y)
                        {
                            SelectPlayer(pc);
                        }
                    } else
                    {
                        if (screenPoint.x < min.x || screenPoint.y < min.y || screenPoint.x > max.x || screenPoint.y > max.y)
                        {
                            UnselectPlayer(pc);
                        }
                    }
                }
            });
        }

        private void SelectPlayer(Character pc)
        {
            onCharacterSelectedEvent?.Raise(pc);
            pc.characterSelection.Select();
            playableCharacterSet.Add(pc);
        }

        private void UnselectPlayer(Character pc)
        {
            onCharacterUnselectedEvent?.Raise();
            pc.characterSelection.Unselect();
            playableCharacterSet.Remove(pc);
        }

        private void UnselectAllPlayers()
        {
            onCharacterUnselectedEvent?.Raise();
            playableCharacterSet.ForEach(oldPc => oldPc.characterSelection.Unselect());
            playableCharacterSet.Clear();
        }

        private void Log(string text)
        {
            if (activateDebug)
                GamedevsToolbox.Utils.Logger.Logger.Log(text);
        }
    }
}