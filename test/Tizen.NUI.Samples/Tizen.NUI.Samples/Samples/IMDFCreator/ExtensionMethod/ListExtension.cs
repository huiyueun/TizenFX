using System.Collections.Generic;
using Tizen.NUI;

namespace Space.BuildingTool.ExtensionMethod
{
    internal static class ListExtension
    {
        #region Arithmetic operations
        public static void AddValue(this IList<int> list, int value, int startIndex, int count)
        {
            for (int i = startIndex; i < startIndex + count; i++)
            {
                list[i] += value;
            }
        }

        public static void AddValue(this IList<int> list, int value)
        {
            AddValue(list, value, 0, list.Count);
        }

        public static void AddValue(this IList<Vector3> list, Vector3 value, int startIndex, int count)
        {
            for (int i = startIndex; i < startIndex + count; i++)
            {
                list[i] += value;
            }
        }

        public static void AddValue(this IList<Vector3> list, Vector3 value)
        {
            AddValue(list, value, 0, list.Count);
        }

        #endregion
    }
}
