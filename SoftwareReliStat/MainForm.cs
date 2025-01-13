using ClassLibrary;
using DatabasePostgreSQL;
using DatabasePostgreSQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;
using TheArtOfDevHtmlRenderer.Core;

namespace SoftwareReliStat
{
	public partial class MainForm : Form
	{

		private readonly DatabaseDbContext _context;

		public MainForm()
		{
			InitializeComponent();

			// Используем фабрику для создания контекста
			var dbContextFactory = new DesignTimeDbContextFactory();
			_context = dbContextFactory.CreateDbContext(null);

			// Загружаем данные
			LoadUnifiedPowerSystems();
		}

		private void LoadUnifiedPowerSystems()
		{
			var unifiedPowerSystems = _context.UnifiedPowerSystem.ToList();
			guna2ComboBox1.DataSource = unifiedPowerSystems;
			guna2ComboBox1.DisplayMember = "Title";
			guna2ComboBox1.ValueMember = "Id";
			guna2ComboBox1.SelectedIndex = -1; // Сбрасываем выбор по умолчанию
		}

		private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (guna2ComboBox1.SelectedValue is int unifiedPowerSystemId)
			{
				var powerSystems = _context.PowerSystem
					.Where(ps => ps.UnifiedPowerSystemId == unifiedPowerSystemId)
					.ToList();

				guna2ComboBox2.DataSource = powerSystems;
				guna2ComboBox2.DisplayMember = "Title";
				guna2ComboBox2.ValueMember = "Id";
				guna2ComboBox2.Enabled = true;
				guna2ComboBox2.SelectedIndex = -1; // Сбрасываем выбор

				// Очистка следующего списка
				guna2ComboBox3.DataSource = null;
				guna2ComboBox3.Enabled = false;
			}
			else
			{
				// Если ничего не выбрано, сбрасываем данные
				guna2ComboBox2.DataSource = null;
				guna2ComboBox2.Enabled = false;
				guna2ComboBox3.DataSource = null;
				guna2ComboBox3.Enabled = false;
			}
		}

		private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (guna2ComboBox2.SelectedValue is int powerSystemId)
			{
				var reliabilityZones = _context.ReliabilityZone
					.Where(rz => rz.PowerSystemId == powerSystemId)
					.ToList();

				guna2ComboBox3.DataSource = reliabilityZones;
				guna2ComboBox3.DisplayMember = "Title";
				guna2ComboBox3.ValueMember = "Id";
				guna2ComboBox3.Enabled = true;
				guna2ComboBox3.SelectedIndex = -1; // Сбрасываем выбор
			}
			else
			{
				// Если ничего не выбрано, сбрасываем данные
				guna2ComboBox3.DataSource = null;
				guna2ComboBox3.Enabled = false;
			}
		}


		private void ButtonConnectionNpgsql(object sender, EventArgs e)
		{
			// Строка подключения к PostgreSQL
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			var connectionString = configuration.GetConnectionString("DefaultConnection");

			try
			{
				using (var connection = new NpgsqlConnection(connectionString))
				{
					connection.Open(); // Попытка открыть соединение

					// Уведомление о успешном соединении
					MessageBox.Show("Соединение с базой данных установлено!", "Уведомление",
						MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch (Exception ex)
			{
				// Уведомление о неудачном соединении с указанием ошибки
				MessageBox.Show($"Не удалось подключиться к базе данных:\n{ex.Message}", "Ошибка",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void ButtonConnectionSCADA(object sender, EventArgs e)
		{

		}

		private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
		{
			DateTime selectedDateTime = guna2DateTimePicker1.Value;
			//MessageBox.Show($"Вы выбрали: {selectedDateTime}");
		}

		private void CSVToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Filter = "CSV files (*.csv)|*.csv",
				Title = "Выберите CSV файл"
			};

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					HandlerCSV handler = new HandlerCSV();
					string[] data = handler.ReadCSVData(openFileDialog.FileName);

					// Уведомление о сохранении данных
					MessageBox.Show($"Данные успешно загружены! " +
						$"Найдено строк: {data.Length}.",
						"Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch (ArgumentException ex)
				{
					MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Неизвестная ошибка: {ex.Message}", "Ошибка",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
	}
}
