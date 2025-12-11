using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using static UnityEditor.ObjectChangeEventStream;

namespace EggCentric.CodeGeneration
{
    public class CodeWriter
    {
        private readonly StringBuilder _builder;
        private int _depth;

        public CodeWriter() => _builder = new StringBuilder();

        public CodeWriter OpenBlock(int depth = 0)
        {
            AppendLine("{");
            _depth++;

            return this;
        }

        public CodeWriter CloseBlock(int depth = 0)
        {
            _depth--;
            AppendLine("}");

            return this;
        }

        public CodeWriter AddNamespace(string namespaceName)
        {
            var safeName = MakeSafe(namespaceName);
            AppendLine($"namespace {namespaceName}");

            return this;
        }

        public CodeWriter AddClass(string className)
        {
            var safeName = MakeSafe(className);
            AppendLine($"public static class {safeName}");

            return this;
        }

        public CodeWriter AddVariable<TVariable>(string variableName, TVariable variableValue)
        {
            var safeName = MakeSafe(variableName);
            AppendLine($"public const {NameAliases.GetTypeName<TVariable>()} {safeName} = {variableValue};");

            return this;
        }

        public CodeWriter AddEmptyLine()
        {
            _builder.AppendLine();
            return this;
        }

        public override string ToString() => _builder.ToString();

        private void AddWarning()
        {
            AppendLine("// AUTO-GENERATED FILE — DO NOT MODIFY MANUALLY");
        }

        private StringBuilder AppendLine(string line)
        {
            _builder.AppendLine();
            AddIndentation(_builder, _depth);
            _builder.Append(line);

            return _builder;
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
            if (!char.IsLetter(name[0]))
                sb.Append('_');

            foreach (char c in name)
            {
                if (char.IsLetterOrDigit(c) || c == '_') sb.Append(c);
                else sb.Append('_');
            }

            return sb.ToString();
        }
    }

    public static class NameAliases
    {
        private static readonly Dictionary<Type, string> Aliases = new()
        {
            { typeof(int), "int" },
            { typeof(string), "string" },
            { typeof(bool), "bool" },
            { typeof(float), "float" },
            { typeof(double), "double" },
            { typeof(long), "long" },
            { typeof(short), "short" },
            { typeof(byte), "byte" },
            { typeof(char), "char" },
            { typeof(decimal), "decimal" },
            { typeof(void), "void" },
        };

        public static string GetTypeName<T>()
        {
            if (!Aliases.TryGetValue(typeof(T), out var typeName))
                return typeof(T).Name;

            return typeName;
        }

        public static string GetTypeName(Type type)
        {
            if (!Aliases.TryGetValue(type, out var typeName))
                return type.Name;

            return typeName;
        }
    }
}
