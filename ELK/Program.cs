using Nest;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ELK
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = new ConnectionSettings(new Uri("http://127.0.0.1:9200/")).DefaultIndex("article");
            var client = new ElasticClient(settings);

            var r = client.DeleteByQuery<Article>(d=>d.QueryOnQueryString("selete * from article where Title like 'Test%'"));

//.Match(m => m
//    .Field(f => f.Title)
//    .Query("Test"))
//   )
//);
            List <Article> list = new List<Article>();
            for (int i = 0; i < 10; i++)
            {
                Article article = new Article();
                article.Name = $"Jack{i}";
                article.Age = 20+i;
                article.Title = $"Test{i}";
                article.Tags = new string[] { "a","b","c"};
                list.Add(article);
            }

            client.IndexMany<Article>(list);
            Thread.Sleep(200);
            var search = client.Search<Article>(s => s
                .From(0)
                .Size(10)
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.Title)
                        .Query("Test2"))
                    )
            //  .Highlight(h => h.Fields(e => e.Field("title")
            //                  .PreTags("<b style='color:red'>")
            //                  .PostTags("</b>")))
            //.Sort(r => r.Descending(q => q.CreateDate))

            //在工作中把<b style='color:red'>这个换成em标签就可以了,然后在css里面给em加上高亮即可
            );

            var articles = search.Documents;
            foreach (var item in articles)
            {
                Console.WriteLine(item.ToString()); 
            }
            Console.ReadLine();
            //foreach (var hit in search.Hits)
            //{
            //    foreach (var highlightField in hit.Highlight)
            //    {
            //        if (highlightField.Key == "title")
            //        {
            //            foreach (var highlight in highlightField.Value)
            //            {
            //                hit.Source.Title = highlight.ToString();
            //            }
            //        }
            //    }
            //}

        }
    }
}
