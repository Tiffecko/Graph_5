using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class MyGraph
    {
        public List<Vertice> AllVertices;

        public List<Connection> Connections;

        public MyGraph(List<Vertice> vertices, List<Connection> connections)
        {
            AllVertices = vertices;
            Connections = connections;
        }

        public MyGraph(MyGraph graph)
        {
            AllVertices=graph.AllVertices;
            Connections=graph.Connections;
        }



    }
}
