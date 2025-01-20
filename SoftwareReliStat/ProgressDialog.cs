using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace View
{
	public partial class ProgressDialog : Form
	{
		public ProgressDialog()
		{
			InitializeComponent();

			// Инициализация прогресс-бара
			guna2ProgressBar1.Minimum = 0;
			guna2ProgressBar1.Maximum = 100;
			guna2ProgressBar1.Value = 0;
			label1.Text = "Прогресс: 0%";
		}

		public void UpdateProgress(int percent)
		{
			if (InvokeRequired)
			{
				Invoke(new Action(() => UpdateProgress(percent)));
			}
			else
			{
				guna2ProgressBar1.Value = percent;
				label1.Text = $"Прогресс: {percent}%";
			}
		}
	}
}
