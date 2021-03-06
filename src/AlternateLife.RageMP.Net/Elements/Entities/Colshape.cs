using System;
using System.Numerics;
using AlternateLife.RageMP.Net.Enums;
using AlternateLife.RageMP.Net.Interfaces;
using AlternateLife.RageMP.Net.Native;

namespace AlternateLife.RageMP.Net.Elements.Entities
{
    internal class Colshape : Entity, IColshape
    {
        public ColshapeType ShapeType
        {
            get
            {
                CheckExistence();

                return (ColshapeType) Rage.Colshape.Colshape_GetShapeType(NativePointer);
            }
        }

        internal Colshape(IntPtr nativePointer, Plugin plugin) : base(nativePointer, plugin, EntityType.Colshape)
        {
        }

        public bool IsPointWhithin(Vector3 position)
        {
            CheckExistence();

            return Rage.Colshape.Colshape_IsPointWithin(NativePointer, position);
        }
    }
}
