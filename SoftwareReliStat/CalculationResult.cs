using DatabasePostgreSQL.Models;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ClassLibrary.DistributionAnalyzer;

namespace View
{
	public partial class CalculationResult : Form
	{
		//private List<ClusterAnalysisResult> calculationResults;

		public CalculationResult()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			// Настройка DataGridView
			InitializeDataGridView(guna2DataGridView1);

			// Добавление столбцов с указанными названиями
			guna2DataGridView1.Columns.Add("ClusterNumber", "Кластер №");
			//guna2DataGridView1.Columns.Add("Interval", "Интервал");
			guna2DataGridView1.Columns.Add("Weight", "Весовой коэффициент");
			guna2DataGridView1.Columns.Add("Distribution", "Закон распределения");
			guna2DataGridView1.Columns.Add("Parameters", "Значение параметра закона распределения");
			guna2DataGridView1.Columns.Add("Deviation", "Величина отклонения");

			// Задание ширины столбцов
			guna2DataGridView1.Columns["ClusterNumber"].Width = 80;
			guna2DataGridView1.Columns["Weight"].Width = 120;
			guna2DataGridView1.Columns["Distribution"].Width = 220;
			guna2DataGridView1.Columns["Parameters"].Width = 210;
			guna2DataGridView1.Columns["Deviation"].Width = 100;
		}

		// Инициализация DataGridView
		private void InitializeDataGridView(Guna2DataGridView dataGridView)
		{
			dataGridView.RowHeadersVisible = false;
			dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dataGridView.AllowUserToAddRows = false; // Запрет на добавление строк пользователем
			dataGridView.AllowUserToDeleteRows = false; // Запрет на удаление строк пользователем
			dataGridView.ReadOnly = true; // Делаем таблицу только для чтения

			// Настройка линий сетки
			dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Single; // Линия для каждой ячейки
			dataGridView.GridColor = System.Drawing.Color.Gray; // Цвет сетки
		}

		// Метод для загрузки данных в DataGridView
		public void LoadData(List<ClusterAnalysisResult> results)
		{
			guna2DataGridView1.Rows.Clear();

			foreach (var result in results)
			{
				guna2DataGridView1.Rows.Add(
					result.ClusterNumber,
					//result.Interval,
					result.Weight,
					result.Distribution,
					result.Parameters,
					result.Deviation);
			}
		}

		/// <summary>
		/// Обработчик кнопки "Измерение по данным из файла CSV".
		/// </summary>
		/// <param name="sender">Источник события, кнопка.</param>
		/// <param name="e">Аргументы события клика.</param>
		private void guna2Button2_Click(object sender, EventArgs e)
		{
			try
			{
				// Обновление таблицы результатами
				LoadData(SoftwareReliStat.MainForm.CalculationResults);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка при заполнении таблицы: {ex.Message}", "Ошибка", 
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
