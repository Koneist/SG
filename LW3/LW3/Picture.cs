using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LW3.Shapes;
using OpenTK.Mathematics;

namespace LW3
{
    internal class Picture
    {
        private List<Shape> _shapes;
        
        public List<Shape> Shapes {  get => _shapes; }
        public int ShapeCount { get => _shapes.Count; }
        public Picture()
        {
            _shapes = new List<Shape>();
        }

        public Picture(params Shape[] shapes)
        {
            _shapes = shapes.ToList();
        }

        public Picture(List<Shape> shapes)
        {
            _shapes = shapes;
        }

        public void AddShape(Shape shape)
        {
            _shapes.Add(shape);
        }

        private void Draw(Window canvas)
        {
            foreach (var shape in _shapes)
                shape.Draw(canvas);
        }

        public static Picture Kopatych 
        {
            get
            {
                Picture picture = new();

                //left leg
                picture.AddShape(new Curve(new float[] { -100, -300, -35, -150, -10, -300 }, new(146, 60, 33, 255)));
                picture.AddShape(new Ellipse(-85, -350, 75, 50, new(146, 60, 33, 255)));
                picture.AddShape(new Curve(new float[] { -160, -350, -160, -295, -85, -290 }, new(146, 60, 33, 255)));
                picture.AddShape(new Curve(new float[] { -160, -350, -170, -400, -85, -400 }, new(146, 60, 33, 255)));
                picture.AddShape(new Curve(new float[] { -85, -400, 0, -400, -10, -350 }, new(146, 60, 33, 255)));
                picture.AddShape(new Curve(new float[] { -10, -300, 0, -350, -10, -370 }, new(146, 60, 33, 255)));
                picture.AddShape(new Polygon(new float[] { -100, -300, -10, -300, -10, -350, -100, -350 }, new(146, 60, 33, 255)));

                picture.AddShape(new Curve(new float[] { -90, -295, -35, -165, -20, -295 }, new(227, 105, 19, 255)));
                picture.AddShape(new Ellipse(-85, -350, 70, 45, new(227, 105, 19, 255)));
                picture.AddShape(new Curve(new float[] { -155, -355, -160, -295, -75, -305 }, new(227, 105, 19, 255)));
                picture.AddShape(new Curve(new float[] { -155, -345, -160, -400, -75, -395 }, new(227, 105, 19, 255)));
                picture.AddShape(new Curve(new float[] { -75, -395, -5, -395, -15, -345 }, new(227, 105, 19, 255)));
                picture.AddShape(new Curve(new float[] { -20, -295, -10, -345, -15, -365 }, new(227, 105, 19, 255)));
                picture.AddShape(new Polygon(new float[] { -75, -295, -20, -295, -15, -345, -75, -345 }, new(227, 105, 19, 255)));

                //right leg
                picture.AddShape(new Curve(new float[] { 100, -300, 35, -150, 10, -300 }, new(146, 60, 33, 255)));
                picture.AddShape(new Ellipse(85, -350, 75, 50, new(146, 60, 33, 255)));
                picture.AddShape(new Curve(new float[] { 160, -350, 160, -295, 85, -290 }, new(146, 60, 33, 255)));
                picture.AddShape(new Curve(new float[] { 160, -350, 170, -400, 85, -400 }, new(146, 60, 33, 255)));
                picture.AddShape(new Curve(new float[] { 85, -400, 0, -400, 10, -350 }, new(146, 60, 33, 255)));
                picture.AddShape(new Curve(new float[] { 10, -300, 0, -350, 10, -370 }, new(146, 60, 33, 255)));
                picture.AddShape(new Polygon(new float[] { 100, -300, 10, -300, 10, -350, 100, -350 }, new(146, 60, 33, 255)));

                picture.AddShape(new Curve(new float[] { 90, -295, 35, -165, 20, -295 }, new(227, 105, 19, 255)));
                picture.AddShape(new Ellipse(85, -350, 70, 45, new(227, 105, 19, 255)));
                picture.AddShape(new Curve(new float[] { 155, -355, 160, -295, 75, -305 }, new(227, 105, 19, 255)));
                picture.AddShape(new Curve(new float[] { 155, -345, 160, -400, 75, -395 }, new(227, 105, 19, 255)));
                picture.AddShape(new Curve(new float[] { 75, -395, 5, -395, 15, -345 }, new(227, 105, 19, 255)));
                picture.AddShape(new Curve(new float[] { 20, -295, 10, -345, 15, -365 }, new(227, 105, 19, 255)));
                picture.AddShape(new Polygon(new float[] { 75, -295, 20, -295, 15, -345, 75, -345 }, new(227, 105, 19, 255)));

                //Body
                picture.AddShape(new Ellipse(0, 0, 250f, 250f, new(146, 60, 33, 255)));
                picture.AddShape(new Ellipse(0, 0, 240f, 240f, new(227, 105, 19, 255)));
                picture.AddShape(new Curve(new float[] {-240, 0, -240, -240, 30, -290, 130, -202 }, new(202, 84, 28, 255)));
                picture.AddShape(new Curve(new float[] { -240, 0, -240, -180, 30, -230, 130, -202 }, new(227, 105, 19, 255)));

                //Right Ear
                picture.AddShape(new Ellipse(140, 225, 50f, 50f, new(146, 60, 33, 255)));
                picture.AddShape(new Ellipse(140, 225, 40f, 40f, new(227, 105, 19, 255)));
                picture.AddShape(new Polygon(new float[]
                {
                    100f, 225f,
                    75f, 220f,
                    170f, 120f,
                    160f, 195f,
                }, new(227, 105, 19, 255)));

                // Left Ear
                picture.AddShape(new Ellipse(-140, 225, 50f, 50f, new(146, 60, 33, 255)));
                picture.AddShape(new Ellipse(-140, 225, 40f, 40f, new(227, 105, 19, 255)));
                picture.AddShape(new Polygon(new float[]
                {
                    -100f, 225f,
                    -75f, 220f,
                    -170f, 120f,
                    -160f, 195f,
                }, new(227, 105, 19, 255)));

                //eyes
                picture.AddShape(new Ellipse(60, 80, 75, 75, new(146, 60, 33, 255)));
                picture.AddShape(new Ellipse(-60, 80, 75, 75, new(146, 60, 33, 255)));
                picture.AddShape(new Ellipse(60, 80, 70, 70, Color4.White));
                picture.AddShape(new Ellipse(-60, 80, 70, 70, Color4.White));
                picture.AddShape(new BrokenLine(new float[] { 0f, 40f, 0f, 125f }, new(146, 60, 33, 255), 5f));
                
                picture.AddShape(new Ellipse(-30, 80, 20, 28, Color4.Black));
                picture.AddShape(new Ellipse(-17, 95, 5, 7, Color4.White));
                picture.AddShape(new Ellipse(30, 80, 20, 28, Color4.Black));
                picture.AddShape(new Ellipse(43, 95, 5, 7, Color4.White));

                picture.AddShape(new Ellipse(0, -40, 80, 55, new(95, 56, 44, 255)));
                picture.AddShape(new Ellipse(0, -20, 85, 55, new(227, 105, 19, 255)));

                //brows
                picture.AddShape(new Polygon(new float[] { 30, 200, 130, 185, 130, 155, 30, 165 }, new(95, 56, 44, 255)));
                picture.AddShape(new Polygon(new float[] { -30, 200, -130, 185, -130, 155, -30, 165 }, new(95, 56, 44, 255)));

                //nose
                picture.AddShape(new Ellipse(0, 0, 80, 55, new(95, 56, 44, 255)));
                picture.AddShape(new Curve(new float[] { -80, 0, -80, 55, -10, 55 }, new(95, 56, 44, 255)));
                picture.AddShape(new Curve(new float[] { 80, 0, 80, 55, 10, 55 }, new(95, 56, 44, 255)));
                picture.AddShape(new Curve(new float[] { 80, 0, 80, -55, 10, -55 }, new(95, 56, 44, 255)));
                picture.AddShape(new Curve(new float[] { -80, 0, -80, -55, -10, -55 }, new(95, 56, 44, 255)));


                //cheecks
                picture.AddShape(new Curve(new float[] { 180, 65, 70, 65, 70, -15 }, new(146, 60, 33, 255)));
                picture.AddShape(new Polygon(new float[]
                {
                    70, -10,
                    75, -10,
                    75, -20,
                    70, -20,
                }, new(146, 60, 33, 255)));
                picture.AddShape(new Curve(new float[] { 175, 60, 75, 65, 75, -20 }, new(227, 105, 19, 255)));
                picture.AddShape(new Curve(new float[] { 150, -95, 70, -95, 70, -15 }, new(146, 60, 33, 255)));
                picture.AddShape(new Curve(new float[] { 155, -90, 75, -95, 75, -10 }, new(227, 105, 19, 255)));

                
                picture.AddShape(new Curve(new float[] { -180, 65, -70, 65, -70, -15 }, new(146, 60, 33, 255)));
                picture.AddShape(new Polygon(new float[]
                {
                    -70, -10,
                    -75, -10,
                    -75, -20,
                    -70, -20,
                }, new(146, 60, 33, 255)));
                picture.AddShape(new Curve(new float[] { -175, 60, -75, 65, -75, -20 }, new(227, 105, 19, 255)));
                picture.AddShape(new Curve(new float[] { -150, -95, -70, -95, -70, -15 }, new(146, 60, 33, 255)));
                picture.AddShape(new Curve(new float[] { -155, -90, -75, -95, -75, -10 }, new(227, 105, 19, 255)));

                //left hand
                picture.AddShape(new Curve(new float[] { -280, 0, -240, 50, -230, -10 }, new(146, 60, 33, 255)));
                picture.AddShape(new Polygon(new float[] { -280, 0, -230, -10, -240, -100, -310, -200 }, new(146, 60, 33, 255)));
                picture.AddShape(new Curve(new float[] { -280, 0, -340, -100, -310, -200 }, new(146, 60, 33, 255)));
                picture.AddShape(new Curve(new float[] { -310, -200, -300, -240, -200, -240, -240, -100}, new(146, 60, 33, 255)));

                picture.AddShape(new Curve(new float[] { -275, -5, -240, 40, -235, -15 }, new(227, 105, 19, 255)));
                picture.AddShape(new Polygon(new float[] { -275, -5, -235, -15, -245, -95, -305, -195 }, new(227, 105, 19, 255)));
                picture.AddShape(new Curve(new float[] { -275, -5, -335, -95, -305, -195 }, new(227, 105, 19, 255)));
                picture.AddShape(new Curve(new float[] { -305, -195, -295, -235, -205, -235, -245, -95 }, new(227, 105, 19, 255)));
                picture.AddShape(new BrokenLine(new float[] { -290, -220, -300, -180, -298, -170 }, new(146, 60, 33, 255), 5));
                picture.AddShape(new BrokenLine(new float[] { -260, -215, -265, -180, -263, -170 }, new(146, 60, 33, 255), 5));

                //right hand
                picture.AddShape(new Curve(new float[] { 280, 0, 240, 50, 230, -10 }, new(146, 60, 33, 255)));
                picture.AddShape(new Polygon(new float[] { 280, 0, 230, -10, 240, -100, 310, -200 }, new(146, 60, 33, 255)));
                picture.AddShape(new Curve(new float[] { 280, 0, 340, -100, 310, -200 }, new(146, 60, 33, 255)));
                picture.AddShape(new Curve(new float[] { 310, -200, 300, -240, 200, -240, 240, -100 }, new(146, 60, 33, 255)));

                picture.AddShape(new Curve(new float[] { 275, -5, 240, 40, 235, -15 }, new(227, 105, 19, 255)));
                picture.AddShape(new Polygon(new float[] { 275, -5, 235, -15, 245, -95, 305, -195 }, new(227, 105, 19, 255)));
                picture.AddShape(new Curve(new float[] { 275, -5, 335, -95, 305, -195 }, new(227, 105, 19, 255)));
                picture.AddShape(new Curve(new float[] { 305, -195, 295, -235, 205, -235, 245, -95 }, new(227, 105, 19, 255)));
                picture.AddShape(new BrokenLine(new float[] { 290, -220, 300, -180, 298, -170 }, new(146, 60, 33, 255), 5));
                picture.AddShape(new BrokenLine(new float[] { 260, -215, 265, -180, 263, -170 }, new(146, 60, 33, 255), 5));

                return picture;
            }
        }
    }
}
