using UnityEngine;

namespace EggCentric.QoL
{
    public static class GeneralUtil
    {
        public static Vector2Int GetGridPosition(int width, int index)
        {
            int row = index / width;
            int col = index % width;

            return new Vector2Int(col, row);
        }
    }
}