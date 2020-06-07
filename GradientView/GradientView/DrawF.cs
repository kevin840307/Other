using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GradientView
{
    class DrawF
    {
        PointF2D _center;
        float _offset;

        public DrawF(float offset)
        {
            _center = new PointF2D(5.0f * offset, 5.0f * offset);
            _offset = offset;
        }

        /*  DrawF Params:
         *  offset  :point's size
         *  x       : x's count
         *  y       : y's count
         */
        public DrawF(float offset, float x, float y)
        {
            _center = new PointF2D(x * offset, y * offset);
            _offset = offset;
        }


        public PointF2D getBlockPoint(float x, float y)
        {
            PointF2D point = new PointF2D();
            point.X = (_center.X / _offset + x) * _offset;
            point.Y = (_center.Y / _offset - y) * _offset;
            return point;
        }

        public PointF2D Center
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
        public void drawHistogram(Graphics graphics, float height, float width)
        {
            Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0), 1);
            float xLen = width * _offset;
            float yLen = height * _offset;

            // 水平的分割出垂直數量
            graphics.DrawLine(pen, 0, 0, yLen, 0);
            graphics.DrawLine(pen, 0, width * _offset, yLen, width * _offset);

            // 垂直的分割出水平數量
            graphics.DrawLine(pen, 0, 0, 0, xLen);
            graphics.DrawLine(pen, height * _offset, 0, height * _offset, xLen);
        }

        public void drawBlock(Graphics graphics, float x, float y)
        {
            Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0), 1);
            float xLen = x * _offset;
            float yLen = y * _offset;

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

        public void drawCoordinate(Graphics graphics, float x, float y)
        {
            Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0), 1);
            float xLen = x * _offset;
            float yLen = y * _offset;


                graphics.DrawLine(pen, 0, xLen / 2.0f, yLen, xLen / 2.0f);

                graphics.DrawLine(pen, yLen / 2.0f, 0, yLen / 2.0f, xLen);

        }

        public void drawLine(Graphics graphics, float xS, float yS, float xE, float yE)
        {
            Pen pen = new Pen(Color.FromArgb(255, 178, 34, 34), 5);
            graphics.DrawLine(pen, xS, yS, xE, yE);
        }

        public void drawLine(Graphics graphics, PointF2D pStart, PointF2D pEnd)
        {
            drawLine(graphics, pStart.X, pStart.Y, pEnd.X, pEnd.Y);
        }

        public void drawPoint(Graphics graphics, Brush color, float x, float y)
        {
            graphics.FillRectangle(color, x - 5.0f, y - 5.0f, 10.0f, 10.0f);
        }

        public void drawPoint(Graphics graphics, Brush color, PointF2D point)
        {
            drawPoint(graphics, color, point.X, point.Y);
        }

        public void drawPointLine(Graphics graphics, Brush color, float x, float y)
        {
            graphics.FillRectangle(color, x, y, 2.0f, 2.0f);
        }

        public void drawGradient(Graphics graphics, PointF2D point, float U, float V, float maxSum)
        {
            // 鄰邊 = U = x
            // 對邊 = V = y
            float absU = Math.Abs(U);
            float absV = Math.Abs(V);

            // atan取弧度(tan = 對邊 / 鄰邊
            float radian = absU != 0 ? (float)Math.Atan(absV / absU) : (float)(Math.PI * 0.5);
            Console.WriteLine(radian / Math.PI * 180);

            // cos = 鄰邊 / 斜邊, sin = 對邊 / 斜邊
            float xCos = U != 0.0f ? (float)Math.Cos(radian) : 1.0f;
            float ySin = V != 0.0f ? (float)Math.Sin(radian) : 0.0f;

            float xDirection = U < 0.0f ? -1.0f : U > 0 ? 1.0f : 0.0f;
            float yDirection = V < 0.0f ? 1.0f : V > 0 ? -1.0f : 0.0f;


            // 計算顯示長度比例
            float max = absU > absV ? absU : absV;
            float maxLen = (max / maxSum * _offset);

            for (float index = 0; index < maxLen; index += 0.01f)
            {
                // 取得斜邊
                // 取得x + 方向和y + 方向(對邊)
                float bevel = index / xCos;
                float x = index * xDirection;
                float y = ySin * bevel * yDirection;

                if (Math.Abs(y) > maxLen)
                {
                    return;
                }

                drawPointLine(graphics, Brushes.Black, point.X + x, point.Y + y);
            }
        }

        public void drawGradient(Graphics graphics, PointF2D point, float U, float V)
        {
            float sum = Math.Abs(V) + Math.Abs(U);
            drawGradient(graphics, point, U, V, sum);
        }

        public void drawGradient(Graphics graphics, PointF2D xyPoint, PointF2D uvPoint)
        {
            float sum = Math.Abs(uvPoint.X) + Math.Abs(uvPoint.Y);
            drawGradient(graphics, xyPoint, uvPoint.X, uvPoint.Y, sum);
        }

        public void drawGradients(Graphics graphics, ArrayList xyPoints, ArrayList uvPoints)
        {
            float maxUV = getMaxUV(uvPoints);

            for (int index = 0; index < xyPoints.Count; index++)
            {
                PointF2D xyPoint = (PointF2D)xyPoints[index];
                PointF2D uvPoint = (PointF2D)uvPoints[index];

                drawGradient(graphics, xyPoint, uvPoint.X, uvPoint.Y, maxUV);
            }
        }

        private float getMaxUV(ArrayList uvPoints)
        {
            float maxUV = 0.0f;

            for (int index = 0; index < uvPoints.Count; index++)
            {
                PointF2D uvPoint = (PointF2D)uvPoints[index];
                float sum = Math.Abs(uvPoint.X) + Math.Abs(uvPoint.Y);
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
