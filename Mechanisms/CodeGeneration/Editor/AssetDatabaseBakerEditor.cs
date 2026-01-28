using UnityEditor;
using UnityEngine;

namespace EggCentric.CodeGeneraton.AssetDatabases
{
    [CustomEditor(typeof(AssetDatabaseBaker), true)]
    public class GenericAssetDatabaseBakerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Rebake"))
            {
                ((AssetDatabaseBaker)target).Rebake();
            }
        }
    }
}