using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwarmIntel {
	class Robot {
		#region Variables
		const double pi = Math.PI, pi2 = 2 * Math.PI;
		public int n;

		private double x,  y,  t,  xx,  yy,  tt;
		private double dx, dy, dt, dxx, dyy, dtt;
		public double ox, oy, ot, oxx, oyy, ott;
		public double X  { get { return x ; } }
		public double Y  { get { return y ; } }
		public double T  { get { return t ; } }
		public double XX { get { return xx; } }
		public double YY { get { return yy; } }
		public double TT { get { return tt; } }

		public double width = 16.0, depth = 16.0;
		public Point fl, fr, bl, br;
		public Rectangle rect;

		public Color c; public Pen p; public SolidBrush b;
		#endregion Variables

		public Robot(int nn, Color nc, double nx, double ny, double nt = 0.0) {
			ox = oy = ot = oxx = oyy = ott = -1;
			x = dx = nx; y = dy = ny; t = dt = nt; xx = yy = tt = dxx = dyy = dtt = 0;
			c = nc; p = new Pen(c); b = new SolidBrush(c); n = nn; //Calc();
		
		}

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
			double tx = 0, ty = 0, d = 0; int tc = 0;
			for(double a = 0 ; a < pi2 ; a += 10.0 * pi2 / 360.0)
				if((d = World.Scan(n, a, 500, 16)) > 0) { tc++; 
					tx += (Math.Cos(a) * d); ty += (Math.Sin(a) * d); }
			if(tc > 0) { dx = x + tx / tc; dy = y + ty / tc; }

			dx = (ox > -1) ? (dx + ox) / 2 : dx;
			dy = (oy > -1) ? (dy + oy) / 2 : dy;

			xx = Math.Sign(dx - x); yy = Math.Sign(dy - y);

			x += xx; y += yy;



			fl = new Point((int)(x - width / 2.0), (int)(y - depth / 2.0));
			fr = new Point((int)(x + width / 2.0), (int)(y - depth / 2.0));
			bl = new Point((int)(x - width / 2.0), (int)(y + depth / 2.0));
			br = new Point((int)(x + width / 2.0), (int)(y + depth / 2.0));
			rect = new Rectangle(fl.X, fl.Y, (int)width, (int)depth);


		}
		public void Draw(Graphics gb) {
			#region Robot
			//gb.DrawLine(Pens.White, x - width / 2 - depth / 2, y - width / 2 - depth / 2, x + width / 2 - depth / 2, y - width / 2 - depth / 2);
			gb.FillRectangle(b, rect);
			gb.DrawRectangle(Pens.White, rect);
			#endregion Robot

			gb.DrawLine(p, (int)(dx - width / 2), (int)(dy), (int)(dx + width / 2), (int)(dy));
			gb.DrawLine(p, (int)(dx), (int)(dy - depth / 2), (int)(dx), (int)(dy + depth / 2));
			gb.DrawLine(p, (int)(ox - width / 2), (int)(oy), (int)(ox + width / 2), (int)(oy));
			gb.DrawLine(p, (int)(ox), (int)(oy - depth / 2), (int)(ox), (int)(oy + depth / 2));
			gb.DrawEllipse(p, (int)(ox - width / 2), (int)(oy - depth / 2), (int)width, (int)depth);


		}
	}
}
