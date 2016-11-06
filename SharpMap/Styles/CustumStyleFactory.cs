/*****************************************************
 * CX �����Զ�����ʽ��
 * 
 * ����Ч�� 
 * 
 * ***************************************************/
using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;

namespace SharpMap.Styles
{
    public class CustumStyleFactory
    {

        /// <summary>
        /// ��״-���ٹ�·��ʽ
        /// </summary>
        /// <returns></returns>
        public static VectorStyle HightWayStyle()
        {
            SharpMap.Styles.VectorStyle vs = new SharpMap.Styles.VectorStyle();
            System.Drawing.Pen pen0 = new Pen(Color.FromArgb(255, 187, 051), 5);
            System.Drawing.Pen pen1 = new Pen(Color.FromArgb(208, 161, 065), 8);

            Pen[] linePens = new Pen[2];
            linePens[0] = pen1;
            linePens[1] = pen0;

            vs.Lines = linePens;

            return vs;
        }

        /// <summary>
        /// ��״-һ�����ɵ���ʽ
        /// </summary>
        /// <returns></returns>
        public static VectorStyle Level1WayStyle()
        {
            SharpMap.Styles.VectorStyle vs = new SharpMap.Styles.VectorStyle();
            System.Drawing.Pen pen0 = new Pen(Color.FromArgb(255, 238, 153), 5);
            System.Drawing.Pen pen1 = new Pen(Color.FromArgb(232, 193, 077), 8);

            Pen[] linePens = new Pen[2];
            linePens[0] = pen1;
            linePens[1] = pen0;

            vs.Lines = linePens;

            return vs;
        }

        /// <summary>
        /// ��״-�������ɵ���ʽ
        /// </summary>
        /// <returns></returns>
        public static VectorStyle Level2WayStyle()
        {
            SharpMap.Styles.VectorStyle vs = new SharpMap.Styles.VectorStyle();
            System.Drawing.Pen pen0 = new Pen(Color.FromArgb(255, 255, 204), 4);
            System.Drawing.Pen pen1 = new Pen(Color.FromArgb(233, 214, 168), 7);

            Pen[] linePens = new Pen[2];
            linePens[0] = pen1;
            linePens[1] = pen0;

            vs.Lines = linePens;

            return vs;
        }

        /// <summary>
        /// ��״-С·��ʽ
        /// </summary>
        /// <returns></returns>
        public static VectorStyle LittleRoadStyle()
        {
            SharpMap.Styles.VectorStyle vs = new SharpMap.Styles.VectorStyle();
            System.Drawing.Pen pen0 = new Pen(Color.FromArgb(253, 253, 253), 3);
            System.Drawing.Pen pen1 = new Pen(Color.FromArgb(220, 220, 220), 5);

            Pen[] linePens = new Pen[2];
            linePens[0] = pen1;
            linePens[1] = pen0;

            vs.Lines = linePens;

            return vs;
        }



        /// <summary>
        /// ��״-��԰�̵�
        /// </summary>
        /// <returns></returns>
        public static VectorStyle GreenParkStyle()
        {
            SharpMap.Styles.VectorStyle vs = new SharpMap.Styles.VectorStyle();
            Brush brush = new SolidBrush(Color.FromArgb(199, 228, 185));
            vs.Fill = brush;
            return vs;
        }

        /// <summary>
        /// ��״-��ɫ����
        /// </summary>
        /// <returns></returns>
        public static VectorStyle BlueRiverStyle()
        {
            SharpMap.Styles.VectorStyle vs = new SharpMap.Styles.VectorStyle();
            Brush brush = new SolidBrush(Color.FromArgb(153, 180, 207));
            vs.Fill = brush;
            return vs;
        }

        /// <summary>
        /// ��״-�հ׵�
        /// </summary>
        /// <returns></returns>
        public static VectorStyle SpaceAreaStyle()
        {
            SharpMap.Styles.VectorStyle vs = new SharpMap.Styles.VectorStyle();
            Brush brush = new SolidBrush(Color.FromArgb(240, 240, 240));
            vs.Fill = brush;
            return vs;
        }





    }
}
