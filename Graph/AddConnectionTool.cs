using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Graph
{
    public class AddConnectionTool
    {
        //public static List<Connection> Connections { get; set; }
        public static Connection NewConnection { get; set; }
        private static Ellipse Ellipse1 = null;
        private static Ellipse Ellipse2 = null;
        static MyGraph MainGraph=MainWindow.MainGraph;

        public AddVerticeTool ToolAddVertice = MainWindow.ToolAddVertice;

        public static void addConnection(object sender, MouseEventArgs e)
        {
            
            Vertice data1, data2;
            data1 = data2 = null;
            NewConnection = new Connection();
            if (AddVerticeTool.SelectedEllipse != null)
            {
                if (Ellipse1 == null&& e.LeftButton == MouseButtonState.Pressed)
                {
                    Ellipse1 = AddVerticeTool.SelectedEllipse;
                }
                else if (Ellipse1!=null&&!Ellipse1.Equals(AddVerticeTool.SelectedEllipse) && e.LeftButton == MouseButtonState.Pressed)
                {
                    Ellipse2 = AddVerticeTool.SelectedEllipse;
                    NewConnection.Vertice1 = Vertice.SearchVertice(Ellipse1, MainGraph.AllVertices);
                    NewConnection.Vertice2 = Vertice.SearchVertice(Ellipse2, MainGraph.AllVertices);
                    NewConnection.Length = 1;

                    if (!Connection.ConnectionRepeat(MainGraph.Connections, NewConnection))
                    {
                        MainGraph.Connections.Add(NewConnection);
                        Ellipse1 = Ellipse2 = null;
                    }
                    MainGraph = MainWindow.MainGraph;
                    DrawHelper.DrawConnections(MainWindow.MainCanvas, MainGraph);
                    
                    MainWindow.MainCanvas.MouseDown -= addConnection;
                }
            }
            MainWindow.MainCanvas.MouseDown += MainWindow.ToolAddVertice.rectMouseDown;
        }

        public static bool IsConnectionNewTest(Connection connectionToTest)
        {
            foreach (Connection connection in MainGraph.Connections)
            {
                if ((connection.Vertice1.Id == connectionToTest.Vertice1.Id && connection.Vertice2.Id == connectionToTest.Vertice2.Id)
                    || (connection.Vertice1.Id == connectionToTest.Vertice2.Id && connection.Vertice2.Id == connectionToTest.Vertice1.Id))
                {
                    return false;
                }
            }
            return true;
        }

       
    }
}
