﻿using System.Windows;
using Math_lib;

namespace RasterizerTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();  
            
            Rasterizer r = new Rasterizer(1920, 1080, true, 18);

            //r.DrawLine(p1: new(0,0), p2: new(2, 0), c: System.Drawing.Color.Red, thickness: 1);
            //r.DrawLine(p1: new(0,0), p2: new(r.width - 1, r.height - 1), c: System.Drawing.Color.FromArgb(226, 7, 255), thickness: 10);
            //r.DrawLine(p1: new(0,r.height/2.0), p2: new(r.width - 1, r.height/2.0), c: System.Drawing.Color.Cyan, thickness: 10);
            //r.DrawLine(p1: new(r.width/2.0,0.0), p2: new(r.width/2.0, r.height - 1), c: System.Drawing.Color.Green, thickness: 10);

            //r.DrawCircle(p1: new(r.width/2.0, r.height/2.0), radius: 30, c: System.Drawing.Color.White, fill: true);
            //
            //r.DrawCircle(p1: new(0,0), radius: 30, c: System.Drawing.Color.White, fill: true);

            //r.DrawCircle(new(0,0), 0.5, System.Drawing.Color.White, true);
            //r.DrawLine(new(-1,-1), new(1,1), System.Drawing.Color.White);

            //r.DrawRectangle(new(-1, 1), new(1, -1), System.Drawing.Color.White, true);
            //r.DrawLine(new(-1, 1), new(1, -1), System.Drawing.Color.White);

            //r.DrawLine(new(0, 0), new(80, 8), System.Drawing.Color.White);

            Display.Source = r.GetSource();
        }
    }
}
