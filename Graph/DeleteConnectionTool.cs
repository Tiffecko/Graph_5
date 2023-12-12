using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace Graph
{
    public class DeleteConnectionTool
    {
        static MyGraph MainGraph=MainWindow.MainGraph;
        public static Polyline SelectLine;
        private static void SelectedLine(Canvas canvas)
        {
            foreach (var elem in canvas.Children)
            {
                if (elem is Polyline line)
                {
                    if (line.IsMouseOver)
                    {
                        SelectLine = line;              
                        SelectLine.Effect = new DropShadowEffect() { Color = Colors.Blue };
                    }
                }
            }
        }


        public static void clearSelection(object sender, MouseEventArgs e)
        {
            SelectedLine(MainWindow.MainCanvas);
            foreach (Connection connect in MainGraph.Connections)
            {
                if (!connect.Line.IsMouseOver)
                {
                    connect.Line.Effect = null;
                    if (connect.Line == SelectLine) SelectLine = null;
                }
            }
        }

        public static void deleteLine(object sender, MouseEventArgs e)
        {
            
            if (SelectLine != null)
            {
                TextBlock textBlock = Connection.SearchConnection(SelectLine, MainGraph.Connections).BlockText;
                MainGraph.Connections.Remove(Connection.SearchConnection(SelectLine, MainGraph.Connections));
                MainWindow.MainCanvas.Children.Remove(SelectLine);
                MainWindow.MainCanvas.Children.Remove(textBlock);
                MainWindow.MainCanvas.MouseDown -= deleteLine;
            }
        }
    }
}
