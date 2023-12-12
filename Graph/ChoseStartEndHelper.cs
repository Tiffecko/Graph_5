using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;


namespace Graph
{
    public class ChoseStartEndHelper
    {

        public static Vertice Start, End;
        public static MyGraph MainGraph=MainWindow.MainGraph;
        static TextBlock TextBlock = new TextBlock
        {
            Height = 20,
            Width = 200,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(200, 20, 0, 0)
        };

        public static Action<Vertice, Vertice, MyGraph> Function;

        public static void ChooseVertices(Action<Vertice, Vertice, MyGraph> func)
        {
            TextBlock.Text = "Выберите стартовую вершину";
            MainWindow.MainCanvas.Children.Add(TextBlock);
            Canvas.SetZIndex(TextBlock, 20);
            Function = func;
            MainWindow.MainCanvas.MouseDown += ChooseVertice_MouseDown;
            MainGraph = MainWindow.MainGraph;
        }

        private static void ChooseVertice_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Ellipse ellipse = AddVerticeTool.SelectedEllipse;
            if (ellipse != null)
            {
                if (Start == null)
                {
                    Start = Vertice.SearchVertice(ellipse, MainGraph.AllVertices);
                    TextBlock.Text = "Выберите конечную вершину";
                }
                else if (End == null && Start.Ellipse != ellipse)
                {
                    End = Vertice.SearchVertice(ellipse, MainGraph.AllVertices);
                    MainWindow.MainCanvas.MouseDown -= ChooseVertice_MouseDown;
                    MainWindow.IsUserCanUseButtons = true;
                    MainWindow.MainCanvas.Children.Remove(TextBlock);
                    Function.Invoke(Start, End,MainGraph);
                    Function = null;
                    Start = null;
                    End = null;
                    MainGraph = null;
                }
            }
            
        }
    }
}
