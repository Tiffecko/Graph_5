using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace Graph
{
    public class Connection
    {
        public int Length { get; set; }
        public Vertice Vertice1 { get; set; }
        public Vertice Vertice2 { get; set; }
        public Polyline Line { get; set; }

        public TextBlock BlockText { get; set; }

            
        public Connection()
        {
            this.BlockText = new TextBlock() { Text="1"};
            BlockText.MouseDown += ChangeDataTool.TextBlock_MouseDown;

        }            

        public Connection(int length, Vertice vertice1, Vertice vertice2)
        {
            this.Length = length;
            Vertice1 = vertice1;
            Vertice2 = vertice2;
            this.BlockText = new TextBlock() { Text = "1" };
            this.BlockText.Text = length.ToString();
            BlockText.MouseDown += ChangeDataTool.TextBlock_MouseDown;
        }

        public static bool ConnectionRepeat(List<Connection> connections, Connection checkConnection)
        {
            foreach (Connection connection in connections)
            {
                if ((connection.Vertice1.Id == checkConnection.Vertice1.Id && connection.Vertice2.Id == checkConnection.Vertice1.Id)
                    || (connection.Vertice1.Id == checkConnection.Vertice2.Id && connection.Vertice2.Id == checkConnection.Vertice1.Id)) return true;
            }
            return false;
        }

        public static Connection SearchConnection(Polyline line, List<Connection> connectionsWhereSeadch)
        {
            foreach (Connection connect in connectionsWhereSeadch)
            {
                if (connect.Line == line) return connect;
            }
            return null;
        }

        public static Connection SearchConnection(TextBlock textBlock, List<Connection> connectionsWhereSeadch)
        {
            foreach (Connection connect in connectionsWhereSeadch)
            {
                if (connect.BlockText == textBlock) return connect;
            }
            return null;
        }

        public static Connection SearchConnection(Vertice vertice1, Vertice vertice2, List<Connection> connectionsWhereSeadch)
        {
            foreach (Connection connection in connectionsWhereSeadch)
            {
                if ((connection.Vertice1.Id == vertice1.Id && connection.Vertice2.Id == vertice2.Id)
                    || (connection.Vertice2.Id == vertice1.Id && connection.Vertice1.Id == vertice2.Id))
                {
                    return connection;
                }
            }
            return null;
        }
        public static Connection SearchConnection(int Id1, int Id2, List<Connection> connectionsWhereSeadch)
        {
            foreach (Connection connection in connectionsWhereSeadch)
            {
                if ((connection.Vertice1.Id == Id1 && connection.Vertice2.Id == Id2)
                    || (connection.Vertice2.Id == Id1 && connection.Vertice1.Id == Id2))
                {
                    return connection;
                }
            }
            return null;
        }
    }
}
