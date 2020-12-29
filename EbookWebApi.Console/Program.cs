using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EbookWebApi.Console
{
    public class Category
    {
        public int Id { get; set; }
        public string Nama { get; set; }
    }
    class Program
    {
        private const string _url = "http://localhost:50001/";

        static async Task Main(string[] args)
        {
           
            try
            {
                bool showMenu = true;
                while (showMenu)
                {
                    showMenu =await  MainMenu();
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                System.Console.Read();
                bool showMenu = true;
                while (showMenu)
                {
                    showMenu =await  MainMenu();
                }

            }
        }
        //Sintaks detail dapat dilihat di project lampiran
        private static  async Task GetAll()
        {
            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_url);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));


            List<Category> categories = null;
            HttpResponseMessage response = await client.GetAsync($"{_url}api/categories");
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                categories = JsonConvert.DeserializeObject<List<Category>>(responseBody);

            }
            else
            {
                System.Console.WriteLine("Sorry, something went wrong");
                System.Console.ReadLine();
                return;
            }

            if (categories != null)
            {
                foreach (var itm in categories)
                {
                    System.Console.WriteLine($"ID: {itm.Id}\tNama: " +
                                             $"{itm.Nama}");
                }
            }


            GoToMainMenu();
        }
        
        private static async Task GetById()
        {
            System.Console.Write("Please enter Category ID : ");
            string id = System.Console.ReadLine();
            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_url);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));


            Category category = null;
            HttpResponseMessage response = await client.GetAsync($"{_url}api/categories/{id}");
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                category = JsonConvert.DeserializeObject<Category>(responseBody);

            }
            else
            {
                System.Console.WriteLine("Sorry, something went wrong");
                System.Console.ReadLine();
                return;
            }
            if (category != null)
            {
                System.Console.WriteLine($"ID: {category.Id}\tNama: " + $"{category.Nama}");
            }
            else
            {
                System.Console.WriteLine("ID yang dicari tidak ditemukan");
            } 

            GoToMainMenu();


        }
        
        private static async Task Insert()
        {
            System.Console.Write("Please enter Category Name : ");
            string name = System.Console.ReadLine();

            var cat = new Category {Id=0,Nama = name};

            var content = JsonConvert.SerializeObject(cat);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "/api/categories");
            request.Content = new StringContent(content, Encoding.UTF8, "application/json");


            var response = await client.SendAsync(request);
           
            if (!response.IsSuccessStatusCode)
            {
                System.Console.WriteLine("Sorry, something went wrong");
                System.Console.ReadLine();
            }

            GoToMainMenu();
        }

        private static async Task Update()
        {
            System.Console.Write("Please enter Category ID : ");
            string id = System.Console.ReadLine();

            System.Console.Write("Please enter Category Name : ");
            string name = System.Console.ReadLine();

            var cat = new Category { Id = Convert.ToInt32(id), Nama = name };

            var content = JsonConvert.SerializeObject(cat);
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_url);
            client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header


            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, "/api/categories");
            request.Content = new StringContent(content,
                Encoding.UTF8,
                "application/json");


            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                System.Console.WriteLine("Sorry, something went wrong");
                System.Console.ReadLine();
            }

            GoToMainMenu();
        }

        private static async Task Delete()
        {
            System.Console.Write("Please enter Category ID : ");
            string id = System.Console.ReadLine();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_url);
            client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));


            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, $"/api/categories/{id}");
         


            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                System.Console.WriteLine("Sorry, something went wrong");
                System.Console.ReadLine();
            }

            GoToMainMenu();
        }

        private static async Task<bool> MainMenu()
        {
            System.Console.Clear();
            System.Console.WriteLine("Choose an option:");
            System.Console.WriteLine("1) Get All Categories");
            System.Console.WriteLine("2) Get Category By ID");
            System.Console.WriteLine("3) Insert Category");
            System.Console.WriteLine("4) Update Category");
            System.Console.WriteLine("5) Delete Category");
            System.Console.WriteLine("6) Exit");
            System.Console.Write("\r\nSelect an option: ");
           
            switch (System.Console.ReadLine())
            {
                case "1":
                    await GetAll();
                    return true;
                case "2":
                    await GetById();

                    return true;
                case "3":
                    await Insert();
                    return true;
                case "4":
                    await Update();
                    return true;

                case "5":
                    await Delete();
                    return true;
                case "6":
                    return false;
                default:
                    return true;
            }
        }
        private static void GoToMainMenu()
        {
            System.Console.WriteLine("Enter untuk menampilkan menu..");
            System.Console.ReadLine();
        }

    }
}
