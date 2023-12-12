using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.IO;
using System.Linq;


namespace Graph
{
    public static class FileWorker
    {


        const int WordCoordId = 0;
        const int CoordinateXId = 1;
        const int CoordinateYId = 2;
        const int coordinatePlaces = 3;

        public static MyGraph Read(string filePath)
        {
            List<Vertice> vertice = new List<Vertice>();
            List<Connection> connections = new List<Connection>();

            List<string[]> stringsVertices = File.ReadAllLines(filePath).Select(x => x.Split(";")).ToList();

            for (int i = 0; i < stringsVertices.Count; i++)
                vertice.Add(new Vertice());

            for (int i = 0; i < vertice.Count; i++)
            {
                
                for (int j = 0; j < vertice.Count; j++)
                {            
                    int lengthConnection = int.Parse(stringsVertices[i][j]);
                    if (lengthConnection != 0)
                    {
                        Connection newConn = new Connection(lengthConnection, vertice[i], vertice[j]);

                        if (!Connection.ConnectionRepeat(connections, newConn))
                        {
                            connections.Add(newConn);
                        }
                    }

                }
                Point point = new Point();
                point.X = double.Parse(stringsVertices[i][vertice.Count + CoordinateXId]);
                point.Y = double.Parse(stringsVertices[i][vertice.Count + CoordinateYId]);
                vertice[i].RectCenter = point;
            }
            return new MyGraph(vertice, connections);
        }


        
        public static void WriteToFile(MyGraph graph, string filePath)
        {

            int countOfVertice = graph.AllVertices.Count;
            List<string[]> stringsVertices = new List<string[]>();
            for (int i = 0; i < countOfVertice; i++)
            {
                string[] arrayConnectionAndCoordinate = new string[countOfVertice + coordinatePlaces];
                for (int j = 0; j < countOfVertice; j++)
                {
                    Connection checkConnection = null;
                    if (i == j) arrayConnectionAndCoordinate[j] = "0";
                    else checkConnection = Connection.SearchConnection(
                        Vertice.SearchVertice(graph.AllVertices[i].Id, graph.AllVertices), 
                        Vertice.SearchVertice(graph.AllVertices[j].Id, graph.AllVertices),
                        graph.Connections);

                    if (checkConnection != null) arrayConnectionAndCoordinate[j] = checkConnection.Length.ToString();
                    else arrayConnectionAndCoordinate[j] = "0";
                }
                stringsVertices.Add(arrayConnectionAndCoordinate);
            }
            int counter = 0;
            foreach(Vertice vertice in graph.AllVertices)
            {
                Point point = vertice.RectCenter;
                for (int j = 0; j < coordinatePlaces; j++)
                {
                    if (j == WordCoordId) stringsVertices[counter][countOfVertice + WordCoordId] = "Coordinates:";
                    else if (j == CoordinateXId) stringsVertices[counter][countOfVertice + CoordinateXId] = point.X.ToString();
                    else if (j == CoordinateYId) stringsVertices[counter][countOfVertice + CoordinateYId] = point.Y.ToString();
                    
                }
                counter++;
            }
            string[] str=stringsVertices.Select(x => string.Join(';',x)).ToArray();
            File.WriteAllLines(filePath,str);

        }

    }
}
