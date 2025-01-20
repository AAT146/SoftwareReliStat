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
using View;
using static ClassLibrary.DistributionAnalyzer;

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

		/// <summary>
		/// Загрузка значений ОЭС из БД
		/// </summary>
		private void LoadUnifiedPowerSystems()
		{
			var unifiedPowerSystems = _context.UnifiedPowerSystem.ToList();
			guna2ComboBox1.DataSource = unifiedPowerSystems;
			guna2ComboBox1.DisplayMember = "Title";
			guna2ComboBox1.ValueMember = "Id";
			guna2ComboBox1.SelectedIndex = -1; // Сбрасываем выбор по умолчанию
		}

		/// <summary>
		/// Метод вывода значений ОЭС на пользовательскую форму.
		/// </summary>
		/// <param name="sender">Данные.</param>
		/// <param name="e">Данные о событие.</param>
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

		/// <summary>
		/// Метод вывода значений ЭС на пользовательскую форму.
		/// </summary>
		/// <param name="sender">Данные.</param>
		/// <param name="e">Данные о событие.</param>
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

		/// <summary>
		/// Метод создания соединения с БД (PostgeSQL).
		/// </summary>
		/// <param name="sender">Данные.</param>
		/// <param name="e">Данные о событие.</param>
		private void ConnectPostgreSQL(object sender, EventArgs e)
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

		/// <summary>
		/// Метод создания соединения с ОИК СК-11.
		/// </summary>
		/// <param name="sender">Данные.</param>
		/// <param name="e">Данные о событие.</param>
		private void ConnectSCADA(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// Поле: исходный массив значений.
		/// </summary>
		private string[] data;

		/// <summary>
		/// Поле: обработанный массив значений.
		/// </summary>
		private int[] processedData;

		private List<ClusterAnalysisResult> _calculationResults; // Поле для хранения результатов

		/// <summary>
		/// Метод загрузки данных из CSV файла.
		/// </summary>
		/// <param name="sender">Данные.</param>
		/// <param name="e">Данные о событие.</param>
		private void UploadFileCSV(object sender, EventArgs e)
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
					data = handler.ReadCSVData(openFileDialog.FileName);

					// Анализ данных
					var result = handler.AnalyzeData(data);
					processedData = result.processedNumbers; // Сохраняем результат в поле
					int replacedCount = result.replacedCount;

					// Уведомление о сохранении данных
					MessageBox.Show($"Данные успешно обработаны! \n" +
						$"Исходных строк: {data.Length}, \n" +
						$"Обработанные числа: {replacedCount}.",
						"Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

		/// <summary>
		/// Метод чтения и записи величин по UID.
		/// </summary>
		/// <param name="sender">Данные.</param>
		/// <param name="e">Данные о событие.</param>
		private void ReadWriteDataUID(object sender, EventArgs e)
		{
			if (processedData == null)
			{
				MessageBox.Show("Сначала загрузите данные.");
				return;
			}
		}

		/// <summary>
		/// Обработчик действий кнопки "Определение законов распределений".
		/// </summary>
		/// <param name="sender">Источник события, кнопка.</param>
		/// <param name="e">Аргументы события клика.</param>
		private async void guna2Button2_ClickAsync(object sender, EventArgs e)
		{
			// Проверка, что данные существуют
			if (processedData == null || processedData.Length == 0)
			{
				MessageBox.Show("Данные отсутствуют для анализа.", "Ошибка",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			// Создание и отображение окна с прогрессом
			ProgressDialog progressDialog = new ProgressDialog();
			progressDialog.Show();

			// Максимально допустимое отклонение
			double maxDeviation = 0.1;

			try
			{

				// Инициализация объекта для отслеживания прогресса
				var progress = new Progress<int>(percent =>
				{
					progressDialog.UpdateProgress(percent);
				});

				// Вызов анализа в асинхронном потоке
				_calculationResults = await Task.Run(() =>
					DistributionAnalyzer.AnalyzeDistribution(
						processedData.Select(x => (double)x).ToArray(),
						maxDeviation,
						progress
					)
				);

				// Закрытие окна прогресса
				progressDialog.Close();

				// Вывод результатов анализа (пример)
				MessageBox.Show("Анализ распределения выполнен успешно!", "Уведомление",
					MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				// Закрытие окна прогресса при ошибке
				progressDialog.Close();

				// Обработка ошибок
				MessageBox.Show($"Ошибка при выполнении анализа: {ex.Message}", "Ошибка",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SaveFileCSV(object sender, EventArgs e)
		{
			if (_calculationResults == null || _calculationResults.Count == 0)
			{
				MessageBox.Show("Нет результатов для формирования отчета.");
				return;
			}

			// Выбор пути для сохранения файла
			using (var saveFileDialog = new SaveFileDialog())
			{
				saveFileDialog.Filter = "CSV файлы (*.csv)|*.csv";
				saveFileDialog.DefaultExt = "csv";
				saveFileDialog.FileName = "ClusterAnalysisReport.csv";

				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					try
					{
						// Сохранение в CSV
						HandlerCSV.WriteCSVData(_calculationResults, saveFileDialog.FileName);
						MessageBox.Show("Отчет успешно сохранен.");
					}
					catch (Exception ex)
					{
						MessageBox.Show($"Ошибка сохранения: {ex.Message}");
					}
				}
			}
		}



		///// <summary>
		///// Обработчик действий кнопки "Определение законов распределений".
		///// </summary>
		///// <param name="sender">Источник события, кнопка.</param>
		///// <param name="e">Аргументы события клика.</param>
		//private void guna2Button2_Click(object sender, EventArgs e)
		//{
		//	// Проверка, что данные существуют
		//	if (processedData == null || processedData.Length == 0)
		//	{
		//		MessageBox.Show("Данные отсутствуют для анализа.", "Ошибка",
		//			MessageBoxButtons.OK, MessageBoxIcon.Error);
		//		return;
		//	}

		//	// Максимально допустимое отклонение
		//	double maxDeviation = 0.1;

		//	try
		//	{
		//		var progress = new Progress<int>(percent => Console.WriteLine($"Прогресс: {percent}%"));

		//		// Вызов анализа
		//		var results = DistributionAnalyzer.AnalyzeDistribution(
		//			processedData.Select(x => (double)x).ToArray(),
		//			maxDeviation,
		//			progress
		//		);

		//		// Вывод результатов анализа (пример)
		//		MessageBox.Show("Анализ распределения выполнен успешно!", "Уведомление",
		//			MessageBoxButtons.OK, MessageBoxIcon.Information);
		//	}
		//	catch (Exception ex)
		//	{
		//		// Обработка ошибок
		//		MessageBox.Show($"Ошибка при выполнении анализа: {ex.Message}", "Ошибка",
		//			MessageBoxButtons.OK, MessageBoxIcon.Error);
		//	}
		//}
	}
}
