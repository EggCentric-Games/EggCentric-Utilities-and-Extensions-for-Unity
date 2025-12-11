using EggCentric.AssetDatabases;
using UnityEngine;

namespace EggCentric.CodeGeneraton.AssetDatabases
{
    [System.Serializable]
    public class TestEntry : IAssetEntry
    {
        public string Name => _name;

        [SerializeField] private string _name;
    }
}