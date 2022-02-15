using System.Collections.ObjectModel;
using System;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using System.Collections.Generic;

namespace LW1
{
    internal class Symbol
    {
        
        public Symbol(float top, float left, params Rectangle[] parts)
        {
            _top = top;
            _left = left;
            _parts = new(parts);
        }

        private List<Shape> _parts;
        private float _top;
        private float _left;

        public List<Shape> Parts { get => _parts; }
        public float Top { get => _top; }
        public float Left { get => _left; }

        public static Rectangle GetRectangle(
            float width, float height, float top, float left, int angle , Brush fillColor)
        {
            var rect = new Rectangle();
            rect.Width = width;
            rect.Height = height;
            rect.RenderTransform = new SkewTransform(angle, 0, width / 2, height / 2);
            rect.Fill = fillColor;
            Canvas.SetTop(rect, top);
            Canvas.SetLeft(rect, left);

            return rect;
        }
    }
}