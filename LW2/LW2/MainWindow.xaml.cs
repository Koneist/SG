using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace LW2
{
    enum EncoderType
    {
        None,
        JPG,
        BMP
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private Vector relativeMousePos;
        private FrameworkElement draggedObject;

        private InkCanvas? _drawCanvas = null;

        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            SetSizeWindow setSizeWindow = new();
            if (setSizeWindow.ShowDialog() == true)
            {
                DragArea.Children.Remove(_drawCanvas);

                _drawCanvas = new();

                SetInkCanvasSize(_drawCanvas, 
                    setSizeWindow.CanvasWidth, setSizeWindow.CanvasHeight);
                
                _drawCanvas.MouseLeftButtonDown += StartDrag;
                DragArea.Children.Add(_drawCanvas);
            }
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.BMP;*.JPG)|*.BMP;*.JPG";

            if (openFileDialog.ShowDialog() == true)
            {
                DragArea.Children.Remove(_drawCanvas);

                _drawCanvas = new();
                _drawCanvas.MouseLeftButtonDown += StartDrag;
                
                var uri = new Uri(openFileDialog.FileName);
                
                BitmapImage newBitmap = new();
                newBitmap.BeginInit();
                newBitmap.UriSource = uri;
                newBitmap.CacheOption = BitmapCacheOption.OnLoad;
                newBitmap.EndInit();

                Image newImage = new();
                newImage.Source = newBitmap;

                SetInkCanvasSize(_drawCanvas, newBitmap.Width, newBitmap.Height);

                _drawCanvas.Children.Add(newImage);
                DragArea.Children.Add(_drawCanvas);
            }
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            if (_drawCanvas == null)
                return;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JPG Image (*.JPG)|*.JPG|BMP Image (*.BMP)|*.BMP";
            if (saveFileDialog.ShowDialog() == true)
            {
                var path = saveFileDialog.FileName;
                var encoderType = GetBitmapEncoderType(path);
                BitmapEncoder? encoder;
                
                if ((encoder = GetBitmapEncoder(encoderType)) == null)
                    return;

                SaveFile(path, _drawCanvas, encoder);
                    
            }
        }

        private BitmapEncoder? GetBitmapEncoder(EncoderType type)
        {
            switch(type)
            {
                case EncoderType.JPG:
                    return new JpegBitmapEncoder();
                case EncoderType.BMP:
                    return new BmpBitmapEncoder();
            }
            return null;
        }

        private EncoderType GetBitmapEncoderType(string path)
        {
            var extansion = Path.GetExtension(path).ToUpper();
            if(extansion == ".JPG")
                return EncoderType.JPG;

            if (extansion == ".BMP")
                return EncoderType.BMP;

            return EncoderType.None;
        }

        private void SaveFile(string path, InkCanvas canvas, BitmapEncoder encoder)
        {
            var rect = new Rect(canvas.RenderSize);
            var visual = new DrawingVisual();
            
            using (var dc = visual.RenderOpen())
            {
                dc.DrawRectangle(new VisualBrush(canvas), null, rect);
            }

            RenderTargetBitmap bitmap = new(
            (int)rect.Width, (int)rect.Height, 96, 96, PixelFormats.Default);
            
            bitmap.Render(visual);

            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            
            using (var file = File.OpenWrite(path))
            {
                encoder.Save(file);
            }

        }

        private void SetInkCanvasSize(InkCanvas canvas, double width, double height)
        {
            canvas.MaxHeight = height;
            canvas.MaxWidth = width;
            canvas.Height = height;
            canvas.Width = width;
        }

        private void StartDrag(object sender, MouseButtonEventArgs e)
        {
            draggedObject = (FrameworkElement)sender;
            relativeMousePos = e.GetPosition(draggedObject) - new Point();
            draggedObject.MouseMove += OnDragMove;
            draggedObject.LostMouseCapture += OnLostCapture;
            draggedObject.MouseUp += OnMouseUp;
            Mouse.Capture(draggedObject);

        }

        void OnDragMove(object sender, MouseEventArgs e)
        {
            UpdatePosition(e);
        }

        void UpdatePosition(MouseEventArgs e)
        {
            var point = e.GetPosition(DragArea);
            var newPos = point - relativeMousePos;
            Canvas.SetLeft(draggedObject, newPos.X);
            Canvas.SetTop(draggedObject, newPos.Y);
        }
        void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            FinishDrag(sender, e);
            Mouse.Capture(null);
        }

        void OnLostCapture(object sender, MouseEventArgs e)
        {
            FinishDrag(sender, e);
        }

        void FinishDrag(object sender, MouseEventArgs e)
        {
            draggedObject.MouseMove -= OnDragMove;
            draggedObject.LostMouseCapture -= OnLostCapture;
            draggedObject.MouseUp -= OnMouseUp;
            UpdatePosition(e);
        }

        private void CanvasMove_Click(object sender, RoutedEventArgs e)
        {
            if (_drawCanvas == null)
                return;

            _drawCanvas.EditingMode = InkCanvasEditingMode.None;
        }

        private void CanvasDraw_Click(object sender, RoutedEventArgs e)
        {
            if(_drawCanvas == null)
                return;

            _drawCanvas.EditingMode = InkCanvasEditingMode.Ink;
        }


    }
}
