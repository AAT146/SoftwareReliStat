namespace ClassLibrary
{
	/// <summary>
	/// Класс для работы с CSV-файлами.
	/// </summary>
	public class HandlerCSV
	{
		/// <summary>
		/// Метод для чтения данных из CSV-файла.
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
						throw new ArgumentException("Некорректный формат файла: отсутствует второй столбец.");
					}
				}
			}

			return result.ToArray();
		}
	}
}
