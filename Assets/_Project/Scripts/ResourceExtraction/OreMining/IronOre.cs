using Assets._Project.Scripts.ResourceExtraction.OreMining;
using System;

namespace Assets._Project.Sctipts.ResourceExtraction.OreMining
{
    public class IronOre : Ore
    {
        public TypesOre TypesOre;

        public override Enum GetItemType() => TypesOre;
    }
}