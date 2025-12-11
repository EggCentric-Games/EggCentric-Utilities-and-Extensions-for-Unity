using EggCentric.AssetDatabases;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace EggCentric.CodeGeneraton.AssetDatabases
{
    public abstract class AssetDatabaseBuilder<TDatabase, TAsset> : ScriptableObject where TDatabase : Object, IAssetLibrary<TAsset> where TAsset : IAssetEntry
    {
        [SerializeField] private string _databasePath;
        [SerializeField] private string _outputPath;

        [SerializeField] private string _namespaceName;

        private nint nextEntryID;

        public void Rebake()
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

        private void BakeLibrary(IAssetLibrary<TAsset> assetLibrary)
        {
            var builder = new StringBuilder();
            AppendLine(builder, "// AUTO-GENERATED FILE — DO NOT MODIFY MANUALLY");
            AppendLine(builder, $"namespace {_namespaceName}");
            AppendLine(builder, "{");

            BakeCategory(builder, assetLibrary, 1);

            AppendLine(builder, "}");

            FinishGeneration(builder);
        }

        private void BakeCategory(StringBuilder builder, IAssetCategory<TAsset> category, int depth = 0)
        {
            string safeName = MakeSafe(category.Name);
            AppendLine(builder, $"public static class {safeName}", depth);
            AppendLine(builder, "{", depth);

            foreach (var entry in category.Entries)
                BakeEntry(builder, entry, depth + 1);

            if (category.SubCategories.Count > 0)
                AppendLine(builder);

            foreach (var subCategory in category.SubCategories)
                BakeCategory(builder, subCategory, depth + 1);

            AppendLine(builder, "}", depth);
        }

        private void BakeEntry(StringBuilder builder, IAssetEntry entry, int depth = 0)
        {
            if (entry == null || string.IsNullOrEmpty(entry.Name))
                return;

            string safeName = MakeSafe(entry.Name);
            AppendLine(builder, $"public const int {safeName} = {nextEntryID};", depth);
            nextEntryID++;
        }

        private void FinishGeneration(StringBuilder builder)
        {
            File.WriteAllText(_outputPath, builder.ToString(), Encoding.UTF8);
            AssetDatabase.Refresh();

            Debug.Log("Database bake completed.");
        }

        private StringBuilder AppendLine(StringBuilder builder) => builder.AppendLine();

        private StringBuilder AppendLine(StringBuilder builder, string line, int depth = 0)
        {
            builder.AppendLine();
            AddIndentation(builder, depth);
            builder.Append(line);

            return builder;
        }

        private void AddIndentation(StringBuilder builder, int depth)
        {
            for (int i = 0; i < depth; i++)
                builder.Append("    ");
        }

        private static string MakeSafe(string name)
        {
            // Replace invalid C# identifier chars
            var sb = new StringBuilder();
            if (!char.IsLetter(name[0])) sb.Append('_');

            foreach (char c in name)
            {
                if (char.IsLetterOrDigit(c) || c == '_') sb.Append(c);
                else sb.Append('_');
            }

            return sb.ToString();
        }
    }
}