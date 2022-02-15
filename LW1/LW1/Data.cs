using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LW1
{
    internal class Data
    {
        public Data() 
        {
            _symbols = new();
            
            _symbols.Add(GetLastNameSymb(25, Brushes.LightPink));
            _symbols.Add(GetFirstNameSymb(25, Brushes.LightBlue));
            _symbols.Add(GetMiddleNameSymb(25, Brushes.LightGreen));
        }

        private Symbol GetLastNameSymb(float width, Brush fillColor)
        {
            var rect1 = Symbol.GetRectangle(width, 150, -0, 40, -15, fillColor);
            var rect2 = Symbol.GetRectangle(width, 150, 0, 90, 15, fillColor);
            var rect3 = Symbol.GetRectangle(150, width, 140, 0, 0, fillColor);
            var rect4 = Symbol.GetRectangle(width, 65, 140, 0, 0, fillColor);
            var rect5 = Symbol.GetRectangle(width, 65, 140, 130, 0, fillColor);

            return new Symbol(100, 130, rect1, rect2, rect3, rect4, rect5);
        }

        private Symbol GetFirstNameSymb(float width, Brush fillColor)
        {
            var rect1 = Symbol.GetRectangle(width, 200, 0, 0, 0, fillColor);
            var rect2 = Symbol.GetRectangle(130, width, 0, 0, 0, fillColor);
            var rect3 = Symbol.GetRectangle(130, width, 90, 0, 0, fillColor);
            var rect4 = Symbol.GetRectangle(130, width, 180, 0, 0, fillColor);
            
            return new Symbol(100, 310, rect1, rect2, rect3, rect4);
        }

        private Symbol GetMiddleNameSymb(float width, Brush fillColor)
        {
            var rect1 = Symbol.GetRectangle(width, 205, 0, 40, -20, fillColor);
            var rect2 = Symbol.GetRectangle(width, 205, 0, 120, 20, fillColor);
            var rect3 = Symbol.GetRectangle(110, width, 140, 40, 0, fillColor);
            
            return new Symbol(100, 460, rect1, rect2, rect3);
        }

        private List<Symbol> _symbols;
        public List<Symbol> Symbols { get => _symbols; } 
    }
}
