using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
	public class HandlerSCADA
	{
		private static string ck11PolEP = ConfigurationManager.AppSettings["ck11EndPoint"];
		
		private static string auth = ck11PolEP + ConfigurationManager.AppSettings["ck11TokenEndPoint"];
		
		private static string measurRead = ck11PolEP + ConfigurationManager.AppSettings["ck11MeasurReadEndPoint"];
		
		public static List<string> ck11Uids = new List<string>
		{
			"A879B6EB-F0B6-4708-A422-12E8890B1D4A"
		};

		/// <summary>
		/// Схема токена доступа.
		/// </summary>
		public class Token
		{
			// Токен доступа
			public string access_token { get; set; }
			// Признак, обозначающий, что данный идентификатор может быть использовать со схемой аутентификации Bearer
			public string token_type { get; set; }
			// Количество секунд, через которое время жизни сессии истечет
			public string expires_in { get; set; }
			// Логин пользователя
			public string user_login { get; set; }
			// Имя компьютера, с которого произведен вход (получен активный токен доступа), либо IP адресс, если получить имя не удалось
			public string user_host { get; set; }
		}

		/// <summary>
		/// Схема тела запроса.
		/// </summary>
		public class ReadRequest
		{
			// UID-ы значений измерений
			public string[] uids { get; set; }
			// Левая граница интервала
			public string fromTimeStamp { get; set; }
			// Правая граница интервала
			public string toTimeStamp { get; set; }
			// Единицы измерения шага времени между столбцами: секунды, дни, месяцы, года
			public string stepUnits { get; set; }
			// Значение шага времени между столбцами
			public int stepValue { get; set; }
		}

		/// <summary>
		/// Схема тела ответа. Содержит таблицу данных.
		/// </summary>
		public class ReadResponse
		{
			// Массив объектов
			public ReadResponseValue[] value { get; set; }
		}

		/// <summary>
		/// Схема измерений.
		/// </summary>
		public class ReadResponseValue
		{
			// UID значения измерения, данные которого находятся в строке таблицы
			public string uid { get; set; }
			// Массив объектов
			public Value[] value { get; set; }
		}

		/// <summary>
		/// Схема значения в ОИК СК-11.
		/// </summary>
		public class Value
		{
			// Глобально-уникальный идентификатор
			public string uid { get; set; }
			// Перва метка времени
			public string timeStamp { get; set; }
			// Вторая метка времени
			public string timeStamp2 { get; set; }
			// Коды качества (32-битное знаковое число)
			public long qCode { get; set; }
			// Фактическое значение экземпляра данных измерения. 
			public double value { get; set; }
		}

		public static Token GetToken()
		{
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

			WebRequest webRequestToken = WebRequest.Create(auth);
			webRequestToken.Method = "POST";
			webRequestToken.Credentials = CredentialCache.DefaultCredentials;
			using (WebResponse webResponseToken = webRequestToken.GetResponse())
			{
				using (Stream tokenStream = webResponseToken.GetResponseStream())
				{
					using (StreamReader tokenStreamReader = new StreamReader(tokenStream))
					{
						string tokenBody = tokenStreamReader.ReadToEnd();
						return JsonConvert.DeserializeObject<Token>(tokenBody);
					}
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="timeStart"></param>
		/// <param name="timeEnd"></param>
		/// <param name="uids"></param>
		/// <returns></returns>
		public static ReadResponse GetDataFromCK11(string timeStart, string timeEnd, List<string> uids, int step)
		{
			ServicePointManager.Expect100Continue = true;
			ServicePointManager.DefaultConnectionLimit = 9999;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

			// Получение токена
			Token token = GetToken();

			WebRequest webRequestMeasur = WebRequest.Create(measurRead);
			webRequestMeasur.Method = "POST";
			webRequestMeasur.ContentType = "application/json";
			webRequestMeasur.Headers.Add("Authorization", token.token_type + " " + token.access_token);

			ReadRequest bodyRequest = new ReadRequest
			{
				uids = uids.ToArray(),
				fromTimeStamp = timeStart,
				toTimeStamp = timeEnd,
				stepUnits = "seconds",
				stepValue = step
			};

			string requestBodyJson = JsonConvert.SerializeObject(bodyRequest);
			using (Stream requestStream = webRequestMeasur.GetRequestStream())
			{
				using (StreamWriter requestStreamWriter = new StreamWriter(requestStream))
				{
					requestStreamWriter.Write(requestBodyJson);
				}
			}

			using (WebResponse webResponseMeasure = webRequestMeasur.GetResponse())
			{
				using (Stream responseReadMeasureStream = webResponseMeasure.GetResponseStream())
				{
					using (StreamReader responseReadMeasureStreamReader = new StreamReader(responseReadMeasureStream))
					{
						string responseReadMeasureBody = responseReadMeasureStreamReader.ReadToEnd();
						return JsonConvert.DeserializeObject<ReadResponse>(responseReadMeasureBody);
					}
				}
			}
		}
	}
}
