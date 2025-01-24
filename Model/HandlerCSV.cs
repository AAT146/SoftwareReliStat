using static ClassLibrary.DistributionAnalyzer;
using System.Text;

namespace ClassLibrary
{
	/// <summary>
	/// Класс для работы с CSV-файлами.
	/// </summary>
	public class HandlerCSV
	{
		/// <summary>
		/// Метод чтения данных из файла CSV.
		/// </summary>
		/// <param name="filePath">Путь к файлу CSV.</param>
		/// <returns>Массив строк из второго столбца файла.</returns>
		/// <exception cref="ArgumentException">Выбрасывается, если файл пуст или 
		/// формат некорректный.</exception>
		public string[] ReadCSVData(string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
				throw new ArgumentException("Путь к файлу не может быть пустым.");

			var result = new List<string>();

			using (var reader = new StreamReader(filePath))
			{
				// Чтение заголовка
				string header = reader.ReadLine();
				if (string.IsNullOrEmpty(header))
					throw new ArgumentException("Файл пуст или отсутствует заголовок.");

				// Чтение данных
				while (!reader.EndOfStream)
				{
					string line = reader.ReadLine();
					if (string.IsNullOrEmpty(line)) continue;

					string[] columns = line.Split(';');
					if (columns.Length > 1)
					{
						result.Add(columns[1]); // Сохраняем данные второго столбца
					}
					else
					{
						throw new ArgumentException("\nНекорректный формат файла,\n" +
							"отсутствует второй столбец.");
					}
				}
			}

			return result.ToArray();
		}

		/// <summary>
		/// Метод для анализа данных.
		/// </summary>
		/// <param name="data">Массив строк данных.</param>
		/// <returns>Массив чисел после обработки.</returns>
		public (int[] processedNumbers, int replacedCount) AnalyzeData(string[] data)
		{
			// Преобразуем строки в числа
			var numbers = data.Select(d => int.TryParse(d, out int num) ? num 
				: throw new ArgumentException("Некорректное число в данных.")).ToArray();

			// Сортируем массив по возрастанию
			Array.Sort(numbers);

			// Находим наибольшее трёхзначное число, либо ближайшее допустимое значение
			int maxThreeDigit = numbers.Where(n => n >= 100 && n <= 999).
				DefaultIfEmpty(numbers.Where(n => n <= 999).DefaultIfEmpty(0).Max()).Max();

			// Находим наименьшее положительное число
			int minPositive = numbers.Where(n => n > 0).DefaultIfEmpty(0).Min();

			int replacedCount = 0;

			// Обработка данных
			for (int i = 0; i < numbers.Length; i++)
			{
				if (numbers[i] < 0)
				{
					// Заменяем отрицательные числа на минимальное положительное
					numbers[i] = minPositive; 
					replacedCount++;
				}
				else if (numbers[i] > 999)
				{
					// Заменяем числа больше 999 на максимальное трёхзначное или ближайшее допустимое
					numbers[i] = maxThreeDigit; 
					replacedCount++;
				}
			}

			return (numbers, replacedCount);
		}

		/// <summary>
		/// Метод записи результатов в файл CSV.
		/// </summary>
		/// <param name="results">Список результатов анализа.</param>
		/// <param name="filePath">Путь для сохранения файла CSV.</param>
		public static void WriteCSVData(List<ClusterAnalysisResult> results, string filePath)
		{
			if (results == null || !results.Any())
				throw new ArgumentException("Результаты анализа пусты.");

			var sb = new StringBuilder();

			// Заголовок CSV
			sb.AppendLine("Кластер №;Весовой коэффициент;Закон распределения;" +
				"Параметры закона распределения;Величина отклонения");

			// Данные
			foreach (var result in results)
			{
				sb.AppendLine($"{result.ClusterNumber};" +
							  //$"{result.Interval};" +
							  $"{result.Weight};" +
							  $"{result.Distribution};" +
							  $"{result.Parameters};" +
							  $"{result.Deviation}");
			}

			// Сохранение в файл
			File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
		}
	}
}
