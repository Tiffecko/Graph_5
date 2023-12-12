using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Graph
{
    public class Traversal
    {
        public static void DepthTraversal(MyGraph graph)//я хз, надо тестить
        {

            Logger logger = new Logger();
            logger.AddLine("Начат алгоритм обхода в глубину.");
            List<Vertice> verticesInOrderOfVisit = new List<Vertice>();
            Stack<Vertice> stack = new Stack<Vertice>();
            List<Vertice> notVisitedV = new List<Vertice>(graph.AllVertices);
            notVisitedV.Reverse();
            logger.AddLine($"В списке непосещённых вершин\nнаходятся все вершины.\nсостояние нашего списка:");
            foreach (Vertice i in notVisitedV)
            {
                logger.AddLine($"{i.NameTextBlock.Text}");
            }
            logger.AddLine($"Добавляем в стек вершину с номером {graph.AllVertices[0].NameTextBlock.Text}.");

            stack.Push(graph.AllVertices[0]);

            while (stack.Count != 0)
            {
                Vertice vert = stack.Pop();
                Vertice[] arrVert = stack.ToArray();
                logger.AddLine($"Достаём из стека вершину с номером {vert.NameTextBlock.Text}.\nсостояние нашего стека:");
                foreach (Vertice i in arrVert)
                {
                    logger.AddLine($"{i.NameTextBlock.Text}");
                }

                if (!verticesInOrderOfVisit.Contains(vert)) verticesInOrderOfVisit.Add(vert);
                logger.AddLine($"Обходим все соединения связывающие вершину {vert.NameTextBlock.Text}.\nДобавляем в стек все вершины из списка\nнепосещеных связанных с {vert.NameTextBlock.Text}.");
                foreach (Vertice vertice in notVisitedV)
                {
                    if (Connection.SearchConnection(vertice, vert, graph.Connections) != null)
                    {
                        logger.AddLine($"Добавляем в стек вершину с номером {vertice.NameTextBlock.Text}.");
                        stack.Push(vertice);

                    }
                }
                arrVert = stack.ToArray();
                logger.AddLine($"состояние нашего стека: ");
                foreach (Vertice i in arrVert)
                {
                    logger.AddLine($"{i.NameTextBlock.Text}");
                }
                logger.AddLine($"Удаляем из списка непосещённых вершин\nвершину с номером {vert.NameTextBlock.Text}.");
                notVisitedV.Remove(vert);
                logger.AddLine($"состояние нашего списка:");
                foreach (Vertice i in notVisitedV)
                {
                    logger.AddLine($"{i.NameTextBlock.Text}");
                }
                if (!verticesInOrderOfVisit.Contains(vert)) verticesInOrderOfVisit.Add(vert);
            }
            logger.AddLine("Вершины в порядке посещения");
            foreach(Vertice vertice in verticesInOrderOfVisit)
            {
                logger.AddLine(vertice.NameTextBlock.Text);
            }
            logger.AddLine("Алгорит обхода в глубину завершён");
            logger.AddLine(String.Empty);



            

            AnimaztionPainter animaztionPainter = new AnimaztionPainter(AnimaztionPainter.AlgorithmType.Traversal);
            List<Connection> connInOrderOfVisit = new List<Connection>();
            animaztionPainter.Shapes.Add(verticesInOrderOfVisit[0].Ellipse);
            for (int i = 1; i < verticesInOrderOfVisit.Count; i++)
            {
                int index = i - 1;
                Vertice vert = verticesInOrderOfVisit[i - 1];
                Vertice vert2 = verticesInOrderOfVisit[i];
                Connection c;
                do
                {
                    index--;
                    c = Connection.SearchConnection(vert, vert2, graph.Connections);
                    if (index > 0) vert = verticesInOrderOfVisit[index];
                } while (c == null && index >= 0);
                connInOrderOfVisit.Add(c);
                animaztionPainter.Shapes.Add(c.Line);
                animaztionPainter.Shapes.Add(vert2.Ellipse);
            }


            Logger.ShowAllLogToUser(MainWindow.MainCanvas, MainWindow.MainGrid);
            animaztionPainter.ShowAnimation();
        }

        public static void WidthTraversal(MyGraph graph)//аналогично - тестить
        {
            Logger logger = new Logger();
            AnimaztionPainter animaztionPainter = new AnimaztionPainter(AnimaztionPainter.AlgorithmType.Traversal);
            logger.AddLine("Начат алгоритм обхода в ширину.");
            logger.AddLine("В списке непосещённых вершин\nнаходятся все вершины.");
            List<Vertice> visitedV = new List<Vertice>();
            List<Vertice> notVisitedV = new List<Vertice>(graph.AllVertices);
            logger.AddLine($"состояние списка непосещенных вершин:");
            foreach (Vertice i in notVisitedV)
            {
                logger.AddLine($"{i.NameTextBlock.Text}");
            }
            int vertCount = notVisitedV.Count;
            logger.AddLine($"Удаляем из списка непосещённых вершин {graph.AllVertices[0].NameTextBlock.Text}");
            logger.AddLine("так как с неё начинается обход.");
            visitedV.Add(graph.AllVertices[0]);
            notVisitedV.Remove(graph.AllVertices[0]);
            logger.AddLine($"состояние списка непосещенных вершин:");
            foreach (Vertice i in notVisitedV)
            {
                logger.AddLine($"{i.NameTextBlock.Text}");
            }
            animaztionPainter.Shapes.Add(graph.AllVertices[0].Ellipse);
            Vertice verticeNow = graph.AllVertices[0];
            Queue<Vertice> qVertice = new Queue<Vertice>();
            while (visitedV.Count != vertCount)
            {
                logger.AddLine($"Проходим по всем соединениям связывающими {verticeNow.NameTextBlock.Text}.\nДобавляем в очередь все вершины из списка\nнепосещеных связанных с {verticeNow.NameTextBlock.Text}.");
                //List<Connection> connections = new List<Connection>();
                foreach (Vertice vert in notVisitedV)
                {
                    Connection conn = Connection.SearchConnection(verticeNow, vert, graph.Connections);
                    if (conn != null && !(animaztionPainter.Shapes.Contains(vert.Ellipse) && animaztionPainter.Shapes.Contains(verticeNow.Ellipse)))
                    {
                        //connections.Add(conn);
                        if (!animaztionPainter.Shapes.Contains(conn.Line)) animaztionPainter.Shapes.Add(conn.Line);
                        if (!animaztionPainter.Shapes.Contains(vert.Ellipse)) animaztionPainter.Shapes.Add(vert.Ellipse);
                        if (!visitedV.Contains(vert)) visitedV.Add(vert);
                        logger.AddLine($"Добавляем в очередь вершину {vert.NameTextBlock.Text}.");
                        logger.AddLine($"состояние очереди:");
                        qVertice.Enqueue(vert);
                        foreach (Vertice i in qVertice)
                        {
                            logger.AddLine($"{i.NameTextBlock.Text}");
                        }
                    }
                }

                Vertice vertice = qVertice.Dequeue();
                verticeNow = vertice;
                logger.AddLine($"удаляем вершину {vertice.NameTextBlock.Text} из очереди\nи удаляем ёё из списка непосещённых вершин.");
                notVisitedV.Remove(vertice);
                logger.AddLine($"состояние списка непосещенных вершин:");
                foreach (Vertice i in notVisitedV)
                {
                    logger.AddLine($"{i.NameTextBlock.Text}");
                }
                logger.AddLine($"состояние очереди:");
                foreach (Vertice i in qVertice)
                {
                    logger.AddLine($"{i.NameTextBlock.Text}");
                }
            }

            logger.AddLine("Вершины в порядке посещения");
            foreach (Vertice vertice in visitedV)
            {
                logger.AddLine(vertice.NameTextBlock.Text);
            }

            logger.AddLine("Алгорит обхода в ширину завершён.");
            logger.AddLine(String.Empty);

            Logger.ShowAllLogToUser(MainWindow.MainCanvas, MainWindow.MainGrid);
            animaztionPainter.ShowAnimation();
        }

    }
}