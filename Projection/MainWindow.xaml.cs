using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

using Math_lib;
using Microsoft.Win32;

namespace Projection {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: Window {

        private readonly DispatcherTimer _timer;
        private readonly ScanLineRasterizer      _rasterizer;
        private          int             _angle;
        private          Mesh3D          _mesh = new();

        private readonly Camera _camera;
        bool _wireframe;

        public MainWindow() {

            InitializeComponent();

            //Init
            _angle = 0;
            const int screenWidth = 1920;
            const int screenHeight = 1080;
            _camera = new Camera();

            //Load Mesh
            ShowOpenFile();

            //Init Rasterizer
            _rasterizer = new ScanLineRasterizer(screenWidth, screenHeight);

            //Render
            Render();
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(40),
                IsEnabled = true
            };

            _timer.Tick += OnRenderSzene;

        }

        private void ShowOpenFile()
        {
            var ofn = new OpenFileDialog
            {
                Filter = "Object files (*.obj)|*.obj",
            };
            if (ofn.ShowDialog() == true)
            {
                _mesh = Importer.Obj(ofn.FileName).CreateMesh();
            }
        }
        protected override void OnKeyDown(KeyEventArgs e) {
        
            if (e.Key == Key.P)
            {
                _timer.IsEnabled ^= true;
                e.Handled        =  true;
            }

            if (e.Key == Key.F)
            {
                _wireframe ^= true;
                e.Handled  =  true;
            }

            if(e.Key == Key.LeftCtrl)
            {
                _camera.Pos += new Point3D(0, 1, 0);
            }
            if (e.Key == Key.Space)
            {
                _camera.Pos = new Point3D(_camera.Pos - new Point3D(0,1,0));
            }

            Vector3D forward = _camera.LookDir * 1;
            if (e.Key == Key.W)
            {
                _camera.Pos += forward;
            }
            if (e.Key == Key.S)
            {
                _camera.Pos -= forward;
            }

            Vector3D cross = Vector3D.Cross(forward, new Vector3D(0, 1, 0));
            if (e.Key == Key.D)
            {
                _camera.Pos -= cross;
            }
            if (e.Key == Key.A)
            {
                _camera.Pos += cross;
            }

            if (e.Key == Key.Right)
            {
                _camera.Yaw -= 1;
            }
            if (e.Key == Key.Left)
            {
                _camera.Yaw += 1;
            }
        }
        
        private void OnRenderSzene(object sender, EventArgs e) {
            Render();
        }
        private void Render() {
            _rasterizer.Clear();

            RenderScene(
                mesh3DCube: _mesh, 
                angle: _angle, 
                r: _rasterizer, 
                c: _camera, 
                wireframe: _wireframe);

            Image.Source = _rasterizer.Bmp.ToImageSource();

            //Show DepthBuffer

            //int max = 0;
            //DirectBitmap bmp = new DirectBitmap(_rasterizer.Width, _rasterizer.Height);
            //for (int x = 0; x < _rasterizer.Width; x++)
            //{
            //    for (int y = 0; y < _rasterizer.Height; y++)
            //    {
            //        byte byteVal = 0;
            //        if (double.IsNegativeInfinity(_rasterizer.zb.At(x, y)))
            //        {
            //            byteVal = 255;

            //        }
            //        else
            //        {
            //            byteVal = Convert.ToByte((_rasterizer.zb.At(x, y) * 100) - 10);
            //            if (byteVal > max)
            //            {
            //                max = byteVal;
            //            }
            //        }
            //        bmp.SetPixel(x, y, Color.FromArgb(1, byteVal, byteVal, byteVal));
            //    }
            //}


            //Image.Source = bmp.ToImageSource();
        }

        private static void RenderScene(Mesh3D mesh3DCube, int angle, Rasterizer r, Camera c, bool wireframe) {

            //Init
            var wireframeRasterizer = new BresenhamRasterizer(r.Width, r.Height);

            var screenWidth  = r.Width;
            var screenHeight = r.Height;

            Matrix4x4 rotateZ = Matrix4x4.RotateZMarix(angle);
            Matrix4x4 rotateY = Matrix4x4.RotateYMarix(angle);
            Matrix4x4 rotateX = Matrix4x4.RotateXMarix(angle);

            Matrix4x4 projection = Matrix4x4.Projection(screenWidth, screenHeight, 100, 1, 1000);
            Matrix4x4 translation = Matrix4x4.Translation(0, 0, -6);

            Matrix4x4 worldMatrix = new Matrix4x4();
            worldMatrix = Matrix4x4.Identity();
            worldMatrix = worldMatrix * translation;
            worldMatrix = worldMatrix * rotateZ;
            worldMatrix = worldMatrix * rotateX;
            worldMatrix = worldMatrix * rotateY;

            //Camera
            c.Target = new Vector3D(0, 0, -1);
            Matrix4x4 cameraRotY = Matrix4x4.RotateYMarix((int)c.Yaw);
            c.LookDir = cameraRotY * c.Target;
            c.Target = new Vector3D(c.Pos + c.LookDir);

            Matrix4x4 viewMatrix = Matrix4x4.LookAt(c.Pos, c.Target, c.Up);

            //Draw
            foreach (Triangle3D tri in mesh3DCube.Triangles) {

                //Transform triangel
                Triangle3D triTranformed = new Triangle3D();
                triTranformed = worldMatrix * tri;

                //calc Normals
                Vector3D line1 = triTranformed.Points[1] - triTranformed.Points[0];
                Vector3D line2 = triTranformed.Points[2] - triTranformed.Points[0];

                Vector3D normal = Vector3D.UnitVector(Vector3D.Cross(line1, line2));

                //check visability
                if (Vector3D.Dot(normal, triTranformed.Points[0] - c.Pos) < 0)
                {
                    //Illumination
                    Vector3D lightDirection = new Vector3D(-.2,-.5,-1);
                    lightDirection = Vector3D.UnitVector(lightDirection);

                    double dp = Vector3D.Dot(normal, lightDirection);
                    var grayValue = Convert.ToByte(Math.Abs(dp * Byte.MaxValue));
                    var col = Color.FromArgb(255, grayValue, grayValue, grayValue);

                    //Convert to View Space
                    var triViewed = viewMatrix * triTranformed;

                    var clippedTris = Clipping.TriangleClipAgainstPlane(planeP: new(0, 0, 1), planeN: new(0, 0, 1), inTri: triViewed);
                    foreach (var clipped in clippedTris)
                    {
                        //project
                        Triangle3D triProjected = projection * clipped;

                        triProjected.Points[0] /= triProjected.Points[0].W;
                        triProjected.Points[1] /= triProjected.Points[1].W;
                        triProjected.Points[2] /= triProjected.Points[2].W;

                        //Convert from Triangle3 to Triangle2
                        Triangle3D triProjectedConv = triProjected;

                        //Scaling into screenspace
                        triProjectedConv += new Point3D(1, 1, 0);
                        triProjectedConv *= new Point3D(0.5 * screenWidth, 0.5 * screenHeight, 1);

                        var listTriangles = new List<Triangle3D>();
                        listTriangles.Add(triProjectedConv);
                        int nNewTris = 1;

                        //Clipping screen Edges
                        for(int p = 0; p < 4; p++)
                        {
                            
                            while(nNewTris > 0)
                            {
                                Triangle3D test = listTriangles[0];
                                listTriangles.RemoveAt(0);
                                nNewTris--;
                                var trisToAdd = p switch 
                                {
                                    //Clipping top
                                    0 => Clipping.TriangleClipAgainstPlane(new(x: 0, y: 0, z: 0), new(x: 0, y: 1, z: 0), test),
                                    //Clipping bottom
                                    1 => Clipping.TriangleClipAgainstPlane(new(x: 0, y: screenHeight - 1, z: 0), new(x: 0, y: -1, z: 0), test),
                                    //Clipping left
                                    2 => Clipping.TriangleClipAgainstPlane(new(x: 0, y: 0, z: 0), new(x: 1, y: 0, z: 0), test),
                                    //Clipping Right
                                    3 => Clipping.TriangleClipAgainstPlane(new(x: screenWidth - 1, y: 0, z: 0), new(x: -1, y: 0, z: 0), test),
                                    _ => Enumerable.Empty<Triangle3D>()
                                };

                                //Saving new Triangles
                                foreach(var t in trisToAdd)
                                {
                                    listTriangles.Add(t);
                                }
                            }
                            nNewTris = listTriangles.Count;
                        }

                        //Drawing
                        foreach (Triangle3D t in listTriangles)
                        {
                            if (wireframe)
                            {
                                wireframeRasterizer.DrawTriangle(t, Color.Red, false);
                            }
                            else
                            {
                                r.DrawTriangle(t, col, false);
                            }
                        }
                    }
                }       
            }
            if (wireframe)
            {
                r.OverrideBitmap(wireframeRasterizer.Bmp);
            }
        }
    }
}