using Raven.Client.Documents;
using System;

namespace BugRaven
{
    public class DocumentStoreHolder
    {
        private static readonly Lazy<IDocumentStore> store = new Lazy<IDocumentStore>(CreateStore);
        public static string[] Urls { get; set; }
        public static string DatabaseName { get; set; }

        public static IDocumentStore Store
        {
            get { return store.Value; }
        }

        private static IDocumentStore CreateStore()
        {
            var store = new DocumentStore
            {
                Urls = Urls,
                Database = DatabaseName
            }.Initialize();

            return store;
        }
    }
}