using System;

namespace Assets._Project.Scripts.ResourceExtraction.ScissorsMining
{
    public class NormalGrass : Grass
    {
        public TypesGrasses TypesGrasses;

        public override Enum GetItemType() => TypesGrasses;
    }
}