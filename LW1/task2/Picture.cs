using System;
using System.Collections.Generic;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace task2
{
    internal class Picture
    {
        private List<Shape> _shapes;
        
        public Picture()
        {
            _shapes = new();
            
            var sky = ShapeCreator.CreateRectangle(Brushes.SkyBlue, Brushes.Transparent);
            var grass = ShapeCreator.CreateRectangle(Brushes.Green, Brushes.Transparent);
            var roof = ShapeCreator.CreateTriangle(0.7f, Brushes.IndianRed, Brushes.Transparent);
            var wall = ShapeCreator.CreateRectangle(Brushes.BurlyWood, Brushes.Transparent);
            var windowFrame = ShapeCreator.CreateRectangle(Brushes.White, Brushes.Transparent);
            var windowGlass = ShapeCreator.CreateRectangle(Brushes.LightBlue, Brushes.Transparent);
            var windowInternalFrame = ShapeCreator.CreatePolygon(Brushes.White, 
                                                                 Brushes.White, 
                                                                 new Point(0,0), 
                                                                 new Point(100, 0), 
                                                                 new Point(100, 10), 
                                                                 new Point(55, 10), 
                                                                 new Point(55, 100), 
                                                                 new Point(45, 100), 
                                                                 new Point(45, 10), 
                                                                 new Point(0, 10));
            var door = ShapeCreator.CreateRectangle(Brushes.Brown, Brushes.Transparent);
            var doorHandle = ShapeCreator.CreateEllipse(Brushes.Black, Brushes.Black);
            var doorPart1 = ShapeCreator.CreateRectangle(Brushes.Brown, Brushes.DarkRed);
            var doorPart2 = ShapeCreator.CreateRectangle(Brushes.Brown, Brushes.DarkRed);
            var tube = ShapeCreator.CreateRectangle(Brushes.Firebrick, Brushes.Transparent);
            var horzontalBoard1 = ShapeCreator.CreateRectangle(Brushes.Brown, Brushes.DarkRed);
            var horzontalBoard2 = ShapeCreator.CreateRectangle(Brushes.Brown, Brushes.DarkRed);
            var verticalBoard1 = ShapeCreator.CreatePolygon(Brushes.Brown,
                                                            Brushes.DarkRed,
                                                            new Point(0, 25),
                                                            new Point(50, 0),
                                                            new Point(100, 25),
                                                            new Point(100, 100),
                                                            new Point(0, 100));
            var verticalBoard2 = ShapeCreator.CreatePolygon(Brushes.Brown,
                                                            Brushes.DarkRed,
                                                            new Point(0, 25),
                                                            new Point(50, 0),
                                                            new Point(100, 25),
                                                            new Point(100, 100),
                                                            new Point(0, 100));
            var verticalBoard3 = ShapeCreator.CreatePolygon(Brushes.Brown,
                                                            Brushes.DarkRed,
                                                            new Point(0, 25),
                                                            new Point(50, 0),
                                                            new Point(100, 25),
                                                            new Point(100, 100),
                                                            new Point(0, 100));
            var verticalBoard4 = ShapeCreator.CreatePolygon(Brushes.Brown,
                                                            Brushes.DarkRed,
                                                            new Point(0, 25),
                                                            new Point(50, 0),
                                                            new Point(100, 25),
                                                            new Point(100, 100),
                                                            new Point(0, 100));

            sky.SetTransform(0, 0, 800, 435, 0);
            grass.SetTransform(350, 0, 800, 85, 0);
            horzontalBoard1.SetTransform(310, 370, 150, 10, 2);
            horzontalBoard2.SetTransform(340, 370, 150, 10, 0);
            verticalBoard1.SetTransform(270, 389, 30, 100, 0);
            verticalBoard2.SetTransform(270, 419, 30, 100, 0);
            verticalBoard3.SetTransform(270, 451, 30, 100, 2);
            verticalBoard4.SetTransform(270, 482, 30, 100, 4);
            wall.SetTransform(170, 60, 330, 200, 0);
            door.SetTransform(220, 250, 100, 150, 0);
            doorHandle.SetTransform(292.5f, 260, 5, 5, 0);
            doorPart1.SetTransform(230, 260, 80, 55, 0);
            doorPart2.SetTransform(305, 260, 80, 55, 0);
            windowFrame.SetTransform(200, 80, 125, 125, 0);
            windowGlass.SetTransform(210, 90, 105, 105, 0);
            windowInternalFrame.SetTransform(240, 90, 105, 75, 0);
            tube.SetTransform(50, 100, 50, 100, -5);
            roof.SetTransform(50, 50, 350, 125, 0);

            _shapes.Add(sky);
            _shapes.Add(grass);
            _shapes.Add(horzontalBoard1);
            _shapes.Add(horzontalBoard2);
            _shapes.Add(verticalBoard1);
            _shapes.Add(verticalBoard2);
            _shapes.Add(verticalBoard3);
            _shapes.Add(verticalBoard4);
            _shapes.Add(wall);
            _shapes.Add(door);
            _shapes.Add(doorHandle);
            _shapes.Add(doorPart1);
            _shapes.Add(doorPart2);
            _shapes.Add(windowFrame);
            _shapes.Add(windowGlass);
            _shapes.Add(windowInternalFrame);
            _shapes.Add(tube);
            _shapes.Add(roof);
        }

        public List<Shape> Shapes { get => _shapes; }

    }
}
