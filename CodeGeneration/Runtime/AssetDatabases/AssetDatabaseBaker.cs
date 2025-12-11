using EggCentric.AssetDatabases;
using EggCentric.CodeGeneration;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace EggCentric.CodeGeneraton.AssetDatabases
{
    public abstract class AssetDatabaseBaker : ScriptableObject, IAssetDatabaseBaker
    {
        public abstract void Rebake();
    }

    public abstract class AssetDatabaseBaker<TDatabase, TAsset> : AssetDatabaseBaker where TDatabase : Object, IAssetDatabase<TAsset> where TAsset : IAssetEntry
    {
        [SerializeField] private string _databasePath;
        [SerializeField] private string _outputPath;

        [SerializeField] private string _namespaceName;

        private int nextEntryID;

        public override void Rebake()
        {
            CleanUp();

            if (!LoadDatabase(out TDatabase assetLibrary))
            {
                Debug.LogError("Error while loading database. Rejecting");
                return;
            }

            BakeLibrary(assetLibrary);
        }

        private void CleanUp()
        {
            nextEntryID = 0;
        }

        private bool LoadDatabase(out TDatabase assetLibrary)
        {
            assetLibrary = null;
            var database = AssetDatabase.LoadAssetAtPath<TDatabase>(_databasePath);
            if (database == null)
            {
                Debug.LogError("Database not found: " + _databasePath);
                return false;
            }

            assetLibrary = database;
            return true;
        }

        private void BakeLibrary(TDatabase assetLibrary)
        {
            var codeWriter = new CodeWriter();

            codeWriter.AddNamespace(_namespaceName);
            codeWriter.OpenBlock();
            
            BakeCategory(codeWriter, assetLibrary.Root);
            
            codeWriter.CloseBlock();

            FinishGeneration(codeWriter);
        }

        private void BakeCategory(CodeWriter codeWriter, IAssetCategory<TAsset> category)
        {
            codeWriter.AddClass(category.Name);
            codeWriter.OpenBlock();

            foreach (var entry in category.Entries)
                BakeEntry(codeWriter, entry);

            if (category.Entries.Count > 0 && category.SubCategories.Count > 0)
                codeWriter.AddEmptyLine();

            foreach (var subCategory in category.SubCategories)
                BakeCategory(codeWriter, subCategory);

            codeWriter.CloseBlock();
        }

        private void BakeEntry(CodeWriter codeWriter, IAssetEntry entry)
        {
            if (entry == null || string.IsNullOrEmpty(entry.Name))
                return;

            codeWriter.AddVariable(entry.Name, nextEntryID);
            nextEntryID++;
        }

        private void FinishGeneration(CodeWriter codeWriter)
        {
            File.WriteAllText(_outputPath, codeWriter.ToString(), Encoding.UTF8);
            AssetDatabase.Refresh();

            Debug.Log("Database bake completed.");
        }
    }
}