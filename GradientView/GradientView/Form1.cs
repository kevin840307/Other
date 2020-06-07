using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GradientView
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            DrawF draw = new DrawF(50.0f, 5.0f, 5.0f);

            //draw.drawHistogram(e.Graphics, 10, 10);
            //DrawCoordinate(e.Graphics, draw);

            DrawBlock(e.Graphics, draw);
            //DrawGradient(e.Graphics, draw);
            //DrawSGD(e.Graphics, draw);
            //DrawMomentum(e.Graphics, draw);
            //DrawAdaGrad(e.Graphics, draw);
            //DrawRMSProp(e.Graphics, draw);
            DrawAdam(e.Graphics, draw);
        }

        private void DrawBlock(Graphics graphics, DrawF draw)
        {
            draw.drawBlock(graphics, 10.0f, 10.0f);
            draw.drawPoint(graphics, Brushes.Red, draw.Center);
        }

        private void DrawCoordinate(Graphics graphics, DrawF draw)
        {
            draw.drawCoordinate(graphics, 200.0f, 200.0f);
            draw.drawPoint(graphics, Brushes.Red, draw.Center);
        }

        private void DrawGradient(Graphics graphics, DrawF draw)
        {
            ArrayList xyPoints = new ArrayList();
            ArrayList uvPoints = new ArrayList();

            //float[] x = { -2, -1, 0, 1, -2, -1, 0, 1 };
            //float[] y = { -2, -2, -2, -2, 1, 1, 1, 1 };
            float[] x = { -2, -1, 0, 1, -2, -1, 0, 1 };
            float[] y = { -2, -2, -2, -2, 1, 1, 1, 1 };

            Function2 fun = new Function2();

            for (int index = 0; index < 8; index++)
            {
                PointF2D xyPoint = draw.getBlockPoint(x[index], y[index]);
                PointF2D uvPoint = Fun(fun, -x[index], -y[index]);
                Console.Write(uvPoint.X + " " + uvPoint.Y);
                xyPoints.Add(xyPoint);
                uvPoints.Add(uvPoint);
                draw.drawPoint(graphics, Brushes.Blue, xyPoint);
            }

            draw.drawGradients(graphics, xyPoints, uvPoints);
        }

        private void DrawSGD(Graphics graphics, DrawF draw)
        {
            ArrayList history = new ArrayList();
            PointF2D point = new PointF2D(-4.0f, 2.0f);
            Function2 fun = new Function2();
            SGD optimizer = new SGD(0.95f);

            for (int index = 0; index < 30; index++)
            {
                PointF2D xyPoint = draw.getBlockPoint(point.X, point.Y);
                history.Add(xyPoint);
                PointF2D diff = fun.DiffFormula(point.X, point.Y);
                optimizer.Update(point, diff);
            }

            PointF2D prePoint = ((PointF2D)history[0]);

            for (int index = 0; index < 30; index++)
            {
                draw.drawPoint(graphics, Brushes.Blue, ((PointF2D)history[index]));
                draw.drawLine(graphics, prePoint, ((PointF2D)history[index]));
                prePoint = ((PointF2D)history[index]);
            }
        }

        private void DrawMomentum(Graphics graphics, DrawF draw)
        {
            ArrayList history = new ArrayList();
            PointF2D point = new PointF2D(-4.0f, 2.0f);
            Function2 fun = new Function2();
            Momentum optimizer = new Momentum(0.07f, 0.9f);

            for (int index = 0; index < 30; index++)
            {
                PointF2D xyPoint = draw.getBlockPoint(point.X, point.Y);
                history.Add(xyPoint);
                PointF2D diff = fun.DiffFormula(point.X, point.Y);
                optimizer.Update(point, diff);
            }

            PointF2D prePoint = ((PointF2D)history[0]);

            for (int index = 0; index < 30; index++)
            {
                draw.drawPoint(graphics, Brushes.Blue, ((PointF2D)history[index]));
                draw.drawLine(graphics, prePoint, ((PointF2D)history[index]));
                prePoint = ((PointF2D)history[index]);
            }
        }

        private void DrawAdaGrad(Graphics graphics, DrawF draw)
        {
            ArrayList history = new ArrayList();
            PointF2D point = new PointF2D(-4.0f, 2.0f);
            Function2 fun = new Function2();
            AdaGrad optimizer = new AdaGrad(0.7f);

            for (int index = 0; index < 30; index++)
            {
                PointF2D xyPoint = draw.getBlockPoint(point.X, point.Y);
                history.Add(xyPoint);
                PointF2D diff = fun.DiffFormula(point.X, point.Y);
                optimizer.Update(point, diff);
            }

            PointF2D prePoint = ((PointF2D)history[0]);

            for (int index = 0; index < 30; index++)
            {
                draw.drawPoint(graphics, Brushes.Blue, ((PointF2D)history[index]));
                draw.drawLine(graphics, prePoint, ((PointF2D)history[index]));
                prePoint = ((PointF2D)history[index]);
            }
        }

        private void DrawRMSProp(Graphics graphics, DrawF draw)
        {
            ArrayList history = new ArrayList();
            PointF2D point = new PointF2D(-4.0f, 2.0f);
            Function2 fun = new Function2();
            RMSProp optimizer = new RMSProp(0.5f, 0.9f);

            for (int index = 0; index < 30; index++)
            {
                PointF2D xyPoint = draw.getBlockPoint(point.X, point.Y);
                history.Add(xyPoint);
                PointF2D diff = fun.DiffFormula(point.X, point.Y);
                optimizer.Update(point, diff);
            }

            PointF2D prePoint = ((PointF2D)history[0]);

            for (int index = 0; index < 30; index++)
            {
                draw.drawPoint(graphics, Brushes.Blue, ((PointF2D)history[index]));
                draw.drawLine(graphics, prePoint, ((PointF2D)history[index]));
                prePoint = ((PointF2D)history[index]);
            }
        }

        private void DrawAdam(Graphics graphics, DrawF draw)
        {
            ArrayList history = new ArrayList();
            PointF2D point = new PointF2D(-4.0f, 2.0f);
            Function2 fun = new Function2();
            Adam optimizer = new Adam(0.17f, 0.9f, 0.999f);

            for (int index = 0; index < 30; index++)
            {
                PointF2D xyPoint = draw.getBlockPoint(point.X, point.Y);
                history.Add(xyPoint);
                PointF2D diff = fun.DiffFormula(point.X, point.Y);
                optimizer.Update(point, diff);
            }

            PointF2D prePoint = ((PointF2D)history[0]);

            for (int index = 0; index < 30; index++)
            {
                draw.drawPoint(graphics, Brushes.Blue, ((PointF2D)history[index]));
                draw.drawLine(graphics, prePoint, ((PointF2D)history[index]));
                prePoint = ((PointF2D)history[index]);
            }
        }

        class SGD
        {
            private float _lr;

            public SGD(float lr)
            {
                _lr = lr;
            }

            public void Update(PointF2D point, PointF2D grad)
            {
                point.X = point.X - grad.X * _lr;
                point.Y = point.Y - grad.Y * _lr;
            }
        }

        class Momentum
        {
            private float _lr;
            private float _m;
            private float[] _v;

            public  Momentum(float lr, float m)
            {
                _lr = lr;
                _m = m;
                _v = new float[2];
            }

            public void Update(PointF2D point, PointF2D grad)
            {
                _v[0] = _m * _v[0] + grad.X * _lr;
                _v[1] = _m * _v[1] + grad.Y * _lr;
                point.X = point.X - _v[0];
                point.Y = point.Y - _v[1];
            }
        }

        class AdaGrad
        {
            private float _lr;
            private float[] _l2;

            public AdaGrad(float lr)
            {
                _lr = lr;
                _l2 = new float[2];
            }

            public void Update(PointF2D point, PointF2D grad)
            {
                _l2[0] = _l2[0] + grad.X * grad.X;
                _l2[1] = _l2[1] + grad.Y * grad.Y;
                point.X = point.X - _lr * grad.X / (float)Math.Sqrt(_l2[0] + 0.0000001f);
                point.Y = point.Y - _lr * grad.Y / (float)Math.Sqrt(_l2[1] + 0.0000001f);
            }
        }

        class RMSProp
        {
            private float _lr;
            private float _q;
            private float[] _sum;

            public RMSProp(float lr, float q)
            {
                _lr = lr;
                _q = q;
                _sum = new float[2];
            }

            public void Update(PointF2D point, PointF2D grad)
            {
                _sum[0] = _q * _sum[0] + grad.X * grad.X;
                _sum[1] = _q * _sum[1] + grad.Y * grad.Y;
                point.X = point.X - _lr * grad.X / (float)Math.Sqrt(_sum[0]);
                point.Y = point.Y - _lr * grad.Y / (float)Math.Sqrt(_sum[1]);
            }
        }

        class Adam
        {
            private float _lr;
            private float _beta1;
            private float _beta2;
            private float[] _b1Sum;
            private float[] _b2Sum;
            private int _iter;
            public Adam(float lr, float beta1, float beta2)
            {
                _lr = lr;
                _beta1 = beta1;
                _beta2 = beta2;
                _b1Sum = new float[2];
                _b2Sum = new float[2];
                _iter = 1;
            }

            public void Update(PointF2D point, PointF2D grad)
            {
                _b1Sum[0] = _beta1 * _b1Sum[0] + (1.0f - _beta1) * grad.X;
                _b1Sum[1] = _beta1 * _b1Sum[1] + (1.0f - _beta1) * grad.Y;

                _b2Sum[0] = _beta2 * _b2Sum[0] + (1.0f - _beta2) * grad.X * grad.X;
                _b2Sum[1] = _beta2 * _b2Sum[1] + (1.0f - _beta2) * grad.Y * grad.Y;

                float[] b1Fix = new float[2];
                b1Fix[0] = _b1Sum[0] / (1.0f - (float)Math.Pow(_beta1, _iter) + 0.00000001f);
                b1Fix[1] = _b1Sum[1] / (1.0f - (float)Math.Pow(_beta1, _iter) + 0.00000001f);

                float[] b2Fix = new float[2];
                b2Fix[0] = _b2Sum[0] / (1.0f - (float)Math.Pow(_beta2, _iter) + 0.00000001f);
                b2Fix[1] = _b2Sum[1] / (1.0f - (float)Math.Pow(_beta2, _iter) + 0.00000001f);

                point.X = point.X - _lr * b1Fix[0] / ((float)Math.Sqrt(b2Fix[0]) + 0.00000001f);
                point.Y = point.Y - _lr * b1Fix[1] / ((float)Math.Sqrt(b2Fix[1]) + 0.00000001f);
                _iter++;
            }
        }

        interface Function
        {
            float Formula(float x, float y);
        }

        class Function1 : Function
        {
            public float Formula(float x, float y)
            {
                return x * x + y * y;
            }
        }

        class Function2 : Function
        {
            public float Formula(float x, float y)
            {
                return x * x / 20.0f + y * y;
            }

            public PointF2D DiffFormula(float x, float y)
            {
                return new PointF2D(x / 10.0f, 2.0f * y);
            }
        }

        PointF2D Fun(Function fun, float x, float y)
        {
            PointF2D grad = new PointF2D();
            float fun1 = 0.0f;
            float fun2 = 0.0f;
            float h = 1e-4f;

            fun1 = fun.Formula((x + h), y);
            fun2 = fun.Formula((x - h), y);
            grad.X = (fun1 - fun2) / (h * 2);

            fun1 = fun.Formula(x, (y + h));
            fun2 = fun.Formula(x, (y - h));
            grad.Y = (fun1 - fun2) / (h * 2);

            return grad;
        }
    }
}
