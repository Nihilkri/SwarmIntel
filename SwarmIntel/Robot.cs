using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwarmIntel {
	class Robot {
		#region Variables
		private double x,  y,  t,  xx,  yy,  tt;
		public double dx, dy, dt, dxx, dyy, dtt;
		public double X  { get { return x ; } set { x  = value; } }
		public double Y  { get { return y ; } set { y  = value; } }
		public double T  { get { return t ; } set { t  = value; } }
		public double XX { get { return xx; } set { xx = value; } }
		public double YY { get { return yy; } set { yy = value; } }
		public double TT { get { return tt; } set { tt = value; } }

		double width = 16.0, depth = 10.0;
		PointF fl, fr, bl, br;

		public Color c; public Pen p; public SolidBrush b;
		#endregion Variables

		public Robot(Color nc, double nx, double ny, double nt = 0.0) {
			c = nc; p = new Pen(c); b = new SolidBrush(c);
			x = dx = nx; y = dy = ny; t = dt = nt; xx = yy = dxx = dyy = 0; }

		public bool Active() {double e = 0.0001; if(
			(Math.Abs(x  - dx ) < e) &&
			(Math.Abs(y  - dy ) < e) &&
			(Math.Abs(t  - dt ) < e) &&
			(Math.Abs(xx - dxx) < e) &&
			(Math.Abs(yy - dyy) < e) &&
			(Math.Abs(tt - dtt) < e))
			return false; else return true;
		}

		public void Calc() {


		}
		public void Draw(Graphics gb) {
			#region Robot
			//gb.DrawLine(Pens.White, x - width / 2 - depth / 2, y - width / 2 - depth / 2, x + width / 2 - depth / 2, y - width / 2 - depth / 2);
			gb.FillRectangle(b, 
				(int)(x - width / 2),
				(int)(y - depth / 2),
				(int)width, (int)depth);
			gb.DrawRectangle(Pens.White,
				(int)(x - width / 2),
				(int)(y - depth / 2),
				(int)width, (int)depth);
			#endregion Robot

			gb.DrawLine(p, (int)(dx - width / 2), (int)(dy), (int)(dx + width / 2), (int)(dy));
			gb.DrawLine(p, (int)(dx), (int)(dy - depth / 2), (int)(dx), (int)(dy + depth / 2));


		}
	}
}
