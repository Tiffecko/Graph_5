using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace Graph
{
    public class AddVerticeTool
    {


        private Ellipse NewVerticeEllipse;
        public static Ellipse SelectedEllipse;
        public static MyGraph MainGraph = MainWindow.MainGraph;
        static int LieIndentificator = 0;
        const int IndentFromCenter = 10;
        static int _counter = 0;
        public void rectMouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.MainCanvas.MouseMove -= newRectMouseMove;
            MainWindow.MainCanvas.MouseDown -= rectMouseDown;
            NewVerticeEllipse = null;
            SelectedEllipse = null;
        }

        public void newRectMouseMove(object sender, MouseEventArgs e)
        {


            if (_counter == 0) _counter = MainGraph.AllVertices.Count;
            Point point = new Point();
            double mouseX = e.GetPosition(MainWindow.MainCanvas).X;
            double mouseY = e.GetPosition(MainWindow.MainCanvas).Y;

            if (NewVerticeEllipse == null)
            {
                Vertice vertice = new Vertice();
                NewVerticeEllipse = new Ellipse();
                NewVerticeEllipse.Width = 50;
                NewVerticeEllipse.Height = 50;
                NewVerticeEllipse.Fill = new SolidColorBrush(Colors.LightBlue);
                NewVerticeEllipse.Stroke = new SolidColorBrush(Colors.Black);
                NewVerticeEllipse.MouseMove += RectangleMouseMove;
                Canvas.SetZIndex(NewVerticeEllipse, 2);
                MainWindow.MainCanvas.Children.Add(NewVerticeEllipse);
                point.X = mouseX;
                point.Y = mouseY;
                vertice.Ellipse = NewVerticeEllipse;
                MainGraph.AllVertices.Add(vertice);
                vertice.RectCenter = point;

                TextBlock textBlock = new TextBlock() { Text = (vertice.Id + 1).ToString()};

                textBlock.Height = 20;
                textBlock.Width = 50;
                textBlock.VerticalAlignment = VerticalAlignment.Top;
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                textBlock.TextAlignment = TextAlignment.Center;
                Canvas.SetZIndex(textBlock, 2);
                MainWindow.MainCanvas.Children.Add(textBlock);
                Canvas.SetTop(textBlock, mouseY + IndentFromCenter);
                Canvas.SetLeft(textBlock, mouseX - NewVerticeEllipse.Height / 2);
                vertice.NameTextBlock = textBlock;
            }

            Canvas.SetLeft(NewVerticeEllipse, mouseX - NewVerticeEllipse.Width / 2);
            Canvas.SetTop(NewVerticeEllipse, mouseY - NewVerticeEllipse.Height / 2);
            TextBlock text = Vertice.SearchVertice(NewVerticeEllipse, MainGraph.AllVertices).NameTextBlock;
            Canvas.SetTop(text, mouseY +  IndentFromCenter);
            Canvas.SetLeft(text, mouseX - NewVerticeEllipse.Width / 2);
        }

        public void RectangleMouseMove(object sender, MouseEventArgs e)
        {
            double mouseX = e.GetPosition(MainWindow.MainCanvas).X;
            double mouseY = e.GetPosition(MainWindow.MainCanvas).Y;
            selectedRectangle(MainWindow.MainCanvas);
            DrawSelection();
            if (SelectedEllipse != null && e.LeftButton == MouseButtonState.Pressed)
            {
                Vertice vertice = Vertice.SearchVertice(SelectedEllipse, MainGraph.AllVertices);
                if (vertice != null)
                {
                    Point point = new Point();
                    point.X = mouseX;
                    point.Y = mouseY;
                    Canvas.SetLeft(SelectedEllipse, mouseX - SelectedEllipse.Width / 2);
                    Canvas.SetTop(SelectedEllipse, mouseY - SelectedEllipse.Height / 2);


                    vertice.RectCenter = point;
                    Canvas.SetTop(vertice.NameTextBlock, mouseY + IndentFromCenter);
                    Canvas.SetLeft(vertice.NameTextBlock, mouseX - SelectedEllipse.Width / 2);
                    MainGraph = MainWindow.MainGraph;
                    DrawHelper.DrawConnections(MainWindow.MainCanvas, MainGraph);
                }
            }
        }

        public static void clearSelection(object sender, MouseEventArgs e)
        {
            foreach (Vertice vertice in MainGraph.AllVertices)
            {
                if (!vertice.Ellipse.IsMouseOver)
                {
                    vertice.Ellipse.Effect = null;
                    if (vertice.Ellipse == SelectedEllipse) SelectedEllipse = null;
                }
            }
        }

        private void selectedRectangle(Canvas canvas)
        {
            foreach (var elem in canvas.Children)
            {
                if (elem is Ellipse ellipse)
                {
                    if (ellipse.IsMouseOver)
                    {
                        SelectedEllipse = ellipse;
                    }
                }
            }
        }

        private void DrawSelection()
        {
            if (SelectedEllipse != null)
                SelectedEllipse.Effect = new DropShadowEffect() { Color = Colors.Green };
        }
    }
}
