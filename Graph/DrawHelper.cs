using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Graph
{
    public class DrawHelper
    {
        const int LeftIndent = 100;
        const int TopIndent = 40;
        const int RectOnOneLine = 11;
        const int RectBetweenIndent = 60;
        const int TextBoxIndent = 35;
        public static void DrawGraph(Canvas canvas, MyGraph graph)
        {

            for (int i = 0; i < graph.AllVertices.Count; i++)
            {
                if(graph.AllVertices[i].Ellipse == null)
                {
                    Ellipse Ellipse = new Ellipse();
                    Ellipse.Width = 50;
                    Ellipse.Height = 50;
                    Ellipse.Fill = new SolidColorBrush(Colors.LightBlue);
                    Ellipse.Stroke = new SolidColorBrush(Colors.Black);
                    Ellipse.MouseMove += MainWindow.ToolAddVertice.RectangleMouseMove;



                    Vertice vert = graph.AllVertices[i];
                    vert.Ellipse = Ellipse;
                    Canvas.SetTop(Ellipse, vert.RectCenter.Y - Ellipse.Height / 2);
                    Canvas.SetLeft(Ellipse, vert.RectCenter.X - Ellipse.Width / 2);
                    canvas.Children.Add(Ellipse);
                    Canvas.SetZIndex(Ellipse, 2);



                    

                    TextBlock textBlock = new TextBlock() { Text = (graph.AllVertices[i].Id + 1).ToString() };
                    textBlock.Height = 20;
                    textBlock.Width = 50;
                    textBlock.VerticalAlignment = VerticalAlignment.Top;
                    textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                    textBlock.TextAlignment = TextAlignment.Center;
                    Canvas.SetZIndex(textBlock, 3);
                    vert.NameTextBlock = textBlock;
                    Canvas.SetTop(textBlock, vert.RectCenter.Y + TextBoxIndent - Ellipse.Height / 2);
                    Canvas.SetLeft(textBlock, vert.RectCenter.X - Ellipse.Width / 2);
                    canvas.Children.Add(textBlock);

                    
                }
                else
                {
                    Vertice vert = graph.AllVertices[i];
                    Canvas.SetTop(vert.Ellipse, vert.RectCenter.Y-vert.Ellipse.Height/2);
                    Canvas.SetLeft(vert.Ellipse, vert.RectCenter.X - vert.Ellipse.Width / 2);
                    canvas.Children.Add(vert.Ellipse);
                    Canvas.SetTop(vert.NameTextBlock, vert.RectCenter.Y + TextBoxIndent - vert.Ellipse.Height / 2);
                    Canvas.SetLeft(vert.NameTextBlock, vert.RectCenter.X - vert.Ellipse.Width / 2);
                    canvas.Children.Add(vert.NameTextBlock);
                }
            }
            DrawConnections(canvas, graph);
        }

        public static void DrawConnections(Canvas canvas, MyGraph graph)
        {
            foreach (Connection connection in graph.Connections)
            {
                if (connection.Line != null) canvas.Children.Remove(connection.Line); //удаление линии при перетаскивании прямоугольника
                Polyline line = new Polyline();
                PointCollection points = new PointCollection();
                Point point1 = connection.Vertice1.RectCenter;
                Point point2 = connection.Vertice2.RectCenter;
                double minX = point1.X < point2.X ? point1.X : point2.X;
                double minY = point1.Y < point2.Y ? point1.Y : point2.Y;
                Point textBlockCenter = new Point(minX + Math.Abs(point1.X - point2.X) / 2, minY + Math.Abs(point1.Y - point2.Y) / 2);
                if (connection.BlockText != null) canvas.Children.Remove(connection.BlockText);
                canvas.Children.Add(connection.BlockText);
                Canvas.SetZIndex(connection.BlockText, 0);
                Canvas.SetLeft(connection.BlockText, textBlockCenter.X);
                Canvas.SetTop(connection.BlockText, textBlockCenter.Y - 15);
                //connection.BlockText.VerticalAlignment =VerticalAlignment.Center;
                //connection.BlockText.HorizontalAlignment =HorizontalAlignment.Center;
                //connection.BlockText.Margin = new Thickness(textBlockCenter.X, textBlockCenter.Y, 800- textBlockCenter.X, 450- textBlockCenter.Y);
                points.Add(point1);
                points.Add(point2);
                line.Fill = Brushes.Black;
                line.Stroke = Brushes.Black;


                line.Points = points;
                Canvas.SetZIndex(line, 1);
                connection.Line = line;
                canvas.Children.Add(line);
            }
        }


        //public static void MoveGragh(Canvas canvas, MyGraph graph)
        //{

        //    foreach(Vertice vertice in graph.AllVertices)
        //    {
        //        if (vertice.RectCenter.Y < canvas.Height/3)
        //        {
        //            Point p = new Point(vertice.RectCenter.X,vertice.RectCenter.Y+canvas.Height/3);

        //            vertice.RectCenter=p;
        //        }
        //    }
        //    canvas.Children.Clear();
        //    DrawGraph(canvas, graph);
        //}

        public static Button BtnNext = new Button
        {
            Content = "Next",
            Height = 20,
            Width = 125,
            Background = new SolidColorBrush(Colors.Gray),
            Margin = new Thickness(5, 460, 0, 0),
            VerticalAlignment = VerticalAlignment.Top,
            HorizontalContentAlignment = HorizontalAlignment.Center

        };

        public static Button BtnReturn = new Button
        {
            Content = "To original graph",
            Height = 20,
            Width = 125,
            Background = new SolidColorBrush(Colors.Gray),
            Margin =  new Thickness(5, 490, 0, 0),
            VerticalAlignment = VerticalAlignment.Top,
            HorizontalContentAlignment = HorizontalAlignment.Center
        };


        public static TextBlock AnswerBlock = new TextBlock
        {
            Height = 40,
            Width = 200,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(10, 515, 0, 0)
        };

        public static Rectangle AnswerRect = new Rectangle
        {
            Height = 100,
            Width = 150,
            Fill = new SolidColorBrush(Colors.DarkGray),
            Margin = new Thickness(0, 450, 0, 0),
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top
        };

    }
}
