using UnityEngine;
using UnityEditor;

namespace Isekai.Characters
{
    [CustomEditor(typeof(CharacterHealth))]
    public class CharacterHealthCustomInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!UnityEditor.EditorApplication.isPlaying)
                return;
            if (GUILayout.Button("Deal 5 damage"))
            {
                CharacterHealth ch = target as CharacterHealth;
                AttackData ad = new AttackData();
                ad.physicalDamage = 5;
                ch.Damage(ad);
            }
            if (GUILayout.Button("Deal 20 damage"))
            {
                CharacterHealth ch = target as CharacterHealth;
                AttackData ad = new AttackData();
                ad.physicalDamage = 20;
                ch.Damage(ad);
            }
        }
    }
}