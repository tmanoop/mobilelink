using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace GpsPerimeter
{
    public partial class Compas : Control
    {


        struct PointD
        {
            public double X;
            public double Y;

            
            public PointD(double px, double py)
            {
                X = px;
                Y = py;
            }

        }

        int _ux, _vy;

        double _angle = 45;

        Rectangle _outerCircleBounds;
        Rectangle _innerCircleBounds;
        Point _centerPoint;

        Color _backgroundColor;

        Color _ringColor;
        Color _pointerColor;
        Color _tailColor;

        bool _enforceAspectRatio = false;


        PointD[] _trianglePointList;
        PointD[] _triangleTailPointList;

        Point[] _transformedPointList;
        Point[] _transformedTailPointList;



        public bool EnforceAspectRatio
        {
            get { return _enforceAspectRatio;  }
            set { _enforceAspectRatio = value; }
        }

        public double Angle
        {
            set 
            { 
                _angle = value;
                this.Invalidate();
            }
            get { return _angle; }
        }
        public Color BackgroundColor
        {
            set { _backgroundColor = value; }
            get { return _backgroundColor; }
        }

        public Color RingColor
        {
            get { return _ringColor;  }
            set { _ringColor = value; }
        }

        public Color PointerColor
        {
            get { return _pointerColor;  }
            set { _pointerColor = value;  }
        }

        public Color TailColor
        {
            get { return _tailColor; }
            set { _tailColor = value; }
        }


        public Compas()
        {
            InitializeComponent();
            _trianglePointList = new PointD[3];
            _triangleTailPointList = new PointD[4];
            _transformedPointList = new Point[3];
            _transformedTailPointList = new Point[4];
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            int outerDeltaX, outerDeltaY;
            int innerDeltaX, innerDeltaY;

            outerDeltaX = (ClientRectangle.Width/10);
            outerDeltaY = (ClientRectangle.Height/10);
            innerDeltaX = (ClientRectangle.Width/5);
            innerDeltaY = (ClientRectangle.Height/5);

            _centerPoint = new Point(ClientRectangle.X + ClientRectangle.Width / 2, ClientRectangle.Y + ClientRectangle.Height / 2);


            

            _ux = ( ClientRectangle.Width / 2) ;
            _vy = ( ClientRectangle.Height / 2) ;

            _trianglePointList[0] = new PointD(_ux * 0.0 , _vy * 1);
            _trianglePointList[1] = new PointD(_ux *-0.06 , _vy * 0);
            _trianglePointList[2] = new PointD(_ux * 0.06 , _vy * 0);


            _triangleTailPointList[0] = new PointD(_ux * -0.06, _vy * -1);
            _triangleTailPointList[1] = new PointD(_ux * -0.06, _vy * 0);
            _triangleTailPointList[2] = new PointD(_ux * 0.06, _vy * 0);
            _triangleTailPointList[3] = new PointD(_ux * 0.06, _vy * -1);


            if (EnforceAspectRatio)
            {
                int minOuterDelta = Math.Min(outerDeltaX, outerDeltaY);

                _outerCircleBounds = new Rectangle(
                    _centerPoint.X - minOuterDelta*5,
                    _centerPoint.Y- minOuterDelta*5,
                     minOuterDelta*10,
                    minOuterDelta*10
                    );

                _innerCircleBounds = new Rectangle(
                    _centerPoint.X - minOuterDelta * 4,
                    _centerPoint.Y - minOuterDelta * 4,
                     minOuterDelta * 8,
                    minOuterDelta * 8
                    );
            }
            else
            {
                _outerCircleBounds = new Rectangle(
                    ClientRectangle.X + outerDeltaX,
                    ClientRectangle.Y + outerDeltaY,
                    ClientRectangle.Width - (2 * outerDeltaX),
                    ClientRectangle.Height - (2 * outerDeltaY)
                    );

                _innerCircleBounds = new Rectangle(
                    ClientRectangle.X + innerDeltaX,
                    ClientRectangle.Y + innerDeltaY,
                    ClientRectangle.Width - (2 * innerDeltaX),
                    ClientRectangle.Height - (2 * innerDeltaY)
                    );
            }
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            
            Brush BackgroundBrush = new SolidBrush(_backgroundColor);
            Brush RingBrush = new SolidBrush(_ringColor);
            Brush PointerBrush = new SolidBrush(_pointerColor);
            Brush TailBrush = new SolidBrush(_tailColor);

            double radAngle = -((double)_angle*(Math.PI/180));

            for(int i=0;i<_transformedPointList.Length;++i)
            {
                _transformedPointList[i].X = _centerPoint.X + (int)(_trianglePointList[i].X*Math.Cos(radAngle)+_trianglePointList[i].Y*Math.Sin(radAngle));
                _transformedPointList[i].Y = _centerPoint.Y + (int)(_trianglePointList[i].Y*-Math.Cos(radAngle)+_trianglePointList[i].X*Math.Sin(radAngle));
            }

            for (int i = 0; i < _transformedTailPointList.Length; ++i)
            {
                _transformedTailPointList[i].X = _centerPoint.X + (int)(_triangleTailPointList[i].X * Math.Cos(radAngle) + _triangleTailPointList[i].Y * Math.Sin(radAngle));
                _transformedTailPointList[i].Y = _centerPoint.Y + (int)(_triangleTailPointList[i].Y * -Math.Cos(radAngle) + _triangleTailPointList[i].X * Math.Sin(radAngle));
            }
            
            pe.Graphics.FillEllipse(RingBrush, _outerCircleBounds);
            pe.Graphics.FillEllipse(BackgroundBrush, _innerCircleBounds);
            pe.Graphics.FillPolygon(PointerBrush, _transformedPointList);
            pe.Graphics.FillPolygon(TailBrush, _transformedTailPointList);

            
        }
    }
}
