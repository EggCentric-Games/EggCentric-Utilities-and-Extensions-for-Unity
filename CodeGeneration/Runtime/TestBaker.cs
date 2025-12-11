using UnityEngine;

namespace EggCentric.CodeGeneraton.AssetDatabases
{
    [CreateAssetMenu(fileName = "TestBaker", menuName = "Baker")]
    public class TestBaker : AssetDatabaseBaker<TestDatabase, TestEntry>
    {

    }
}