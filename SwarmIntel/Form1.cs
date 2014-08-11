﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SwarmIntel {
	public partial class World : Form {
		#region Variables
		const double pi = Math.PI, pi2 = 2 * Math.PI;
		Graphics gb, gf; Bitmap gi; int fx, fy, fx2, fy2;
		Random rnd = new Random(); Timer tim = new Timer();
		List<Robot> bots = new List<Robot>(); bool active = true;

		#endregion Variables
		#region Events
		public World() {InitializeComponent();}
		private void Form1_Load(object sender, EventArgs e) {
			fx = Width; fy = Height; fx2 = fx / 2; fy2 = fy / 2;
			gi = new Bitmap(fx, fy); gb = Graphics.FromImage(gi);
			gf = CreateGraphics();

			bots.Add(new Robot(bots.Count(), Color.Red, fx2, fy2));
			bots.Add(new Robot(bots.Count(), Color.Orange, rnd.NextDouble() * fx, rnd.NextDouble() * fy));
			bots.Add(new Robot(bots.Count(), Color.Yellow, rnd.NextDouble() * fx, rnd.NextDouble() * fy));
			bots.Add(new Robot(bots.Count(), Color.Green, rnd.NextDouble() * fx, rnd.NextDouble() * fy));
			bots.Add(new Robot(bots.Count(), Color.Blue, rnd.NextDouble() * fx, rnd.NextDouble() * fy));
			bots.Add(new Robot(bots.Count(), Color.Purple, rnd.NextDouble() * fx, rnd.NextDouble() * fy));

			tim.Interval = 1000 / 60; tim.Tick += tim_Tick;

			Calc(); Draw();
		}

		private void Form1_Paint(object sender, PaintEventArgs e) { gf.DrawImage(gi, 0, 0); }

		private void Form1_MouseClick(object sender, MouseEventArgs e) {
			bots[0].dx = e.X; bots[0].dy = e.Y; Draw(); if(bots[0].Active()) active = true;

		}

		private void Form1_KeyDown(object sender, KeyEventArgs e) {
			switch(e.KeyCode) {
				case Keys.Escape: Close(); return;

				default: break;
			}

		}

		private void tim_Tick(object sender, EventArgs e) {
			Calc(); Draw(); 
		}

		#endregion Events
		#region Calc
		void Calc() {

		}
		#endregion Calc
		#region Draw
		void Draw() {
			DateTime st = DateTime.Now; TimeSpan ft;
			gb.Clear(Color.Black);
			foreach(Robot q in bots) {
				q.Draw(gb);
				
			}

			DateTime sst = DateTime.Now; TimeSpan sft;
			for(int b = 0 ; b < bots.Count ; b++) for(double q = 0 ; q < pi2 ; q += pi2 / 360.0) Scan(b, q, 250, 1);
			sft = DateTime.Now - sst;
			gb.DrawString(sft.TotalMilliseconds.ToString(), Font, Brushes.White, fx2, 0);

			ft = DateTime.Now - st;
			gb.DrawString(ft.TotalMilliseconds.ToString(), Font, Brushes.White, 0, 0);
			gf.DrawImage(gi, 0, 0);
		}
		#endregion Draw
		#region Robot Interactions
		/// <summary>
		/// Scans the world around a robot in a straight line
		/// </summary>
		/// <param name="a">Angle relative to robot</param>
		/// <param name="r">Radius of scan</param>
		/// <param name="i">Intensity or resolution</param>
		/// <returns></returns>
		public double Scan(int n, double a, double r, double i) {
			a = (a + bots[n].T + pi2) % pi2 + pi2;
			int x, y;
			for(double q = bots[n].width ; q < r ; q += i) {
				x = (int)(bots[n].X + Math.Cos(a) * q); y = (int)(bots[n].Y + Math.Sin(a) * q);
				if((0 <= x && x < fx) && (0 <= y && y < fy)) gi.SetPixel(x, y, bots[n].c);
				foreach(Robot b in bots) if(b.rect.Contains(x, y)) return q;

			}
			return -1.0;
		}
		#endregion Robot Interactions

	}
}
