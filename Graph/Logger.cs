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
    class Logger
    {
        static TextBlock LoggerTextBlock = new TextBlock();

        static StringBuilder sb = new StringBuilder();
        
        public static void ClearLogger()
        {
            sb.Clear();
            LoggerTextBlock.Text = "";
            start = 0;
        }

        static Canvas _canvas;
        static double FullTextHeigh = 0; 
        static double NowTextHeigh = 0;


        public void AddText(string text)
        {                  
            sb.Append(text);
        }

        public void AddLine(string text)
        {
            sb.Append(text);
            sb.Append("\n");
            
        }


        static int start = 0;
        static int end = 0;
        static int amountOfStrOnTextBlock;
        static string[] Allstrings;


        //Logger.ShowAllLogToUser(MainWindow.MainCanvas, MainWindow.MainGrid);
        public static void ShowAllLogToUser(Canvas canvas, Grid grid)
        {

            MainWindow.IsUserCanUseButtons = false;
            LoggerTextBlock.MouseWheel += LoggerTextBlock_MouseWheel;

            _canvas = canvas;
            

            Allstrings = sb.ToString().Split('\n');
            

            amountOfStrOnTextBlock = (int)(canvas.ActualHeight / 16.1);
            end = amountOfStrOnTextBlock;
            LoggerTextBlock.Height = canvas.ActualHeight - 5;

            FullTextHeigh = 16.1 * Allstrings.Length;
            NowTextHeigh = FullTextHeigh;

            LoggerTextBlock.Width = 290;

            LoggerTextBlock.Margin = new Thickness(0, 5, 1, 0);
            LoggerTextBlock.VerticalAlignment = VerticalAlignment.Top;
            LoggerTextBlock.HorizontalAlignment = HorizontalAlignment.Right;

            StringBuilder stringBuilder = new StringBuilder();
            for (int i = start; i < end; i++)
            {
                if (i >= Allstrings.Length) break;
                stringBuilder.Append(Allstrings[i]);
                stringBuilder.Append('\n');
            }
            LoggerTextBlock.Text= stringBuilder.ToString();


            if(!grid.Children.Contains(LoggerTextBlock)) 
                grid.Children.Add(LoggerTextBlock);                       
        }

        private static void BtnReturn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.IsUserCanUseButtons = true;
            //Remove text
            _canvas.Children.Remove(LoggerTextBlock);
            //_diff = 0;
            LoggerTextBlock.Margin = new Thickness(85, 5, 0, 0);
            start = 0;
            end = amountOfStrOnTextBlock;
            //LoggerTextBlock.MouseWheel -= LoggerTextBlock_MouseWheel;

        }


        private static int _diff;
        const int ChangeLength = 40;



        private static void LoggerTextBlock_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();


            if (NowTextHeigh > LoggerTextBlock.Height)
            {
                Thickness thickness = LoggerTextBlock.Margin;
                int delta = e.Delta;


                if (delta > 0 && start > 0)
                {
                    start -= amountOfStrOnTextBlock / 4;
                    end -= amountOfStrOnTextBlock / 4;
                    //delta = 120;
                    // The user scrolled up.
                    //thickness.Top += ChangeLength;
                    //_diff += ChangeLength;


                }
                else if (delta < 0 && end < Allstrings.Length)
                {
                    //delta = -120;
                    // The user scrolled down.
                    start += amountOfStrOnTextBlock / 4;
                    end += amountOfStrOnTextBlock / 4;
                }

                if (start < 0)
                {
                    start = 0;
                    end = amountOfStrOnTextBlock;
                }

                for (int i = start; i < end; i++)
                {
                    if (i >= Allstrings.Length) break;
                    stringBuilder.Append(Allstrings[i]);
                    stringBuilder.Append('\n');
                }
                LoggerTextBlock.Text = stringBuilder.ToString();

                LoggerTextBlock.Margin = thickness;

            }
        }
    }
}
