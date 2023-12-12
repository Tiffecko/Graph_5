using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Graph
{
    class PrimAlghoritm
    {
        public static List<Connection> AlgorithmByPrim(MyGraph graph)
        {
            AnimaztionPainter animaztionPainter = new AnimaztionPainter(AnimaztionPainter.AlgorithmType.Prim);

            List<Connection> usedE = new List<Connection>();
            //неиспользованные ребра
            List<Connection> notUsedE = new List<Connection>(graph.Connections);

            //использованные вершины
            List<Vertice> usedV = new List<Vertice>();
            //неиспользованные вершины
            List<Vertice> notUsedV = new List<Vertice>(graph.AllVertices);

            Logger logger = new Logger();

            int numberV=notUsedV.Count;

            logger.AddLine("Начат алгоритм Прима");
            //выбираем случайную начальную вершину
            Random rand = new Random();
            int randomNumber =rand.Next(0, numberV);
            Vertice randVertice = notUsedV[randomNumber];
            logger.AddLine($"Выбираем случайную начальную вершину\nНа этот раз это вершина {randVertice.NameTextBlock.Text}");
            usedV.Add(randVertice);
            animaztionPainter.Shapes.Add(randVertice.Ellipse);
            notUsedV.Remove(randVertice);
            logger.AddLine($"Удалим вершину {randVertice.NameTextBlock.Text} из списка непосещённых");
            while (notUsedV.Count > 0)
            {
                int minE = -1; //номер наименьшего ребра
                               //поиск наименьшего ребра

                for (int i = 0; i < notUsedE.Count; i++)
                {
                    if ((usedV.IndexOf(notUsedE[i].Vertice1) != -1) && (notUsedV.IndexOf(notUsedE[i].Vertice2) != -1) ||
                        (usedV.IndexOf(notUsedE[i].Vertice2) != -1) && (notUsedV.IndexOf(notUsedE[i].Vertice1) != -1))
                    {
                        if (minE != -1)
                        {
                            if (notUsedE[i].Length < notUsedE[minE].Length)
                                minE = i; 
                        }
                        else
                        {
                            minE = i;
                        }
                    }
                }

                logger.AddLine($"Ищем минимальное ребро такое, что\nодин конец уже взятая вершина другой ещё нетт\n" +
                    $"На этот раз это ребро между {notUsedE[minE].Vertice1.NameTextBlock.Text} и {notUsedE[minE].Vertice2.NameTextBlock.Text}");
                animaztionPainter.Shapes.Add(notUsedE[minE].Line);

                //заносим новую вершину в список использованных и удаляем ее из списка неиспользованных
                if (usedV.IndexOf(notUsedE[minE].Vertice1) != -1)
                {
                    logger.AddLine($"Добавляем невыделенную вершину,то есть {notUsedE[minE].Vertice2.NameTextBlock.Text}");
                    usedV.Add(notUsedE[minE].Vertice2);
                    animaztionPainter.Shapes.Add(notUsedE[minE].Vertice2.Ellipse);
                    notUsedV.Remove(notUsedE[minE].Vertice2);
                }
                else
                {
                    logger.AddLine($"Добавляем невыделенную вершину, то есть {notUsedE[minE].Vertice1.NameTextBlock.Text}");
                    usedV.Add(notUsedE[minE].Vertice1);
                    animaztionPainter.Shapes.Add(notUsedE[minE].Vertice1.Ellipse);
                    
                    notUsedV.Remove(notUsedE[minE].Vertice1);
                }
                //заносим новое ребро в дерево и удаляем его из списка неиспользованных
                usedE.Add(notUsedE[minE]);
                notUsedE.RemoveAt(minE);
            }

            Logger.ShowAllLogToUser(MainWindow.MainCanvas, MainWindow.MainGrid);
            animaztionPainter.ShowAnimation();
            //Animize
            MainWindow.IsUserCanUseButtons = true;
            logger.AddLine("Алгоритм Прима завершён\n");
            
            return usedE;
        }

        
    }
}
