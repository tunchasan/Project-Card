using System.Collections.Generic;
using UnityEngine;

namespace ProjectCard.Game.SO
{
    [CreateAssetMenu(fileName = "ProjectCards", menuName = "ProjectCards/ThemeData")]
    public class ThemeData : ScriptableObject
    {
        public Sprite backgroundAsset = null;
        public Color color = Color.black;
        public List<Sprite> cardAssets = new();
    }
}