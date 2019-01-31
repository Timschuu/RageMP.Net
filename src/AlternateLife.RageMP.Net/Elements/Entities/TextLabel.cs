using System;
using System.Drawing;
using AlternateLife.RageMP.Net.Data;
using AlternateLife.RageMP.Net.Enums;
using AlternateLife.RageMP.Net.Extensions;
using AlternateLife.RageMP.Net.Helpers;
using AlternateLife.RageMP.Net.Interfaces;
using AlternateLife.RageMP.Net.Native;

namespace AlternateLife.RageMP.Net.Elements.Entities
{
    internal class TextLabel : Entity, ITextLabel
    {
        public Color Color
        {
            get
            {
                CheckExistence();

                return StructConverter.PointerToStruct<ColorRgba>(Rage.TextLabel.TextLabel_GetColor(NativePointer)).FromModColor();
            }
            set
            {
                CheckExistence();

                Rage.TextLabel.TextLabel_SetColor(NativePointer, value.ToModColor());
            }
        }

        public string Text
        {
            get
            {
                CheckExistence();

                return StringConverter.PointerToString(Rage.TextLabel.TextLabel_GetText(NativePointer));
            }
            set
            {
                Contract.NotNull(value, nameof(value));

                using (var converter = new StringConverter())
                {
                    Rage.TextLabel.TextLabel_SetText(NativePointer, converter.StringToPointer(value));
                }
            }
        }

        public bool LOS
        {
            get
            {
                CheckExistence();

                return Rage.TextLabel.TextLabel_GetLOS(NativePointer);
            }
            set
            {
                CheckExistence();

                Rage.TextLabel.TextLabel_SetLOS(NativePointer, value);
            }
        }

        public float DrawDistance
        {
            get
            {
                CheckExistence();

                return Rage.TextLabel.TextLabel_GetDrawDistance(NativePointer);
            }
            set
            {
                CheckExistence();

                Rage.TextLabel.TextLabel_SetDrawDistance(NativePointer, value);
            }
        }

        public uint Font
        {
            get
            {
                CheckExistence();

                return Rage.TextLabel.TextLabel_GetFont(NativePointer);
            }
            set
            {
                CheckExistence();

                Rage.TextLabel.TextLabel_SetFont(NativePointer, value);
            }
        }

        internal TextLabel(IntPtr nativePointer, Plugin plugin) : base(nativePointer, plugin, EntityType.TextLabel)
        {
        }
    }
}
