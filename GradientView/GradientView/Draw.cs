using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradientView
{
    class Draw
    {

        Point2D _center;
        int _offset;

        public Draw(int offset)
        {
            _center = new Point2D(5 * offset, 5 * offset);
            _offset = offset;
        }

        public Draw(int offset, int x, int y)
        {
            _center = new Point2D(x * offset, y * offset);
            _offset = offset;
        }


        public Point2D getBlockPoint(int x, int y)
        {
            Point2D point = new Point2D();
            point.X = (_center.X / _offset + x) * _offset;
            point.Y = (_center.Y / _offset - y) * _offset;
            return point;
        }

        public Point2D Center
        {
            get
            {
                return _center;
            }
        }

        //private Point2D getOriginalPoint(Point2D bPoint)
        //{
        //    Point2D point = new Point2D();
        //    point.X = (bPoint.X - _center.X);
        //    point.Y = (_center.Y + bPoint.Y);
        //    return point;
        //}

        public void drawBlock(Graphics graphics, int x, int y)
        {
            Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0), 1);
            int xLen = x * _offset;
            int yLen = y * _offset;

            // 水平的分割出垂直數量
            for (int row = 0; row <= x; row++)
            {
                graphics.DrawLine(pen, 0, row * _offset, yLen, row * _offset);
            }

            // 垂直的分割出水平數量
            for (int col = 0; col <= y; col++)
            {
                graphics.DrawLine(pen, col * _offset, 0, col * _offset, xLen);
            }
        }

        public void drawLine(Graphics graphics, int xS, int yS, int xE, int yE)
        {
            Pen pen = new Pen(Color.FromArgb(255, 178, 34, 34), 5);
            graphics.DrawLine(pen, xS, yS, xE, yE);
        }

        public void drawLine(Graphics graphics, Point2D pStart, Point2D pEnd)
        {
            drawLine(graphics, pStart.X, pStart.Y, pEnd.X, pEnd.Y);
        }

        public void drawPoint(Graphics graphics, Brush color, int x, int y)
        {
            graphics.FillRectangle(color, x - 5, y - 5, 10, 10);
        }

        public void drawPoint(Graphics graphics, Brush color, Point2D point)
        {
            drawPoint(graphics, color, point.X, point.Y);
        }

        public void drawPointLine(Graphics graphics, Brush color, int x, int y)
        {
            graphics.FillRectangle(color, x, y, 2, 2);
        }

        public void drawGradient(Graphics graphics, Point2D point, int U, int V, int maxSum)
        {
            // 鄰邊 = U = x
            // 對邊 = V = y
            int absU = Math.Abs(U);
            int absV = Math.Abs(V);

            // atan取弧度(tan = 對邊 / 鄰邊
            double radian = absU != 0 ? Math.Atan((float)absV / absU) : (Math.PI * 0.5);
            //Console.WriteLine(radian / Math.PI * 180);

            // cos = 鄰邊 / 斜邊, sin = 對邊 / 斜邊
            double xCos = U != 0 ? Math.Cos(radian) : 1;
            double ySin = V != 0 ? Math.Sin(radian) : 0;

            int xDirection = U < 0 ? -1 : U > 0 ? 1 : 0;
            int yDirection = V < 0 ? 1 : V > 0 ? -1 : 0;


            // 計算比例
            int max = absU > absV ? absU : absV;
            int maxLen = (int)((float)(max) / maxSum * _offset);

            for (float index = 0; index < maxLen; index += 0.01f)
            {
                // 取得斜邊
                // 取得x + 方向和y + 方向(對邊)
                double bevel = index / xCos;
                double x = index * xDirection;
                double y = ySin * bevel * yDirection;

                if (y > maxLen)
                {
                    return;
                }

                drawPointLine(graphics, Brushes.Black, (int)(point.X + x), (int)(point.Y + y));
            }
        }

        public void drawGradient(Graphics graphics, Point2D point, int U, int V)
        {
            int sum = Math.Abs(V) + Math.Abs(U);
            drawGradient(graphics, point, U, V, sum);
        }

        public void drawGradient(Graphics graphics, Point2D xyPoint, Point2D uvPoint)
        {
            int sum = Math.Abs(uvPoint.X) + Math.Abs(uvPoint.Y);
            drawGradient(graphics, xyPoint, uvPoint.X, uvPoint.Y, sum);
        }

        public void drawGradients(Graphics graphics, ArrayList xyPoints, ArrayList uvPoints)
        {
            int maxUV = getMaxUV(uvPoints);

            for (int index = 0; index < xyPoints.Count; index++)
            {
                Point2D xyPoint = (Point2D)xyPoints[index];
                Point2D uvPoint = (Point2D)uvPoints[index];

                drawGradient(graphics, xyPoint, uvPoint.X, uvPoint.Y, maxUV);
            }
        }

        private int getMaxUV(ArrayList uvPoints)
        {
            int maxUV = 0;

            for (int index = 0; index < uvPoints.Count; index++)
            {
                Point2D uvPoint = (Point2D)uvPoints[index];
                int sum = Math.Abs(uvPoint.X) + Math.Abs(uvPoint.Y);
                if (maxUV < sum)
                {
                    maxUV = sum;
                }
            }

            return maxUV;
        }

        //public void drawString(Graphics graphics, String str, int x, int y)
        //{
        //    // Create font and brush.
        //    Font drawFont = new Font("Arial", 12);
        //    SolidBrush drawBrush = new SolidBrush(Color.Black);

        //    // Create rectangle for drawing.

        //    float width = str.Length * 12;
        //    float height = 15.0F;
        //    RectangleF drawRect = new RectangleF(x, y, width, height);

        //    // Set format of string.
        //    StringFormat drawFormat = new StringFormat();
        //    drawFormat.Alignment = StringAlignment.Center;

        //    // Draw string to screen.
        //    graphics.DrawString(str, drawFont, drawBrush, drawRect, drawFormat);
        //}
    }
}
