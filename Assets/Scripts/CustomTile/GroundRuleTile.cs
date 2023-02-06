using System.Collections.Generic;
using GGJ2023.Level;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GGJ2023
{
    [CreateAssetMenu(fileName = "Ground Rule Tile", menuName = "Custom Rule Tile/Ground")]
    public class GroundRuleTile : RuleTile
    {
        public override bool RuleMatch(int neighbor, TileBase other)
        {
            if (other is RuleOverrideTile ot)
                other = ot.m_InstanceTile;

            switch (neighbor)
            {
                case TilingRuleOutput.Neighbor.This: return MatchTile(other);
                case TilingRuleOutput.Neighbor.NotThis: return  !MatchTile(other);
            }
            return true;
        }

        bool MatchTile(TileBase tileBase)
        {
            return tileBase != null;
        }

        public enum TileType
        {
            Corner, Edge, Center, Single, MinusCorner
        }
    }
}