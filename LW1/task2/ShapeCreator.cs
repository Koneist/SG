using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace task2
{
    internal static class ShapeCreator
    {
        public static Ellipse CreateEllipse(Brush fillColor, Brush strokeColor)
            //float top, float left, float width, float height, int rotation ,Brush FillColor, Brush StrokeColor)
        {
            var ellipse = new Ellipse();
            ellipse.Fill = fillColor;
            ellipse.Stroke = strokeColor;
            
            
            return ellipse;
        }

        public static Polygon CreateTriangle(float VertexOffset, Brush fillColor, Brush strokeColor)
        {
            var triangle = new Polygon();

            Point startPoint = new(0, 1);
            Point endPoint = new(1, 1);
            Point VertexPoint = new(VertexOffset, 0);
            
            triangle.Points.Add(startPoint);
            triangle.Points.Add(VertexPoint);
            triangle.Points.Add(endPoint);

            triangle.Stretch = Stretch.Fill;
            triangle.Fill = fillColor;
            triangle.Stroke = strokeColor;
            triangle.StrokeThickness = 2;

            return triangle;
        }

        public static Rectangle CreateRectangle(Brush fillColor, Brush strokeColor)
        {
            var rectangle = new Rectangle();
            rectangle.Fill = fillColor;
            rectangle.Stroke = strokeColor;
            rectangle.StrokeThickness = 2;

            return rectangle;
        }

        public static Polygon CreatePolygon(Brush fillColor, Brush strokeColor, params Point[] points)
        {
            var polygon = new Polygon();

            polygon.Points = new PointCollection(points);

            polygon.Stretch = Stretch.Fill;
            polygon.Fill = fillColor;
            polygon.Stroke = strokeColor;
            polygon.StrokeThickness = 2;

            return polygon;
        }

        public static void SetTransform(this Shape shape, float top, float left, float width, float height, int angle)
        {
            shape.Width = width;
            shape.Height = height;
            shape.RenderTransform = new RotateTransform(angle, width / 2, height / 2);
            shape.StrokeThickness = 2;

            Canvas.SetTop(shape, top);
            Canvas.SetLeft(shape, left);
        }
    }
}
