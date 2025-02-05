using Newtonsoft.Json;
using prb_session2_first_try.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace prb_session2_first_try.ClassHelpers
{
    public class ApiHelper
    {
        private static HttpClient _httpClient;
        private static string _url = "https://localhost:7208";
        public static void SetHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public static async Task<List<Departament>> GetDepartaments()
        {
            string urlDepartaments = _url + "/Departament/GetDepartaments";
            var response = await _httpClient.GetAsync(urlDepartaments);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Departament>>(content);
            }
            else
            {
                throw new Exception($"Ошибка при получении данных. Код ошибки: {response.StatusCode}. Текст ошибки: {response.Content}.");
            }
        }

        public static async Task<List<Worker>> GetWorkersFromDepartament(int idDepartament)
        {
            string urlWorkers = _url + $"/Workers/GetWorkersFromDepartament/{idDepartament}";
            var response = await _httpClient.GetAsync(urlWorkers);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Worker>>(content);
            }
            else
            {
                throw new Exception($"Ошибка при получении данных. Код ошибки: {response.StatusCode}. Текст ошибки: {response.Content}.");
            }
        }


        public static async Task<List<Worker>> GetWorkersFromDepartamentAndChilds(int idDepartament)
        {
            string urlWorkers = _url + $"/Workers/GetWorkersFromDepartamentAndChilds/{idDepartament}";
            var response = await _httpClient.GetAsync(urlWorkers);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Worker>>(content);
            }
            else
            {
                throw new Exception($"Ошибка при получении данных. Код ошибки: {response.StatusCode}. Текст ошибки: {response.Content}.");
            }
        }

        public static async Task<Worker> GetWorker(int id)
        {
            string url = _url + $"/Workers/GetWorker/{id}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Worker>(content);
            }
            else
            {
                throw new Exception("Ошибка при получении данных рабочего.");
            }
        }

        public static async Task<WorkerPrivateInfo> GetWorkerPrivateInfo(int id)
        {
            string url = _url + $"/Workers/GetWorkersPrivateInfo/{id}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<WorkerPrivateInfo>(content);
            }
            else
            {
                throw new Exception("Ошибка при получении личных данного рабочего.");
            }
        }

        public static async Task<ObservableCollection<CalendarNode>> GetWorkerCalendar(int id)
        {
            string url = _url + $"/Workers/GetWorkersCalendar/{id}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ObservableCollection<CalendarNode>>(content);
            }
            else
            {
                throw new Exception("Ошибка при получении событий данного рабочего.");
            }
        }

        public static async Task<string> DeleteWorker(int id)
        {
            string url = _url + $"/Workers/DeleteWorker/{id}";
            var response = await _httpClient.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            else
            {
                throw new Exception("Ошибка при увольнении данного рабочего.");
            }
        }

        public static async Task<string> PutWorker(int id, Worker worker)
        {
            string url = _url + $"/Workers/ChangeWorker/{id}";
            var jsonContent = JsonConvert.SerializeObject(worker);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, httpContent);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            else
            {
                throw new Exception("Ошибка при изменении данного рабочего.");
            }
        }

        public static async Task<List<Position>> GetPositions()
        {
            string url = _url + $"/Position/GetPositions";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Position>>(content);
            }
            else
            {
                throw new Exception($"Ошибка при получении данных. Код ошибки: {response.StatusCode}. Текст ошибки: {response.Content}.");
            }
        }

        public static async Task<List<Cabinet>> GetCabinets()
        {
            string url = _url + $"/Cabinet/GetCabinets";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Cabinet>>(content);
            }
            else
            {
                throw new Exception($"Ошибка при получении данных. Код ошибки: {response.StatusCode}. Текст ошибки: {response.Content}.");
            }
        }
    }
}
