using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Net;
using System.IO;
using HtmlAgilityPack;

namespace Find_My_Game
{
     class stores
    {
        static string[,] store = new string[4, 4] // stores-data{name, link, html-class, photo, space-parameter}
        {
            {"Steam","https://store.steampowered.com/search/?term=","//div[@class='col search_price  responsive_secondrow']","+" },
            {"Games 2 Egypt", "https://www.games2egypt.com/Web/Products/Index?search=","//span[@class='int_price']", "-" },
            {"Amazon", "https://www.amazon.eg/s?k=","//span[@class='a-price-whole']", "+" },
            {"gamestop", "https://www.gamestop.com/search/?q=", "//span[@class='actual-price ']","+"}
        };
        public  string[,] search_results { get; set; } = new string[4, 4] // search-results{"store-name, game-name, price}
        {
            {null,null,null, null},
            {null, null, null, null},
            {null, null, null,null},
            {null, null, null,null}
        };
        public  string[,] logo { get; set; } = new string[4, 2] // search-results{"store-name, game-name, price}
        {
            {"Steam","0" },
            {"Games 2 Egypt","1" },
            {"Amazon","2"},
            {"gamestop", "3"}
        };
        public  void search(string game,string[] stores_tosearch)
        {
            //ring text = logo.GetValue()
            for (int i = 0; i < store.GetLength(0); i++)
            {
                for (int j = 0; j < stores_tosearch.Length; j++)
                {


                    if (stores_tosearch[j] != null)
                    {
                        if (store[i, 0].ToLower() == stores_tosearch[j].ToLower())
                        {


                            game.Replace(" ", store[i, 3]);

                            var dataout = BeginSearch(game, store[i, 1]);
                            using (StreamWriter writetext = new StreamWriter("datasteam.txt"))
                            {
                                writetext.Write(dataout);
                            }


                            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();

                            doc.LoadHtml(dataout);

                            try
                            {

                                var results = doc.DocumentNode.SelectNodes(store[i, 2]);
                                doc.OptionEmptyCollection = true;
                                HtmlNodeCollection results1 = results;
                                if (results1 != null)
                                {
                                    foreach (var result in results)
                                    {
                                        //table.Rows.Add(name[i],game, result);
                                        search_results[j, 0] = stores_tosearch[j];
                                        search_results[j, 1] = game;
                                        search_results[j, 2] = result.InnerText;
                                       /* if (search_results[j,0].ToLower() =="amazon")
                                        {
                                            search_results[j, 2].Replace(",", "");
                                            search_results[j, 2].Replace(".", "");
                                        }*/
                                        for (int m = 0; m < 4; m++)
                                        {
                                            if (search_results[j, 0].ToLower() == store[m, 0].ToLower())
                                            {
                                                search_results[j, 3] = store[i, 1] + game.Replace(" ", store[i, 3]);
                                            }
                                        }
                                        




                                        break;
                                    }
                                }
                                else
                                {
                                    search_results[i, 1] = game;
                                    search_results[i, 2] = "can't found";
                                }

                            }

                            catch (Exception ex)
                            {
                                search_results[i, 1] = "error";
                                search_results[i, 2] = ex.ToString();

                            }

                        }
                    }
                }
            }
        }
        
         string BeginSearch(string GameName, string store)
        {

           
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(store + GameName);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream StreamRec = response.GetResponseStream();
                StreamReader streamReader = null;
                if (response.CharacterSet.ToString() != null)
                {
                    streamReader = new StreamReader(StreamRec, Encoding.GetEncoding(response.CharacterSet));
                }
                else
                {
                    streamReader = new StreamReader(StreamRec);
                }
                string PageData = streamReader.ReadToEnd();
                response.Close();
                streamReader.Close();
                return (PageData);
            }
            else
            {
                return ("Error while connecting to " + store);
            }
        }
    }
}
